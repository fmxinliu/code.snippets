#include "papermgr.h"
#include <QDebug>

// 注意：必须添加 static，限制文件作用域
static int id = 0;
static const QString str_arrays[] = {
    "start pick paper",
    "start turn paper"
};

PaperMgr::PaperMgr(QObject *parent) :
    MgrBase(parent)
{
}

void PaperMgr::pickPaper()
{
    id = 0;
    qDebug() << str_arrays[id];
    startSingleShot(2000);
}

void PaperMgr::turnPaper()
{
    id = 1;
    qDebug() << str_arrays[id];
    startSingleShot(1000);
}

void PaperMgr::onTimerOut()
{
    if (id == 0)
    {
        emit pickPaperFinished(true);
//        qDebug() << "pick paper ok";
    }
    else
    {
        emit turnPaperFinished(true);
//        qDebug() << "turn paper ok";
    }
}
