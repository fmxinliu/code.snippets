#ifndef WIDGET_V3X_H
#define WIDGET_V3X_H

////////////////////////////
// 切记: 不要包含 widget_p.h
////////////////////////////

#include "rect.h"
#include <string>
using namespace std;

class WidgetPrivate;
class MODULE_API Widget
{
public:
    Widget();
    virtual ~Widget();
    Rect geometry() const;
    string stylesheet() const;
    static string version();

    // 公共操作
    virtual void update();
    virtual void repaint();

protected:
    Widget(WidgetPrivate &d); // 允许子类通过它们自己的私有结构体来初始化

protected:
    WidgetPrivate *d_ptr;

private:
    Widget(const Widget &);
    Widget & operator=(const Widget &);
};


#endif // WIDGET_V3X_H
