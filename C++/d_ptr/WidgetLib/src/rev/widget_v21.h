#ifndef WIDGET_V21_H
#define WIDGET_V21_H

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

protected:
    WidgetPrivate *d_ptr; // 相比于v2.0版本，新增成员
};


#endif // WIDGET_V21_H
