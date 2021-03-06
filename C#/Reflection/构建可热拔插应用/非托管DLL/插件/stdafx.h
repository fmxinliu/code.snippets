// stdafx.h : 标准系统包含文件的包含文件，
// 或是经常使用但不常更改的
// 特定于项目的包含文件
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             //  从 Windows 头文件中排除极少使用的信息
// Windows 头文件:
#include <windows.h>



// TODO: 在此处引用程序需要的其他头文件

// __cdecl: 导出函数名为 Add
// __stdcall: 导出函数名为 _Add@8
// 使用 mydef.dll导出，函数名为Add
//#define DLL_CALLINGCONVENTION __stdcall

//#define DLL_EXPORTS
//#  if defined (DLL_EXPORTS)
//#    define DLL_API extern "C" __declspec(dllexport)
//#  elif defined (DLL_EXPORTS_USE_DEF_FILE)
//#    define DLL_API
//#  else
//#    define DLL_API extern "C" __declspec(dllimport)
//#  endif
//
//DLL_API int DLL_CALLINGCONVENTION Add(int plus1, int plus2);

//You can also write like this:
//extern "C" {
//    _declspec(dllexport) int Add(int plus1, int plus2);
//};
