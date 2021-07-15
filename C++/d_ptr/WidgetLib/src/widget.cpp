#include "widget.h"

////////////////////////
// v2.x 数据封装
////////////////////////
#if V2_0 || V2_1
struct WidgetPrivate
{
    Rect m_geometry;

    // 相比于v2.0版本，v2.1新增成员
#if V2_1
    std::string m_stylesheet;
#endif
};
#endif


Widget::Widget()
    // v1.x 数据未封装
#if V1_1
    : m_stylesheet("stylesheet")
    // v2.x数据已封装
#elif V2_0 || V2_1
    : d_ptr(new WidgetPrivate)
#endif
{
}

Widget::~Widget()
{
    // v2.x释放 d 指针
#if V2_0 || V2_1
    delete d_ptr;
    d_ptr = NULL;
#endif
}

Rect Widget::geometry() const
{
     // v1.x 直接访问
#if V1_0 || V1_1
    return m_geometry;
     // v2.x通过 d 指针访问
#elif V2_0 || V2_1
    return d_ptr->m_geometry;
#endif
}

////////////////////////
// v1.1、v2.1新增
////////////////////////
#if V1_1 || V2_1
string Widget::stylesheet() const
{
#if V1_1
    return m_stylesheet; 
#else
    return d_ptr->m_stylesheet;
#endif
}
#endif 


string Widget::version()
{
#if V1_0
    return "v1.0";
#elif V1_1
    return "v1.1";
#elif V2_0
    return "v2.0";
#else V2_1
    return "v2.1";
#endif
}
