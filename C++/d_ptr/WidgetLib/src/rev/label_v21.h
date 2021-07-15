#ifndef LABEL_V21_H
#define LABEL_V21_H

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
    LabelPrivate *d_ptr;
};

#endif // LABEL_V21_H
