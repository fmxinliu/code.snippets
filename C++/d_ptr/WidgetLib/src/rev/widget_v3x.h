#ifndef WIDGET_V3X_H
#define WIDGET_V3X_H

#include "libcommon.h"
#include <string>
using namespace std;

struct MODULE_API Rect
{
    int x;
    int y;
    int width;
    int height;

    Rect() { x = y = width = height = 0; }
};

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

protected:
    WidgetPrivate *d_ptr;
    Widget(WidgetPrivate *d);

private:
    Widget(const Widget &);
    Widget & operator=(const Widget &);
};


#endif // WIDGET_V3X_H
