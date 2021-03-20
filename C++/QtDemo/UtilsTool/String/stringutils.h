#ifndef STRINGUTILS_H
#define STRINGUTILS_H

#include <QString>

class StringUtils
{
public:
    static QString toAsciiString(const QString &hexString);
    static QString toAsciiString(const QString &hexString, int width);
    static QString toAsciiString(const QString &hexString, const QString &delimiter);
    static QString toAsciiString(const QString &hexString, const QString &delimiter, int width);

    static QString toHexString(const QString &asciiString);
    static QString toHexString(const QString &asciiString, int width);
    static QString toHexString(const QString &asciiString, const QString &delimiter);
    static QString toHexString(const QString &asciiString, const QString &delimiter, int width);
    static QString toHexString(const QString &asciiString, const QString &delimiter, int width, bool showPrefix);

    static const char* toCharArray(QString str);
    static const wchar_t* toWCharArray(QString str);

private:
    Q_DISABLE_COPY(StringUtils)
};

#endif // STRINGUTILS_H
