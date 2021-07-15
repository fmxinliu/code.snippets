#ifndef WIDGET_V11_H
#define WIDGET_V11_H

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
