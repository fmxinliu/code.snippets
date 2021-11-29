#include "rect.h"
#include <QPainter>

Rect::Rect(QString name)
    : Shape(Shape::eRect, name)
{
}

void Rect::paint(QPainter *painter)
{
    Shape::paint(painter);
    painter->drawRect(m_startPoint.x(), m_startPoint.y(), m_endPoint.x() - m_startPoint.x(), m_endPoint.y() - m_startPoint.y());
}

bool Rect::isSelected(const QPoint &point) const
{
    int dx = (point.x() - m_startPoint.x()) * (point.x() - m_endPoint.x());
    int dy = (point.y() - m_startPoint.y()) * (point.y() - m_endPoint.y());
    return (dx <= 0) && (dy <= 0);
}
