#include "customlineedit.h"
#include <QKeyEvent>
#include <Windows.h>
#include <Imm.h>

#pragma comment(lib, "imm32.lib")

#pragma warning(push)
#pragma warning(disable : 4482)
class CustomLineEditPrivate
{
public:
    explicit CustomLineEditPrivate(HWND getHWnd = NULL, HIMC getHIMC = NULL, bool getDisableIME = false);

    HWND getHWnd() const;
    void setHWnd(const HWND &value);

    HIMC getHIMC() const;
    void setHIMC(const HIMC &value);

    bool getDisableIME() const;
    void setDisableIME(bool value);

    void enableIME();
    void disableIME();

private:
    HWND _hWnd;
    HIMC _hIMC;
    bool _disableIME;
};

CustomLineEditPrivate::CustomLineEditPrivate(HWND hWnd, HIMC hIMC, bool disableIME)
    :_disableIME(disableIME)
    , _hWnd(hWnd)
    , _hIMC(hIMC)
{
}

HWND CustomLineEditPrivate::getHWnd() const
{
    return _hWnd;
}

void CustomLineEditPrivate::setHWnd(const HWND &value)
{
    _hWnd = value;
}

HIMC CustomLineEditPrivate::getHIMC() const
{
    return _hIMC;
}

void CustomLineEditPrivate::setHIMC(const HIMC &value)
{
    _hIMC = value;
}

bool CustomLineEditPrivate::getDisableIME() const
{
    return _disableIME;
}

void CustomLineEditPrivate::setDisableIME(bool value)
{
    _disableIME = value;
}

void CustomLineEditPrivate::enableIME()
{
    if (_hWnd && IsWindow(_hWnd))
    {
        if (_hIMC)
        {
            ImmAssociateContext(_hWnd, _hIMC);
        }
        ::SetFocus(_hWnd);
    }
}

void CustomLineEditPrivate::disableIME()
{
    if (_hWnd && IsWindow(_hWnd))
    {
        _hIMC = ImmGetContext(_hWnd);
        if (_hIMC)
        {
            ImmAssociateContext(_hWnd, NULL);
        }
        ImmReleaseContext(_hWnd, _hIMC);
        ::SetFocus(_hWnd);
    }
}


CustomLineEdit::CustomLineEdit(QWidget *parent)
    : QLineEdit(parent)
    , d(new CustomLineEditPrivate((HWND)winId()))
{
    this->installEventFilter(this);
    this->setContextMenuPolicy(Qt::NoContextMenu); // 禁用右键菜单
}

CustomLineEdit::CustomLineEdit(const QString &text, QWidget *parent)
    : QLineEdit(text, parent)
    , d(new CustomLineEditPrivate((HWND)winId()))
{
    this->installEventFilter(this);
    this->setContextMenuPolicy(Qt::NoContextMenu);
}

CustomLineEdit::~CustomLineEdit()
{
    delete d;
    d = NULL;
}

bool CustomLineEdit::imeDisabled() const
{
    return d->getDisableIME();
}

void CustomLineEdit::DisableIME(bool disable)
{
    d->setDisableIME(disable);
}

void CustomLineEdit::focusInEvent(QFocusEvent *e)
{
    QLineEdit::focusInEvent(e);
    emit focusIn();
    if (d->getDisableIME())
    {
        d->disableIME();
    }
    else
    {
        d->enableIME();
    }
}

void CustomLineEdit::focusOutEvent(QFocusEvent *e)
{
    QLineEdit::focusOutEvent(e);
    emit foucusOut();
}

bool CustomLineEdit::eventFilter(QObject *target, QEvent *event)
{
    if (d->getDisableIME() &&
        this == target &&
        this->echoMode() == EchoMode::Password &&
        QEvent::KeyPress == event->type()) {
        QKeyEvent *keyEvent = static_cast<QKeyEvent *>(event);
        if (keyEvent->matches(QKeySequence::Cut) ||
            keyEvent->matches(QKeySequence::Copy) ||
            keyEvent->matches(QKeySequence::Paste) ||
            keyEvent->matches(QKeySequence::Undo)) {
            return true; // 密码框，禁用指定的快捷键
        }
    }

    return QLineEdit::eventFilter(target, event);
}
#pragma warning(pop)
