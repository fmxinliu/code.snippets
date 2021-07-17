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
    WidgetPrivate *d_ptr; // �����v2.0�汾��������Ա
};


#endif // WIDGET_V21_H
