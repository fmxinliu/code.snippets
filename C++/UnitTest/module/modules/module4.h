#ifndef MODULE4_H
#define MODULE4_H

#include "libcommon.h"

class MODULE_API Utils {
public:
    // Returns n! (the factorial of n).  For negative n, n! is defined to be 1.
    int Factorial(int n);

    // Returns true iff n is a prime number.
    bool IsPrime(int n);

    // Returns ture iff n is even.
    bool IsEven(int n);

    bool IsSuc(bool b) {
        return b;
    }
};

#endif  // MODULE4_H
