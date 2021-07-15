#ifndef LABEL_V2X_H
#define LABEL_V2X_H

#include "widget.h"

class LabelPrivate;
class MODULE_API Label : public Widget
{
public:
    Label();
    Label(std::string text);
    ~Label();

    string text() const;

private:
    LabelPrivate *d_ptr;  // d ÷∏’Î
};


#endif // LABEL_V2X_H
