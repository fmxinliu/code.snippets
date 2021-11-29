#include "shapelist.h"
#include "shapes/line.h"
#include "shapes/rect.h"
#include "shapes/circle.h"
#include <QTextStream>
#include <QFile>

ShapeList::ShapeList()
    : m_shapeId(0)
{
}

ShapeList::~ShapeList()
{
    clearShapes();
}

Shape* ShapeList::appendShape(Shape::EShapeType shapeType)
{
    Shape* shape = createShapeBySimpleFactory(shapeType);
    if (shape)
    {
        m_shapeList.append(shape);
    }

    return shape;
}

void ShapeList::clearShapes()
{
    qDeleteAll(m_shapeList);
    m_shapeList.clear();
}

void ShapeList::popShape()
{
    if (!m_shapeList.empty())
    {
        delete m_shapeList.back();
        m_shapeList.pop_back();
    }
}

void ShapeList::removeShape(const QString &uuid)
{
    for (int i = 0; i < m_shapeList.count();)
    {
        if (m_shapeList[i]->uuid() == uuid)
        {
            delete m_shapeList[i];
            m_shapeList.removeAt(i);
        }
        else
        {
            i++;
        }
    }
}

bool ShapeList::saveToFile(const QString &fileName)
{
    QFile file(fileName);
    if (!file.open(QIODevice::WriteOnly))
        return false;

    QTextStream out(&file);
    Q_FOREACH(Shape *shape, m_shapeList)
    {
        out << shape->ShapeTypeString()   << "\r\n"
            << shape->shapeName()         << "\r\n"
            << shape->startPoint().x()    << "\r\n"
            << shape->startPoint().y()    << "\r\n"
            << shape->endPoint().x()      << "\r\n"
            << shape->endPoint().y()      << "\r\n";
    }

    file.close();
    return true;
}

bool ShapeList::loadFromFile(const QString &fileName)
{
    if (!QFile::exists(fileName))
        return false;

    QFile file(fileName);
    if (!file.open(QIODevice::ReadOnly))
        return false;

    clearShapes();

    QTextStream in(&file);
    while (!in.atEnd())
    {
        // get shapeType
        QString shapeType;
        in >> shapeType;

        Shape* shape = createShapeBySimpleFactory(shapeType.trimmed());
        if (!shape)
            continue;

        QString shapeName;
        in >> shapeName;
        shape->setShapeName(shapeName);

        int sx, sy, ex, ey;
        in >> sx >> sy >> ex >> ey;
        shape->setStartPoint(QPoint(sx, sy));
        shape->setEndPoint(QPoint(ex, ey));

        m_shapeList.append(shape);
    }

    file.close();
    return true;
}

QStringList ShapeList::getShapeUuids() const
{
    QStringList shapesUuid;
    Q_FOREACH(Shape *shape, m_shapeList)
    {
        shapesUuid << shape->uuid();
    }

    return shapesUuid;
}

Shape *ShapeList::getShape(const QString &uuid) const
{
    Q_FOREACH(Shape *shape, m_shapeList)
    {
        if (shape->uuid() == uuid)
        {
            return shape;
        }
    }

    return nullptr;
}

void ShapeList::clearSelectedShapes()
{
    Q_FOREACH(Shape *shape, m_shapeList)
    {
        if (shape->isSelected())
        {
            shape->setSelected(false);
        }
    }
}

void ShapeList::setShapeSelected(const QString &uuid)
{
    Q_FOREACH(Shape *shape, m_shapeList)
    {
        if (shape->uuid() == uuid)
        {
            shape->setSelected(true);
        }
        else
        {
            shape->setSelected(false);
        }
    }
}

void ShapeList::draw(QPainter *painter)
{
    Q_FOREACH(Shape *shape, m_shapeList)
    {
        shape->paint(painter);
    }
}

Shape* ShapeList::createShapeBySimpleFactory(Shape::EShapeType shapeType)
{
    Shape *shape = nullptr;
    switch (shapeType)
    {
        case Shape::eLine:
        {
            shape = new Line(QString("Line%1").arg(++m_shapeId));
            break;
        }
        case Shape::eRect:
        {
            shape = new Rect(QString("Rect%1").arg(++m_shapeId));
            break;
        }
        case Shape::eCircle:
        {
            shape = new Circle(QString("Circle%1").arg(++m_shapeId));
        }
    }

    return shape;
}

Shape *ShapeList::createShapeBySimpleFactory(const QString &shapeName)
{
    return createShapeBySimpleFactory(Shape::getShapeType(shapeName));
}

Shape* ShapeList::searchShapeIsSelected(QPoint point)
{
    Q_FOREACH(Shape *shape, m_shapeList)
    {
        if (shape->isSelected(point))
        {
            return shape;
        }
    }

    return nullptr;
}
