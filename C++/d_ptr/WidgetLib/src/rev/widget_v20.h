#ifndef WIDGET_V20_H
#define WIDGET_V20_H

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
    static string version();

protected:
    WidgetPrivate *d_ptr;
};


#endif // WIDGET_V20_H
