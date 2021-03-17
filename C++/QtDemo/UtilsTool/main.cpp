#include "mainwindow.h"
#include <QApplication>
#include "minidumper.h"

void InitMiniDumper();

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

//    InitMiniDumper();
//    int *p = 0;
//    *p = 100;

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
