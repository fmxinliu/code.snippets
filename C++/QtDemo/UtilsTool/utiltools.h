#ifndef UTILTOOLS_H
#define UTILTOOLS_H

#include <QtGlobal>

class UtilToolsPrivate;
class UtilTools
{
public:
    static bool isSingleton(QString identifying);
    static void ReleaseSingleton(QString identifying);

private:
    Q_DISABLE_COPY(UtilTools)
};

#endif // UTILSTOOL_H
