#ifndef WIDGET_V3X_H
#define WIDGET_V3X_H

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

    // ¹«¹²²Ù×÷
    virtual void update();
    virtual void repaint();

private:
    WidgetPrivate *d_ptr;

private:
    Widget(const Widget &);
    Widget & operator=(const Widget &);
};


#endif // WIDGET_V3X_H
