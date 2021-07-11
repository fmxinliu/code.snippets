﻿#include "module4.h"


// Returns n! (the factorial of n).  For negative n, n! is defined to be 1.
static int Factorial(int n) {
    int result = 1;
    for (int i = 1; i <= n; i++) {
        result *= i;
    }

    return result;
}

// Returns true iff n is a prime number.
static bool IsPrime(int n) {
    // Trivial case 1: small numbers
    if (n <= 1) return false;

    // Trivial case 2: even numbers
    if (n % 2 == 0) return n == 2;

    // Now, we have that n is odd and n >= 3.

    // Try to divide n by every odd number i, starting from 3
    for (int i = 3; ; i += 2) {
        // We only have to try i up to the squre root of n
        if (i > n/i) break;

        // Now, we have i <= n/i < n.
        // If n is divisible by i, n is not prime.
        if (n % i == 0) return false;
    }

    // n has no integer factor in the range (1, n), and thus is prime.
    return true;
}

// Returns ture iff n is even.
static bool IsEven(int n) {
    return n % 2 == 0;
}

int Utils::Factorial(int n) {
    return ::Factorial(n);
}

bool Utils::IsPrime(int n) {
    return ::IsPrime(n);
}

bool Utils::IsEven(int n) {
    return ::IsEven(n);
}
