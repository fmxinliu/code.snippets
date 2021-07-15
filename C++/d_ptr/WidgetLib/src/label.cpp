#include "label.h"

////////////////////////
// v2.x 数据封装
////////////////////////
#if V2_0 || V2_1
struct LabelPrivate
{
    string m_text;
    LabelPrivate(string text) : m_text(text) {}
};
#endif

Label::Label()
    // v2.x数据已封装
#if V2_0 || V2_1
    : d_ptr(new LabelPrivate(""))
#endif
{
}

Label::Label(string text)
#if V1_0 || V1_1
    : m_text(text)
#else
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
#if V1_0 || V1_1
    return m_text;
#elif V2_0 || V2_1
    return d_ptr->m_text;
#endif
}

