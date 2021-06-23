#ifndef MGRBASE_H
#define MGRBASE_H

#include <QObject>

class MgrBase : public QObject
{
    Q_OBJECT
public:
    MgrBase(QObject *parent);
    virtual ~MgrBase() {}

protected:
    // 启动计时器
    void startSingleShot(int msec);

private Q_SLOTS:
    virtual void onTimerOut() = 0;
};

#endif // MGRBASE_H
