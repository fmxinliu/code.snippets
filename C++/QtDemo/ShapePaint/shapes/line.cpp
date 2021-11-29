#include "line.h"
#include <QPainter>

Line::Line(QString name)
    : Shape(Shape::eLine, name)
{
}

void Line::paint(QPainter *painter)
{
    Shape::paint(painter);
    painter->drawLine(m_startPoint, m_endPoint);
}

bool Line::isSelected(const QPoint &point) const
{
    if (m_startPoint.x() == m_endPoint.x())
        return point.y() == m_endPoint.y();

    double k1 = (m_endPoint.y() - point.y()) / (m_endPoint.x() - point.x());
    double k2 = (m_startPoint.y() - point.y()) / (m_startPoint.x() - point.x());
    return qAbs(k1 - k2) < 1e-8;
}
