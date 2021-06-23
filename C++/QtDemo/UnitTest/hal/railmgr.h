#ifndef RAILMGR_H
#define RAILMGR_H

#include <QObject>
#include "mgrbase.h"

class RailMgr : public MgrBase
{
    Q_OBJECT
public:
    RailMgr(QObject *parent = 0);

public:
    // 进样一位
    void feedOne();

    // 快推一位
    void moveFast();

Q_SIGNALS:
    // 进样一位完成
    void feedOneFinished(bool ret);

    // 快推一位完成
    void fastMoveFinished(bool ret);

protected Q_SLOTS:
    void onTimerOut();
};

#endif // RAILMGR_H
