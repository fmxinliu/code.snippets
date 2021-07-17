#include "label.h"
#include "widget_p.h"

#if !V3_X
/////////////////
// v2.x ���ݷ�װ
/////////////////
#if V2_0 || V2_1
class LabelPrivate
{
    friend Label;
public:
    LabelPrivate(string text) : m_text(text) {}

private:
    string m_text;
};
#endif

Label::Label()
    // v2.x�����ѷ�װ
#if V2_0 || V2_1
    : d_ptr(new LabelPrivate(""))
#endif
{
}

Label::Label(string text)
#if V1_0 || V1_1
    : m_text(text)
    // v2.x�����ѷ�װ
#elif V2_0 || V2_1
    : d_ptr(new LabelPrivate(text))
#endif
{
}

Label::~Label()
{
#if V2_0 || V2_1
    delete d_ptr;
    d_ptr = NULL;
#endif
}

string Label::text() const
{
    // v1.x ֱ�ӷ���
#if V1_0 || V1_1
    return m_text;
    // v2.x ͨ�� d ָ�����
#elif V2_0 || V2_1
    return d_ptr->m_text;
#endif
}


/////////////////
// v3.x ���ݷ�װ
/////////////////
#elif V3_X
    // v3.x�����̳й�ϵ: d ָ���ɻ��� Widget ����q ָ���ɻ��� WidgetPrivate ����
class LabelPrivate : public WidgetPrivate
{
    friend Label;
public:
    LabelPrivate(string text, Label *q) : WidgetPrivate(q), m_text(text) {}
    void Func() { q_ptr->repaint(); }

private:
    string m_text;
};

Label::Label()
    : Widget(*new LabelPrivate("", this))
{
}

Label::Label(string text)
    : Widget(*new LabelPrivate(text, this))
{
}

Label::~Label()
{
}

string Label::text() const
{
    LabelPrivate *d =static_cast<LabelPrivate *>(d_ptr);
    return d->m_text;
}

void Label::repaint()
{
    printf("Label::repaint()\r\n");
}

void Label::refresh()
{
    d_ptr->Func();
    this->Widget::repaint();
    this->update();
}

#endif