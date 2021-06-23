#-------------------------------------------------
#
# Project created by QtCreator 2021-06-22T21:48:32
#
#-------------------------------------------------

QT       += core

QT       -= gui

TARGET = UnitTest
CONFIG   += console
CONFIG   -= app_bundle

TEMPLATE = app


SOURCES += main.cpp \
    motionclass.cpp \
    hal/mgrbase.cpp \
    hal/papermgr.cpp \
    hal/railmgr.cpp \
    hal/scannermgr.cpp

HEADERS += \
    motionclass.h \
    hal/mgrbase.h \
    hal/papermgr.h \
    hal/railmgr.h \
    hal/scannermgr.h

INCLUDEPATH += hal
