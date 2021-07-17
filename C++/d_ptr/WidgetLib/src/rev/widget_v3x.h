#ifndef WIDGET_V3X_H
#define WIDGET_V3X_H

////////////////////////////
// 切记: 不要包含 widget_p.h
////////////////////////////

#include "global.h"
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

    D_DECLARE_PRIVATE(Widget)
    QT_DISABLE_COPY(Widget)
};


#endif // WIDGET_V3X_H
