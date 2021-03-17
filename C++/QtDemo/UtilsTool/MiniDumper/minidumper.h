#ifndef MINIDUMPER_H
#define MINIDUMPER_H

#include <QString>

class MiniDumper
{
public:
    static int getMaxDumpNum();
    static void setMaxDumpNum(int value);

    static QString getDumpDir();
    static void setDumpDir(const QString &value);

    static QString getProcessuffix();
    static void setProcessuffix(const QString &value);

    static void registerUnhandledExceptionDumper();

    static void clearSuperfluousDumpFile();
    static QString fileExt();

private:
    Q_DISABLE_COPY(MiniDumper)
    static int maxDumpNum;
    static QString dumpDir;
    static QString processuffix;
};

#endif // MINIDUMPER_H
