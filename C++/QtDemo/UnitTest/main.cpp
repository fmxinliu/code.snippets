#include <QCoreApplication>
#include "motionclass.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    MotionClass motion;
    motion.connectSignals();
    motion.start();

    return a.exec();
}
