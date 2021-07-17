#ifndef WIDGET_V21_H
#define WIDGET_V21_H

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

private:
    WidgetPrivate *d_ptr; // 相比于v2.0版本，新增成员
};


#endif // WIDGET_V21_H
