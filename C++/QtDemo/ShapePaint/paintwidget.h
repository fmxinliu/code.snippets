#ifndef PAINTWIDGET_H
#define PAINTWIDGET_H

#include <QWidget>
#include <QList>
#include "shapes/shape.h"

class ShapeList;
class QPaintEvent;
class QMouseEvent;

class PaintWidget : public QWidget
{
    Q_OBJECT

public:
    PaintWidget(QWidget *parent = 0);
    ~PaintWidget();

    bool saveToFile(const QString &fileName);
    bool loadFromFile(const QString &fileName);

    void setCurrentShape(Shape::EShapeType shapeTyp);
    void setShapeHightLight(const QString &uuid);

    QStringList getShapeUuids() const;
    QString getShapeName(const QString &uuid) const;
    QString getShapeType(const QString &uuid) const;

Q_SIGNALS:
    void updateShapeList();
    void updateMousePosition(const QString &pos);

public Q_SLOTS:
    void undo();
    void clear();
    void remove(const QString &uuid);

protected:
    void paintEvent(QPaintEvent *event);
    void mousePressEvent(QMouseEvent *event);
    void mouseMoveEvent(QMouseEvent *event);
    void mouseReleaseEvent(QMouseEvent *event);

private:
    Shape::EShapeType   m_curShapeType;
    Shape*              m_curShape;
    bool                m_drawing;
    ShapeList*          m_shapeList;
};

#endif // PAINTWIDGET_H
