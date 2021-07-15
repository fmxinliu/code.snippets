#ifndef LABEL_V1X_H
#define LABEL_V1X_H

#include "widget.h"

class MODULE_API Label : public Widget
{
public:
    Label();
    Label(std::string text);
    ~Label();

    string text() const;

private:
    string m_text;
};


#endif // LABEL_V1X_H
