// plugin.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

//#pragma comment(lib, "D:\\Repo\\Project\\code.snippets\\C#\\Reflection\\构建可热拔插应用\\综合DLL\\应用\\bin\\Debug\\plugins\\libhello.lib")
//extern "C" void __stdcall sayHello();

int Add(int plus1, int plus2)
{
    //sayHello();
    return plus1 + plus2;
}
