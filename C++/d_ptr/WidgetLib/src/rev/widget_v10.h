#ifndef WIDGET_V10_H
#define WIDGET_V10_H

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
    static string version();

protected:
    Rect m_geometry;
};


#endif // WIDGET_V10_H
