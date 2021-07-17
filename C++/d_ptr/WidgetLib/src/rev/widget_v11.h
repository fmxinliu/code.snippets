#ifndef WIDGET_V11_H
#define WIDGET_V11_H

#include "rect.h"
#include <string>
using namespace std;

class MODULE_API Widget
{
public:
    Widget();
    virtual ~Widget();
    Rect geometry() const;
    string stylesheet() const;
    static string version();

protected:
    Rect m_geometry;
    string m_stylesheet; // 相比于v1.0版本，新增成员
};


#endif // WIDGET_V11_H
