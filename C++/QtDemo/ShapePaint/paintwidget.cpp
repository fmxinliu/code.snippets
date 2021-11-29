#include "paintwidget.h"
#include <QPaintEvent>
#include <QMouseEvent>
#include <QPainter>
#include <QPen>
#include <QColor>
#include "shapelist.h"

PaintWidget::PaintWidget(QWidget *parent)
    : QWidget(parent)
    , m_curShapeType(Shape::eLine)
    , m_curShape(nullptr)
    , m_drawing(false)
    , m_shapeList(new ShapeList)
{
    setMouseTracking(true); // Real time
    setSizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);
}

PaintWidget::~PaintWidget()
{
    delete m_shapeList;
    m_shapeList = nullptr;
}

void PaintWidget::paintEvent(QPaintEvent *event)
{
    Q_UNUSED(event);

    QPainter painter(this);
    painter.fillRect(this->rect(), Qt::white);
    painter.setBrush(Qt::transparent);
    m_shapeList->draw(&painter);
}

void PaintWidget::mousePressEvent(QMouseEvent *event)
{
    m_curShape = m_shapeList->appendShape(m_curShapeType);
    if(m_curShape)
    {
        m_drawing = true;
        m_curShape->setStartPoint(event->pos());
        m_curShape->setEndPoint(event->pos());
        m_shapeList->clearSelectedShapes();
        emit updateShapeList();
    }
}

void PaintWidget::mouseMoveEvent(QMouseEvent *event)
{
    if(m_curShape && m_drawing)
    {
        m_curShape->setEndPoint(event->pos());
        update();
    }

    int x = event->pos().x();
    int y = event->pos().y();
    QString pos = "(" + QString::number(x) + "," + QString::number(y) + ")";
    emit updateMousePosition(pos);
}

void PaintWidget::mouseReleaseEvent(QMouseEvent *event)
{
    Q_UNUSED(event);
    m_drawing = false;
}

void PaintWidget::undo()
{
    m_shapeList->popShape();
    emit updateShapeList();
    update();
}

void PaintWidget::clear()
{
    m_shapeList->clearShapes();
    emit updateShapeList();
    update();
}

void PaintWidget::remove(const QString &uuid)
{
    m_shapeList->removeShape(uuid);
    emit updateShapeList();
    update();
}

void PaintWidget::setCurrentShape(Shape::EShapeType shapeTyp)
{
   if(shapeTyp != m_curShapeType)
       m_curShapeType = shapeTyp;
}

void PaintWidget::setShapeHightLight(const QString &uuid)
{
    m_shapeList->setShapeSelected(uuid);
    update();
}

QStringList PaintWidget::getShapeUuids() const
{
    return m_shapeList->getShapeUuids();
}

QString PaintWidget::getShapeName(const QString &uuid) const
{
    QString shapeName;
    Shape* shape = m_shapeList->getShape(uuid);
    if (shape)
    {
        shapeName = shape->shapeName();
    }

    return shapeName;
}

QString PaintWidget::getShapeType(const QString &uuid) const
{
    QString shapeTypeString;
    Shape* shape = m_shapeList->getShape(uuid);
    if (shape)
    {
        shapeTypeString = shape->ShapeTypeString();
    }

    return shapeTypeString;
}

bool PaintWidget::saveToFile(const QString &fileName)
{
    return m_shapeList->saveToFile(fileName);
}

bool PaintWidget::loadFromFile(const QString &fileName)
{
    return m_shapeList->loadFromFile(fileName);
}
