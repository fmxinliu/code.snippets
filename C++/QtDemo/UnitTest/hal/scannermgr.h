#ifndef SCANNERMGR_H
#define SCANNERMGR_H

#include <QObject>
#include "mgrbase.h"

class ScannerMgr : public MgrBase
{
    Q_OBJECT
public:
    ScannerMgr(QObject *parent = 0);

public:
    // 开始扫描
    void scan();

Q_SIGNALS:
    // 扫描完成
    void scanFinished(bool ret);

protected Q_SLOTS:
    void onTimerOut();
};

#endif // SCANNERMGR_H
