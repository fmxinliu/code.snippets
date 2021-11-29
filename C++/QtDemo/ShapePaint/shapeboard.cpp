#include "shapeboard.h"
#include <QStatusBar>
#include <QMenuBar>
#include <QMenu>
#include <QToolBar>
#include <QActionGroup>
#include <QFileDialog>
#include <QMessageBox>
#include <QFile>
#include <QPoint>
#include <QDesktopWidget>
#include <QSplitter>
#include <QTableWidget>
#include <QTableWidgetItem>
#include <QHeaderView>
#include <QLabel>
#include "paintwidget.h"

ShapeBoard::ShapeBoard(QWidget *parent)
    : QMainWindow(parent)
{
    createTableWidget();
    createPaintWidget();
    createCentralWidget();

    createActions();
    createMenu();
    createToolBar();
    createStartBar();

    initBoardPropty();
}

ShapeBoard::~ShapeBoard()
{
}

void ShapeBoard::createActions()
{
    QAction *openFileAction = new QAction(QIcon(":/images/open.png"), tr("&Open file"), this);
    openFileAction->setToolTip(tr("Open a file."));
    openFileAction->setStatusTip(tr("Open a file."));
    connect(openFileAction, SIGNAL(triggered()), this, SLOT(onOpenFile()));
    m_fileActions << openFileAction;

    QAction *saveFileAction = new QAction(QIcon(":/images/save.png"), tr("&Save file"), this);
    saveFileAction->setToolTip(tr("Save to file."));
    saveFileAction->setStatusTip(tr("Save to file."));
    connect(saveFileAction, SIGNAL(triggered()), this, SLOT(onSaveFile()));
    m_fileActions << saveFileAction;

    m_group = new QActionGroup(this);
    QAction *drawLineAction = new QAction(QIcon(":/images/line.png"), tr("&Line"), this);
    drawLineAction->setToolTip(tr("Draw a line."));
    drawLineAction->setStatusTip(tr("Draw a line."));
    drawLineAction->setCheckable(true);
    drawLineAction->setChecked(true);
    m_group->addAction(drawLineAction);
    connect(drawLineAction, SIGNAL(triggered()), this, SLOT(onRrawLine()));
    m_drawActions << drawLineAction;

    QAction *drawRectAction = new QAction(QIcon(":/images/rect.png"), tr("&Rectangle"), this);
    drawRectAction->setToolTip(tr("Draw a rectangle."));
    drawRectAction->setStatusTip(tr("Draw a rectangle."));
    drawRectAction->setCheckable(true);
    m_group->addAction(drawRectAction);
    connect(drawRectAction, SIGNAL(triggered()), this, SLOT(onDrawRect()));
    m_drawActions << drawRectAction;

    QAction *drawCircleAction = new QAction(QIcon(":/images/circle.png"), tr("&Circle"), this);
    drawCircleAction->setToolTip(tr("Draw a circle."));
    drawCircleAction->setStatusTip(tr("Draw a circle."));
    drawCircleAction->setCheckable(true);
    m_group->addAction(drawCircleAction);
    connect(drawCircleAction, SIGNAL(triggered()), this, SLOT(onDrawCircle()));
    m_drawActions << drawCircleAction;

    QAction *undoAction = new QAction(QIcon(":/images/delete.png"), tr("&Remove Shape"), this);
    undoAction->setToolTip(tr("remove selected shape."));
    undoAction->setStatusTip(tr("remove selected shape."));
    connect(undoAction, SIGNAL(triggered()), this, SLOT(onRemoveItem()));
    m_editActions << undoAction;

    QAction *clearAction = new QAction(QIcon(":/images/clear.png"), tr("&Clear All"), this);
    clearAction->setToolTip(tr("Clear all shapes."));
    clearAction->setStatusTip(tr("Clear all shapes."));
    connect(clearAction, SIGNAL(triggered()), m_paintWidget, SLOT(clear()));
    m_editActions << clearAction;
}

void ShapeBoard::createMenu()
{
    m_fileMenu = new QMenu(tr("&File"));
    m_fileMenu->addActions(m_fileActions);

    m_drawMenu = new QMenu(tr("&Draw"));
    m_drawMenu->addActions(m_drawActions);

    m_editMenu = new QMenu(tr("&Edit"));
    m_editMenu->addActions(m_editActions);

    menuBar()->addMenu(m_fileMenu);
    menuBar()->addMenu(m_drawMenu);
    menuBar()->addMenu(m_editMenu);
}

void ShapeBoard::createToolBar()
{
    m_fileToolBar = new QToolBar(tr("&File"));
    m_fileToolBar->addActions(m_fileActions);

    m_drawToolBar = new QToolBar(tr("&Draw"));
    m_drawToolBar->addActions(m_drawActions);
    m_drawToolBar->addSeparator();

    m_editToolBar = new QToolBar(tr("&Edit"));
    m_editToolBar->addActions(m_editActions);
    m_editToolBar->addSeparator();

    addToolBar(m_fileToolBar);
    addToolBar(m_drawToolBar);
    addToolBar(m_editToolBar);
}

void ShapeBoard::createStartBar()
{
    m_statusBar = new QStatusBar;
    setStatusBar(m_statusBar);

    //右下角状态栏显示鼠标位置
    m_statusLabel = new QLabel;
    m_statusLabel ->resize(200, 30);
    m_statusBar->addPermanentWidget(m_statusLabel);
}

void ShapeBoard::createCentralWidget()
{
    m_mainSplitter = new QSplitter(Qt::Horizontal, this);
    m_mainSplitter->addWidget(m_paintWidget);
    m_mainSplitter->addWidget(m_shapeListWidget);
    m_mainSplitter->setStretchFactor(0, 90);
    m_mainSplitter->setStretchFactor(1, 10);
    setCentralWidget(m_mainSplitter);
}

void ShapeBoard::initBoardPropty()
{
    QDesktopWidget dw;
    const double rate = 0.7;
    int x = (int)(dw.width() * rate);
    int y = (int)(dw.height() * rate);
    this->resize(x, y);
    setWindowTitle(tr("ShapePaint Tool"));
}

void ShapeBoard::createTableWidget()
{
    QStringList header;
    header << tr("Name") << tr("Sharp Type");
    m_shapeListWidget = new QTableWidget(this);
    m_shapeListWidget->setColumnCount(3);
    m_shapeListWidget->setHorizontalHeaderLabels(header);
    m_shapeListWidget->setSelectionBehavior(QAbstractItemView::SelectRows);
    m_shapeListWidget->setSelectionMode(QAbstractItemView::SingleSelection);
    m_shapeListWidget->setEditTriggers(QAbstractItemView::NoEditTriggers);
    m_shapeListWidget->verticalHeader()->setSectionResizeMode(QHeaderView::Fixed);
    m_shapeListWidget->hideColumn(2);
    connect(m_shapeListWidget, SIGNAL(itemClicked(QTableWidgetItem *)), this, SLOT(onRefreshItemColor(QTableWidgetItem *)));
}

void ShapeBoard::createPaintWidget()
{
    m_paintWidget = new PaintWidget(this);
    connect(m_paintWidget, SIGNAL(updateShapeList()), this, SLOT(onRefreshListWidget()));
    connect(m_paintWidget, SIGNAL(updateMousePosition(const QString&)), this, SLOT(onRefreshMousePosition(const QString&)));
}

void ShapeBoard::onRrawLine()
{
    m_paintWidget->setCurrentShape(Shape::eLine);
}

void ShapeBoard::onDrawRect()
{
    m_paintWidget->setCurrentShape(Shape::eRect);
}

void ShapeBoard::onDrawCircle()
{
    m_paintWidget->setCurrentShape(Shape::eCircle);
}

void ShapeBoard::onOpenFile()
{
    QString fileName = QFileDialog::getOpenFileName(this, tr("Open File"), ".", tr("dat Files (*.dat)"));
    if (fileName.isEmpty())
        return;

    if (!m_paintWidget->loadFromFile(fileName))
    {
        QMessageBox::warning(this, tr("Warning"), tr("File not opened"));
    }

    m_paintWidget->update();
    onRefreshListWidget();
}

void ShapeBoard::onSaveFile()
{
    QString fileName = QFileDialog::getSaveFileName(this, tr("Save File"), ".", tr("dat Files (*.dat)"));
    if (fileName.isEmpty())
        return;

    if (!m_paintWidget->saveToFile(fileName))
    {
        QMessageBox::warning(this, tr("Warning"), tr("File not saved!"));
    }
}

void ShapeBoard::onRemoveItem()
{
    QList<QTableWidgetItem*> items = m_shapeListWidget->selectedItems();
    if (items.count() > 0)
    {
        QTableWidgetItem *item = m_shapeListWidget->item(items[0]->row(), 2);
        QString uuid = item->text();
        m_paintWidget->remove(uuid);
    }
    else
    {
        m_paintWidget->undo();
    }
}

void ShapeBoard::onRefreshItemColor(QTableWidgetItem *item)
{
    int row = item->row();
    m_paintWidget->setShapeHightLight(m_shapeListWidget->item(row, 2)->text());
}

void ShapeBoard::onRefreshMousePosition(const QString &pos)
{
    m_statusLabel->setText(pos);
}

static QIcon getShapeQIcon(QString shapeTypeString)
{
    QMap<QString, QIcon> mapper;
    mapper.insert("line",   QIcon(":/images/line.png"));
    mapper.insert("rect",   QIcon(":/images/rect.png"));
    mapper.insert("circle", QIcon(":/images/circle.png"));

    QIcon icon;
    if (mapper.keys().contains(shapeTypeString.toLower()))
    {
        icon = mapper.value(shapeTypeString.toLower());
    }

    return icon;
}

void ShapeBoard::onRefreshListWidget()
{
    m_shapeListWidget->setRowCount(0);
    Q_FOREACH(QString uuid, m_paintWidget->getShapeUuids())
    {
        int row = m_shapeListWidget->rowCount();
        m_shapeListWidget->insertRow(row);
        m_shapeListWidget->setItem(row, 0, new QTableWidgetItem(m_paintWidget->getShapeName(uuid)));
        m_shapeListWidget->setItem(row, 1, new QTableWidgetItem(getShapeQIcon(m_paintWidget->getShapeType(uuid)), m_paintWidget->getShapeType(uuid)));
        m_shapeListWidget->setItem(row, 2, new QTableWidgetItem(uuid));
    }
}

