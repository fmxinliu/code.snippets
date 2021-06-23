#include "mgrbase.h"
#include <QTimer>

MgrBase::MgrBase(QObject *parent) : QObject(parent)
{
}

void MgrBase::startSingleShot(int msec)
{
    QTimer::singleShot(msec, this, SLOT(onTimerOut()));
}
