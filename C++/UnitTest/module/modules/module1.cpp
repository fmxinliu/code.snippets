#include "module1.h"
#include <cmath>

// 求最大公约数
int Foo(int a, int b)
{
    if (a == 0 || b == 0)
        return 0;

    int c = a % b;
    if (c == 0)
        return b;
    return Foo(b, c);
}

// 四舍五入
double Round_1(double num, int fig)
{
    int shift = static_cast<int>(pow(10.0, fig));
    double half = (num >= 0) ? 0.5f : -0.5f;
    return int(num * shift + half) / double(shift);
}

// 四舍五入
double Round_2(double num, int fig)
{
    int shift = static_cast<int>(pow(10.0, fig));
    double half = (num >= 0) ? 0.5f : -0.5f;
    num = num * shift;

    double num1 = num - floor(num);
    double num2 = ceil(num) - num;

    if (num1 > num2)
        return ceil(num) / shift;
    else
        return floor(num) / shift;
}

// 抛出异常
void ThrowException(int n)
{
    switch (n)
    {
    case 0:
        throw 0;
    case 1:
        throw "error";
    case 2:
        throw 1.1f;
    case 3:
        return;
    }
}