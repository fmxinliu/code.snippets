#include <iostream>
#include <string>
#include <cctype>
#include <algorithm>
#include "label.h"

#pragma comment(lib, "WidgetLib")


int main(int argc, char *argv[])
{
    Label label("¿Ø¼þ" + Widget::version());
    cout << label.text() << endl;

    // ÊÇ·ñÔÝÍ£
    for (int i = 1; i < argc; i++) {
        string s = argv[i];
        transform(s.begin(), s.end(), s.begin(), ::tolower);
        if (s == "nopause") {
            return 0;
        }
    }

    system("pause");
    return 0;
}
