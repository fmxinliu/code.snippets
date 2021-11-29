#include "shapeboard.h"
#include <QApplication>
#include <QTranslator>
#include <QDir>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);

    QTranslator translator;
    bool b = false;
    b = translator.load(qApp->applicationDirPath() + QDir::separator() + "cn.qm");
    qApp->installTranslator(&translator);

    ShapeBoard w;
    w.show();

    return a.exec();
}
