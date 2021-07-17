#ifndef WIDGET_P_H
#define WIDGET_P_H

#include "widget.h"

////////////////////////
// v2.x��v3.x ���ݷ�װ
////////////////////////
#if V2_0 || V2_1 || V3_X
class WidgetPrivate
{
    friend Widget;
public:
    // v3.x���� q ָ�룬�ɷ����ⲿ API ��
#if V3_X
    WidgetPrivate(Widget *q) : q_ptr(q) {}
    void Func() { q_ptr->update(); }
#endif

private:
    Rect m_geometry;

    // �����v2.0�汾��v2.1��v3.x������Ա
#if V2_1 || V3_X
    std::string m_stylesheet;
#endif

#if V3_X
    Q_DECLARE_PRIVATE(Widget)
#endif
};

#endif

#endif // WIDGET_P_H
