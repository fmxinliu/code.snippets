#include "shape.h"
#include <QPainter>
#include <QUuid>
#include <QMap>

Shape::Shape(Shape::EShapeType shapeType, const QString &name)
    : m_shapeType(shapeType)
    , m_shapeName(name)
    , m_isSelected(false)
    , m_uuid(QUuid::createUuid().toString())
{
}

Shape::~Shape()
{
}

void Shape::paint(QPainter *painter)
{
    if (m_isSelected)
    {
        painter->setPen(QPen(Qt::red, 3, Qt::SolidLine));
    }
    else
    {
        painter->setPen(QPen(Qt::black, 2, Qt::SolidLine));
    }
}

QPoint Shape::startPoint() const
{
    return m_startPoint;
}

void Shape::setStartPoint(const QPoint &startPoint)
{
    m_startPoint = startPoint;
}
QPoint Shape::endPoint() const
{
    return m_endPoint;
}

void Shape::setEndPoint(const QPoint &endPoint)
{
    m_endPoint = endPoint;
}

QString Shape::shapeName() const
{
    return m_shapeName;
}

void Shape::setShapeName(const QString &shapeName)
{
    m_shapeName = shapeName;
}

Shape::EShapeType Shape::shapeType() const
{
    return m_shapeType;
}

QString Shape::ShapeTypeString() const
{
    return Shape::getShapeTypeString(m_shapeType);
}

QString Shape::getShapeTypeString(EShapeType shapeType)
{
    QMap<Shape::EShapeType, QString> mapper;
    mapper.insert(Shape::eLine,   "line");
    mapper.insert(Shape::eRect,   "rect");
    mapper.insert(Shape::eCircle, "circle");

    QString shapeString = "";
    if (mapper.keys().contains(shapeType))
    {
        shapeString = mapper.value(shapeType);
    }

    return shapeString;
}

Shape::EShapeType Shape::getShapeType(const QString &str)
{
    QMap<QString, Shape::EShapeType> mapper;
    mapper.insert("line",   Shape::eLine);
    mapper.insert("rect",   Shape::eRect);
    mapper.insert("circle", Shape::eCircle);

    Shape::EShapeType shapeType = Shape::eNone;
    if (mapper.keys().contains(str.toLower()))
    {
        shapeType = mapper.value(str.toLower());
    }

    return shapeType;
}
QString Shape::uuid() const
{
    return m_uuid;
}

void Shape::setSelected(bool selected)
{
    m_isSelected = selected;
}

bool Shape::isSelected() const
{
    return m_isSelected;
}
