#include "railmgr.h"
#include <QDebug>

static int id = 0;
static const QString str_arrays[] = {
    "start rail feed one",
    "start rail move fast"
};

RailMgr::RailMgr(QObject *parent) :
    MgrBase(parent)
{
}

void RailMgr::feedOne()
{
    id = 0;
    qDebug() << str_arrays[id];
    startSingleShot(2000);
}

void RailMgr::moveFast()
{
    id = 1;
    qDebug() << str_arrays[id];
    startSingleShot(500);
}

void RailMgr::onTimerOut()
{
    if (id == 0)
    {
        emit feedOneFinished(true);
//        qDebug() << "rail feed one ok";
    }
    else
    {
        emit fastMoveFinished(true);
//        qDebug() << "rail move fast ok";
    }
}
