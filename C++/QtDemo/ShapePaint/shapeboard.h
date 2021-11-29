#ifndef SHAPEBOARD_H
#define SHAPEBOARD_H

#include <QMainWindow>

class PaintWidget;
class QActionGroup;
class QTableWidget;
class QTableWidgetItem;
class QSplitter;
class QLabel;
class ShapeBoard : public QMainWindow
{
    Q_OBJECT

public:
    ShapeBoard(QWidget *parent = 0);
    ~ShapeBoard();

private:
    void createActions();
    void createMenu();
    void createToolBar();
    void createStartBar();
    void createCentralWidget();
    void createTableWidget();
    void createPaintWidget();
    void initBoardPropty();

private:    
    QMenu*          m_fileMenu;
    QMenu*          m_drawMenu;
    QMenu*          m_editMenu;

    QToolBar*       m_fileToolBar;
    QToolBar*       m_drawToolBar;
    QToolBar*       m_editToolBar;

    QStatusBar*     m_statusBar;
    QLabel*         m_statusLabel;

    QActionGroup*   m_group;
    QList<QAction*> m_fileActions;
    QList<QAction*> m_drawActions;
    QList<QAction*> m_editActions;

    PaintWidget*    m_paintWidget;
    QSplitter*      m_mainSplitter;
    QTableWidget*   m_shapeListWidget;

private Q_SLOTS:
    void onRrawLine();
    void onDrawRect();
    void onDrawCircle();

    void onOpenFile();
    void onSaveFile();

    void onRemoveItem();
    void onRefreshListWidget();
    void onRefreshItemColor(QTableWidgetItem *item);

    void onRefreshMousePosition(const QString &pos);
};

#endif // SHAPEBOARD_H
