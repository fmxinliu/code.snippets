#include "motionclass.h"
#include "scannermgr.h"
#include "papermgr.h"
#include "railmgr.h"
#include <QDebug>

class MotionClassPrivate
{
public:
    MotionClassPrivate(MotionClass *parent);
    ~MotionClassPrivate();

public:
    QString      m_testType; // 测试类型
    MotionClass* m_parent;

    /*************
     * 组件模拟器 *
     *************/
    ScannerMgr* m_canner;
    PaperMgr*   m_paper;
    RailMgr*    m_rail;
};

MotionClass::MotionClass(QObject *parent) :
    QObject(parent),
    d(new MotionClassPrivate(this))

{
}

MotionClass::~MotionClass()
{
    delete d;
}

void MotionClass::start()
{
    d->m_paper->pickPaper();
}

void MotionClass::setArgs()
{
    d->m_testType = "MotionClass";
}

void MotionClass::connectSignals()
{
    connect(d->m_canner, SIGNAL(scanFinished(bool)),      this, SLOT(onScanFinished(bool)));
    connect(d->m_paper,  SIGNAL(pickPaperFinished(bool)), this, SLOT(onPickPaperFinished(bool)));
    connect(d->m_paper,  SIGNAL(turnPaperFinished(bool)), this, SLOT(onTurnPaperFinished(bool)));
    connect(d->m_rail,   SIGNAL(feedOneFinished(bool)),   this, SLOT(onFeedOneFinished(bool)));
    connect(d->m_rail,   SIGNAL(fastMoveFinished(bool)),  this, SLOT(onFastMoveFinished(bool)));
}

void MotionClass::disconnectSignals()
{
    disconnect(d->m_canner, 0, this, 0);
    disconnect(d->m_paper, 0, this, 0);
    disconnect(d->m_rail, 0, this, 0);
}

void MotionClass::onPickPaperFinished(bool ret)
{
    qDebug() << "== onPickPaperFinished : " << ret << endl;

    d->m_paper->turnPaper();
}

void MotionClass::onTurnPaperFinished(bool ret)
{
    qDebug() << "== onTurnPaperFinished : " << ret << endl;

    d->m_rail->moveFast();
}

void MotionClass::onFastMoveFinished(bool ret)
{
    qDebug() << "== onFastMoveFinished : " << ret << endl;

    d->m_rail->feedOne();
}

void MotionClass::onFeedOneFinished(bool ret)
{
    qDebug() << "== onFeedOneFinished : " << ret << endl;

    d->m_canner->scan();
}

void MotionClass::onScanFinished(bool ret)
{
    qDebug() << "== onScanFinished : " << ret << endl;
}

MotionClassPrivate::MotionClassPrivate(MotionClass *parent) :
    m_parent(parent)
{
    m_canner = new ScannerMgr;
    m_paper = new PaperMgr;
    m_rail = new RailMgr;
}

MotionClassPrivate::~MotionClassPrivate()
{
    delete m_canner;
    delete m_paper;
    delete m_rail;
}
