#ifndef WIDGET_V10_H
#define WIDGET_V10_H

#include "rect.h"
#include <string>
using namespace std;

class MODULE_API Widget
{
public:
    Widget();
    virtual ~Widget();
    Rect geometry() const;
    static string version();

protected:
    Rect m_geometry;
};


#endif // WIDGET_V10_H
