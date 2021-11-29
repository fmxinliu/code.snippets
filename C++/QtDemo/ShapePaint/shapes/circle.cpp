#include "circle.h"
#include <QPainter>
#include <QtMath>

Circle::Circle(QString name)
    : Shape(Shape::eCircle, name)
{
}

void Circle::paint(QPainter *painter)
{
    int dx = m_startPoint.x() - m_endPoint.x();
    int dy = m_startPoint.y() - m_endPoint.y();
    int r = (int)qSqrt(dx * dx + dy * dy);

    Shape::paint(painter);
    painter->drawEllipse(m_startPoint, r, r);

    m_center = m_startPoint;
    m_radius = r;
}

bool Circle::isSelected(const QPoint &point) const
{
    int dx = point.x() - m_center.x();
    int dy = point.y() - m_center.y();
    return (qSqrt(dx * dx + dy * dy) <= m_radius);
}
