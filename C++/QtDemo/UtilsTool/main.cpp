#include "mainwindow.h"
#include <QApplication>
#include <QMessageBox>
#include "minidumper.h"
#include "stringutils.h"
#include "utiltools.h"

void InitMiniDumper();
void TestStringUtils();
void TestSingleton();

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    TestSingleton();

//    InitMiniDumper();
//    int *p = 0;
//    *p = 100;

    TestStringUtils();

    MainWindow w;
    w.show();

    return a.exec();
}

void InitMiniDumper()
{
    // mini dumper
    MiniDumper::setDumpDir("D:\\");
    MiniDumper::setMaxDumpNum(5);
    MiniDumper::setProcessuffix("UtilsTool");
    MiniDumper::registerUnhandledExceptionDumper();
}

void TestStringUtils()
{
    QString s1, s2;
    QString str = "123";

    s1 = StringUtils::toHexString(str);
    s2 = StringUtils::toAsciiString(s1);
    Q_ASSERT(s2 == str);

    s1 = StringUtils::toHexString(str, 3);
    s2 = StringUtils::toAsciiString(s1, 3);
    Q_ASSERT(s2 == str);

    s1 = StringUtils::toHexString(str, 1);
    s2 = StringUtils::toAsciiString(s1, 1);
    Q_ASSERT(s2 == str);

    s1 = StringUtils::toHexString(str, " ");
    s2 = StringUtils::toAsciiString(s1, " ");
    Q_ASSERT(s2 == str);

    s1 = StringUtils::toHexString(str, ", ", 4);
    s2 = StringUtils::toAsciiString(s1, ", ", 4);
    Q_ASSERT(s2 == str);

    s1 = StringUtils::toHexString(str, " ", 5, true);
    s2 = StringUtils::toAsciiString(s1, " ", 5);
    Q_ASSERT(s2 == str);
}

void TestSingleton()
{
    QString s = "App单例运行";
    if (!UtilTools::isSingleton(s)){
        QMessageBox::warning(0, "Warn", "application already started!");
        exit(-1);
    }
}
