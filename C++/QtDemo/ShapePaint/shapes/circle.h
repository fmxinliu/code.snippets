#ifndef CIRCLE_H
#define CIRCLE_H

#include "shape.h"

class Circle : public Shape
{
public:
    Circle(QString name);
    void paint(QPainter *painter);
    bool isSelected(const QPoint &point) const;

private:
    QPoint m_center;
    int    m_radius;
};

#endif // CIRCLE_H
