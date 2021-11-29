#ifndef RECT_H
#define RECT_H

#include "shape.h"

class Rect : public Shape
{
public:
    Rect(QString name);
    void paint(QPainter *painter);
    bool isSelected(const QPoint &point) const;
};

#endif // RECT_H
