#ifndef WIDGET_V20_H
#define WIDGET_V20_H

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
    static string version();

private:
    WidgetPrivate *d_ptr;
};


#endif // WIDGET_V20_H
