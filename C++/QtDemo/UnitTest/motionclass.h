#ifndef MOTIONCLASS_H
#define MOTIONCLASS_H

#include <QObject>

class MotionClassPrivate;
class MotionClass : public QObject
{
    Q_OBJECT
public:
    explicit MotionClass(QObject *parent = 0);
    ~MotionClass();

public:
    // 启动测试
    void start();

    // 从配置文件中，加载参数
    void setArgs();

    // 连接信号
    void connectSignals();

    // 断开信号
    void disconnectSignals();

public Q_SLOTS:
    // 选纸完成
    void onPickPaperFinished(bool ret);

    // 翻纸完成
    void onTurnPaperFinished(bool ret);

    // 快推完成，上报纸条在位检测
    void onFastMoveFinished(bool ret);

    // 进样完成
    void onFeedOneFinished(bool ret);

    // 扫描完成
    void onScanFinished(bool ret);

private:
    MotionClassPrivate *d;
};

#endif // MOTIONCLASS_H
