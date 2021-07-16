#include "label.h"

////////////////////////
// v2.x��v3.x ���ݷ�װ
////////////////////////
#if V2_0 || V2_1 || V3_X
class LabelPrivate
{
    friend Label;
public:
#if V2_0 || V2_1
    LabelPrivate(string text) : m_text(text) {}

    // v3.x���� q ָ�룬�ɷ����ⲿ API ��
#elif V3_X
    LabelPrivate(string text, Label *q) : q_ptr(q), m_text(text) {}
    void Func() { q_ptr->repaint(); }
#endif

private:
    string m_text;
#if V3_X
    Label *q_ptr; // q ָ��
#endif
};
#endif

Label::Label()
    // v2.x�����ѷ�װ
#if V2_0 || V2_1
    : d_ptr(new LabelPrivate(""))
#endif
{
    // v3.x���� q ָ��
#if V3_X
    d_ptr = new LabelPrivate("", this);
#endif
}

Label::Label(string text)
#if V1_0 || V1_1
    : m_text(text)
    // v2.x�����ѷ�װ
#elif V2_0 || V2_1
    : d_ptr(new LabelPrivate(text))
#endif
{
    // v3.x���� q ָ��
#if V3_X
    d_ptr = new LabelPrivate(text, this);
#endif
}

Label::~Label()
{
    // v2.x��v3.x �ͷ� d ָ��
#if V2_0 || V2_1 || V3_X
    delete d_ptr;
    d_ptr = NULL;
#endif
}

string Label::text() const
{
    // v1.x ֱ�ӷ���
#if V1_0 || V1_1
    return m_text;
    // v2.x��v3.x ͨ�� d ָ�����
#elif V2_0 || V2_1 || V3_X
    return d_ptr->m_text;
#endif
}

#if V3_X
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
