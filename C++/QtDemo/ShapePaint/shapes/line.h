#ifndef LINE_H
#define LINE_H

#include "shape.h"

class Line : public Shape
{
public:
    Line(QString name);
    void paint(QPainter *painter);
    bool isSelected(const QPoint &point) const;
};

#endif // LINE_H
