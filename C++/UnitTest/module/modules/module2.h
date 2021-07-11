#ifndef MODULE2_H
#define MODULE2_H

#include "libcommon.h"
#include <string>

#define SUCCESS_ "success"
#define FAILED_  "failed"
#define ERROR_   "fatal"

MODULE_API std::string RunFunc(int ret);

template <typename T1, typename T2>
bool GreaterThan(T1 x1, T2 x2) {
    return x1 > x2;
}

#endif  // MODULE2_H
