#include "utiltools.h"
#include <QString>
#include <QMutex>
#include <QMap>
#ifdef Q_OS_WIN
#include <qt_windows.h>
#endif

#ifdef Q_OS_WIN
static QMutex mutex;
static QMap<QString, void *> mutexs;
#endif

bool UtilTools::isSingleton(QString identifying)
{
    bool isSingleton = false;
#ifdef Q_OS_WIN
    mutex.lock();
    std::wstring ws = identifying.toStdWString();
    const wchar_t *pw = ws.c_str();
    HANDLE hMutex = ::CreateMutexW(0, true, pw);
    if (::GetLastError() == ERROR_ALREADY_EXISTS) {
        ::CloseHandle(hMutex);
    }
    else {
        mutexs.insert(identifying, hMutex);
        isSingleton = true;
    }
    mutex.unlock();
#endif
    return isSingleton;
}

void UtilTools::ReleaseSingleton(QString identifying)
{
#ifdef Q_OS_WIN
    mutex.lock();
    if (mutexs.keys().contains(identifying)) {
        ::CloseHandle(mutexs[identifying]);
        mutexs.remove(identifying);
    }
    mutex.unlock();
#endif
}
