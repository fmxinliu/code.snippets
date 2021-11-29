#-------------------------------------------------
#
# Project created by QtCreator 2021-11-28T17:49:18
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = ShapePaint
TEMPLATE = app


SOURCES += main.cpp \
    shapeboard.cpp \
    paintwidget.cpp \
    shapes/shape.cpp \
    shapes/circle.cpp \
    shapes/line.cpp \
    shapes/rect.cpp \
    shapelist.cpp

HEADERS  += \
    shapeboard.h \
    paintwidget.h \
    shapes/shape.h \
    shapes/circle.h \
    shapes/line.h \
    shapes/rect.h \
    shapelist.h

RESOURCES += \
    images.qrc

TRANSLATIONS += \
    cn.ts

RC_ICONS += \
    main.ico
