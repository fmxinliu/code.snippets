// stdafx.h : ��׼ϵͳ�����ļ��İ����ļ���
// ���Ǿ���ʹ�õ��������ĵ�
// �ض�����Ŀ�İ����ļ�
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             //  �� Windows ͷ�ļ����ų�����ʹ�õ���Ϣ
// Windows ͷ�ļ�:
#include <windows.h>



// TODO: �ڴ˴����ó�����Ҫ������ͷ�ļ�

// __cdecl: ����������Ϊ Add
// __stdcall: ����������Ϊ _Add@8
// ʹ�� mydef.dll������������ΪAdd
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
