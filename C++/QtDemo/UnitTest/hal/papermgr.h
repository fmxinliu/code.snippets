#ifndef PAPERMGR_H
#define PAPERMGR_H

#include <QObject>
#include "mgrbase.h"

class QTimer;
class PaperMgr : public MgrBase
{
    Q_OBJECT
public:
    PaperMgr(QObject *parent = 0);

public:
    // 选纸
    void pickPaper();

    // 翻纸
    void turnPaper();

Q_SIGNALS:
    // 选纸完成
    void pickPaperFinished(bool ret);

    // 翻纸完成
    void turnPaperFinished(bool ret);

protected Q_SLOTS:
    void onTimerOut();
};

#endif // PAPERMGR_H
