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
    MiniDumper/minidumper.cpp \
    Controls/customlineedit.cpp \
    String/stringutils.cpp \
    utiltools.cpp

HEADERS  += \
    MiniDumper/minidumper.h \
    Controls/customlineedit.h \
    String/stringutils.h \
    utiltools.h \
    mainwindow.h

FORMS    += mainwindow.ui

INCLUDEPATH += MiniDumper \
    Controls \
    String

#
# Release编译生成pdb
#
# 方法1: qmake 配置 "CONFIG+=force_debug_info" "CONFIG+=senarate_debug_info"
# 方法2: pro 配置
QMAKE_CFLAGS_RELEASE = $$QMAKE_CFLAGS_RELEASE_WITH_DEBUGINFO
QMAKE_CXXFLAGS_RELEASE = $$QMAKE_CXXFLAGS_RELEASE_WITH_DEBUGINFO
QMAKE_LFLAGS_RELEASE = $$QMAKE_LFLAGS_RELEASE_WITH_DEBUGINFO

# cod
win32:contains(QMAKE_CC, cl) {
    QMAKE_CXXFLAGS -= -Zc:wchar_t-
    QMAKE_CXXFLAGS += -Zc:wchar_t
    QMAKE_CXXFLAGS += -FAcs
}

# map
win32:contains(QMAKE_LINK, link) {
    QMAKE_LFLAGS += /MAP
    QMAKE_LFLAGS += /MAPINFO:EXPORTS
}
