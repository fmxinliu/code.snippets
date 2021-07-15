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
    WidgetPrivate *d_ptr; // �����v2.0�汾��������Ա
};


#endif // WIDGET_V21_H
