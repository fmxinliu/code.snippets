#include "shapeboard.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    ShapeBoard w;
    w.show();

    return a.exec();
}
