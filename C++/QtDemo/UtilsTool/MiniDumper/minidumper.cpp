#include "minidumper.h"
#include <QDateTime>
#include <QStringList>
#include <QDir>
#include <Windows.h>
#include <DbgHelp.h>

#pragma comment(lib, "dbghelp.lib")

int MiniDumper::maxDumpNum = 3;
QString MiniDumper::dumpDir = "";
QString MiniDumper::processuffix = "";

LONG WINAPI UnhandledExceptionDumper(PEXCEPTION_POINTERS pException);

int MiniDumper::getMaxDumpNum()
{
    return maxDumpNum;
}

void MiniDumper::setMaxDumpNum(int value)
{
    maxDumpNum = (value <= 0) ? 3 : value;
}

QString MiniDumper::getDumpDir()
{
    return dumpDir;
}

void MiniDumper::setDumpDir(const QString &value)
{
    dumpDir = value;
}

QString MiniDumper::getProcessuffix()
{
    return processuffix;
}

void MiniDumper::setProcessuffix(const QString &value)
{
    processuffix = value;
}

void MiniDumper::registerUnhandledExceptionDumper()
{
    SetUnhandledExceptionFilter(UnhandledExceptionDumper); // 注册异常捕获函数
}

void MiniDumper::clearSuperfluousDumpFile()
{
    QStringList nameFilters("*.dmp");
    QStringList dumpfiles = QDir(dumpDir).entryList(nameFilters, QDir::Files | QDir::Readable, QDir::Time);

    while (dumpfiles.count() > maxDumpNum)
    {
        QFile(dumpDir + QDir::separator() + dumpfiles.last()).remove();
        dumpfiles.removeLast();
    }
}

QString MiniDumper::fileExt()
{
    QString ext = "";
    if (MiniDumper::getProcessuffix().isEmpty())
    {
        ext = ".dmp";
    }
    else
    {
        ext = "." + MiniDumper::getProcessuffix() + ".dmp";
    }

    return ext;
}

LONG WINAPI UnhandledExceptionDumper(PEXCEPTION_POINTERS pException)
{
    // dmp 文件的命名
    QString fmtstr = "yyyy-MM-dd HH-mm-ss";
    QString dumpath = MiniDumper::getDumpDir() +
            "\\MiniDmp{" + QDateTime::currentDateTime().toString(fmtstr) + "}" +
            MiniDumper::fileExt();

    HANDLE dumpHandle = CreateFile((LPCWSTR)dumpath.utf16(),
                                   GENERIC_WRITE,
                                   0,
                                   NULL,
                                   CREATE_ALWAYS,
                                   FILE_ATTRIBUTE_NORMAL,
                                   NULL);
    if (INVALID_HANDLE_VALUE != dumpHandle)
    {
        MINIDUMP_EXCEPTION_INFORMATION dumpinfo;
        dumpinfo.ExceptionPointers = pException;
        dumpinfo.ThreadId = GetCurrentThreadId();
        dumpinfo.ClientPointers = TRUE;

        // 将dump信息写入dump文件
        MiniDumpWriteDump(GetCurrentProcess(), GetCurrentProcessId(), dumpHandle, MiniDumpWithFullMemory, &dumpinfo, NULL, NULL);
        CloseHandle(dumpHandle);
        MiniDumper::clearSuperfluousDumpFile();
    }

    return EXCEPTION_EXECUTE_HANDLER;
}
