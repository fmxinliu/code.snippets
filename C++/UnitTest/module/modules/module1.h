#ifndef MODULE1_H
#define MODULE1_H

#include "libcommon.h"

MODULE_API int Foo(int a, int b);

MODULE_API double Round_1(double num, int fig);
MODULE_API double Round_2(double num, int fig);

MODULE_API void ThrowException(int);


#endif  // MODULE1_H
