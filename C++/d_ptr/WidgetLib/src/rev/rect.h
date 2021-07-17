#ifndef RECT_H
#define RECT_H

#include "libcommon.h"

struct MODULE_API Rect
{
    int x;
    int y;
    int width;
    int height;

    Rect() { x = y = width = height = 0; }
};

#endif // RECT_H
