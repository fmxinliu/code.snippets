#ifndef SHAPE_H
#define SHAPE_H

#include <QPoint>
#include <QString>

class QPainter;
class Shape
{
public:
    enum EShapeType { eNone, eLine, eRect, eCircle };

    Shape(EShapeType shapeType, const QString &name);
    virtual ~Shape();
    virtual void paint(QPainter *painter);
    virtual bool isSelected(const QPoint &point) const = 0;

    QPoint startPoint() const;
    void setStartPoint(const QPoint &startPoint);

    QPoint endPoint() const;
    void setEndPoint(const QPoint &endPoint);

    QString shapeName() const;
    void setShapeName(const QString &shapeName);

    EShapeType shapeType() const;
    QString ShapeTypeString() const;

    static QString getShapeTypeString(EShapeType shapeType);
    static EShapeType getShapeType(const QString &str);

    QString uuid() const;

    bool isSelected() const;
    void setSelected(bool selected);

protected:
    QPoint              m_startPoint;
    QPoint              m_endPoint;
    EShapeType          m_shapeType;
    QString             m_shapeName;
    QString             m_uuid;
    bool                m_isSelected;
};

#endif // SHAPE_H
