#include "scannermgr.h"
#include <QDebug>

static int id = 0;
static const QString str_arrays[] = {
    "start scan",
    "start turn paper"
};

ScannerMgr::ScannerMgr(QObject *parent) :
    MgrBase(parent)
{
}

void ScannerMgr::scan()
{
    id = 0;
    qDebug() << str_arrays[id];
    startSingleShot(2000);
}

void ScannerMgr::onTimerOut()
{
    emit scanFinished(true);
//    qDebug() << "scan ok";
}
