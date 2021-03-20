#include "stringutils.h"
#include <QDebug>

QString StringUtils::toAsciiString(const QString &hexString)
{
    return toAsciiString(hexString, "", 2);
}

QString StringUtils::toAsciiString(const QString &hexString, int width)
{
    return toAsciiString(hexString, "", width);
}

QString StringUtils::toAsciiString(const QString &hexString, const QString &delimiter)
{
    return toAsciiString(hexString, delimiter, 2);
}

QString StringUtils::toAsciiString(const QString &hexString, const QString &delimiter, int width)
{
    QString str = hexString.split("0x").join("").split(delimiter).join("");
    int fieldWidth = (width > 2) ? width : 2;
    if (str.length() % fieldWidth != 0) {
        return "";
    }

    bool ok;
    QString asciiString;
    for (int i = 0; i < str.length(); i += fieldWidth) {
        QString byteString = str.mid(i + fieldWidth - 2, 2);
        char letter = byteString.toUInt(&ok, 16) & 0xFF;
        if (ok) {
            asciiString.append(letter);
        }
        else {
            qDebug() << QString("%1 %2, ASCII parse fail: %3 [%4(%5)]")
                .arg(__LINE__).arg(__FUNCTION__).arg(hexString).arg(byteString).arg(letter);
        }
    }
    return asciiString;
}

QString StringUtils::toHexString(const QString &asciiString, const QString &delimiter, int width, bool showPrefix)
{
    QString fmt = showPrefix ? "0x%1" : "%1";
    int fieldWidth = (width > 2) ? width : 2;

    QString hexString;
    foreach (QChar letter, asciiString) {
        ushort value = letter.unicode();
        if (value >= 0 && value <= 0x7F) {
            hexString.append(QString(fmt).arg(value, fieldWidth, 16, QChar('0')));
            hexString.append(delimiter);
        }
        else {
            qDebug() << QString("%1 %2, Hex parse fail: %3 [%4(%5)]")
                .arg(__LINE__).arg(__FUNCTION__).arg(asciiString).arg(letter).arg(value);
        }
    }
    int hexStringLen = hexString.length();
    int delimiterLen = delimiter.length();
    int subStringLen = (hexStringLen > delimiterLen) ? hexStringLen - delimiterLen : 0;
    return hexString.left(subStringLen);
}

QString StringUtils::toHexString(const QString &asciiString)
{
    return toHexString(asciiString, "", 2, false);
}

QString StringUtils::toHexString(const QString &asciiString, int width)
{
    return toHexString(asciiString, "", width, false);
}

QString StringUtils::toHexString(const QString &asciiString, const QString &delimiter)
{
    return toHexString(asciiString, delimiter, 2, false);
}

QString StringUtils::toHexString(const QString &asciiString, const QString &delimiter, int width)
{
    return toHexString(asciiString, delimiter, width, false);
}

const char* StringUtils::toCharArray(QString str)
{
    std::string s = str.toStdString();
    return s.c_str();
}

const wchar_t* StringUtils::toWCharArray(QString str)
{
    std::wstring ws = str.toStdWString();
    return ws.c_str();
}
