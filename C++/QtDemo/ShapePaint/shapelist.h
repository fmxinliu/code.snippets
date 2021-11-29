#ifndef SHAPELIST_H
#define SHAPELIST_H

#include <QList>
#include "shapes/shape.h"

class Shape;
class ShapeList
{
public:
    ShapeList();
    ~ShapeList();

    void draw(QPainter* painter);

    void popShape();
    void clearShapes();
    void removeShape(const QString &uuid);

    Shape* appendShape(Shape::EShapeType shapeType);
    Shape* createShapeBySimpleFactory(Shape::EShapeType shapeType);
    Shape* createShapeBySimpleFactory(const QString &shapeName);
    Shape* searchShapeIsSelected(QPoint point);

    bool saveToFile(const QString &fileName);
    bool loadFromFile(const QString &fileName);

    QStringList getShapeUuids() const;
    Shape* getShape(const QString &uuid) const;

    void clearSelectedShapes();
    void setShapeSelected(const QString &uuid);

private:
    QList<Shape *> m_shapeList;
    long           m_shapeId;
};

#endif // SHAPELIST_H
