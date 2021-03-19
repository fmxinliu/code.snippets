#ifndef CUSTOMLINEEDIT_H
#define CUSTOMLINEEDIT_H

#include <QLineEdit>

class CustomLineEditPrivate;
class CustomLineEdit : public QLineEdit
{
    Q_OBJECT

public:
    explicit CustomLineEdit(QWidget* parent = 0);
    explicit CustomLineEdit(const QString &text, QWidget* parent = 0);
    virtual ~CustomLineEdit();

    bool imeDisabled() const;
    void DisableIME(bool disable);

signals:
    void focusIn();
    void foucusOut();

protected:
    virtual void focusInEvent(QFocusEvent *e);
    virtual void focusOutEvent(QFocusEvent *e);
    virtual bool eventFilter(QObject *target, QEvent *event);

private:
    CustomLineEditPrivate *d;
};

#endif // CUSTOMLINEEDIT_H
