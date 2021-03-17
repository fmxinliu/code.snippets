#-------------------------------------------------
#
# Project created by QtCreator 2021-03-17T21:58:22
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = UtilsTool
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    MiniDumper/minidumper.cpp

HEADERS  += mainwindow.h \
    MiniDumper/minidumper.h

FORMS    += mainwindow.ui

INCLUDEPATH += MiniDumper

#
# Release编译生成pdb
#
# 方法一: qmake配置 "CONFIG+=force_debug_info" "CONFIG+=senarate_debug_info"
# 方法二: .pro配置
QMAKE_CFLAGS_RELEASE = $$QMAKE_CFLAGS_RELEASE_WITH_DEBUGINFO
QMAKE_CXXFLAGS_RELEASE = $$QMAKE_CXXFLAGS_RELEASE_WITH_DEBUGINFO
QMAKE_LFLAGS_RELEASE = $$QMAKE_LFLAGS_RELEASE_WITH_DEBUGINFO
