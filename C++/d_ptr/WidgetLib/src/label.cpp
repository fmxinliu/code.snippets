#include "label.h"

////////////////////////
// v2.x、v3.x 数据封装
////////////////////////
#if V2_0 || V2_1 || V3_X
class LabelPrivate
{
    friend Label;
public:
#if V2_0 || V2_1
    LabelPrivate(string text) : m_text(text) {}

    // v3.x新增 q 指针，可访问外部 API 类
#elif V3_X
    LabelPrivate(string text, Label *q) : q_ptr(q), m_text(text) {}
    void Func() { q_ptr->repaint(); }
#endif

private:
    string m_text;
#if V3_X
    Label *q_ptr; // q 指针
#endif
};
#endif

Label::Label()
    // v2.x数据已封装
#if V2_0 || V2_1
    : d_ptr(new LabelPrivate(""))
#endif
{
    // v3.x新增 q 指针
#if V3_X
    d_ptr = new LabelPrivate("", this);
#endif
}

Label::Label(string text)
#if V1_0 || V1_1
    : m_text(text)
    // v2.x数据已封装
#elif V2_0 || V2_1
    : d_ptr(new LabelPrivate(text))
#endif
{
    // v3.x新增 q 指针
#if V3_X
    d_ptr = new LabelPrivate(text, this);
#endif
}

Label::~Label()
{
    // v2.x、v3.x 释放 d 指针
#if V2_0 || V2_1 || V3_X
    delete d_ptr;
    d_ptr = NULL;
#endif
}

string Label::text() const
{
    // v1.x 直接访问
#if V1_0 || V1_1
    return m_text;
    // v2.x、v3.x 通过 d 指针访问
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
