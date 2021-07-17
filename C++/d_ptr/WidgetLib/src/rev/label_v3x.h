#ifndef LABEL_V3X_H
#define LABEL_V3X_H

#include "widget.h"

class LabelPrivate;
class MODULE_API Label : public Widget
{
public:
    Label();
    Label(std::string text);
    ~Label();

    string text() const;

    void repaint();
    void refresh();

private:
    Label(const Label &);
    Label & operator=(const Label &);
};

#endif // LABEL_V3X_H
