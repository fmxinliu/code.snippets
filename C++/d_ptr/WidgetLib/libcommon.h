#ifndef LIBCOMMON_H
#define LIBCOMMON_H

//////////////////////////////////////////////////////////////////////////
// DLL����: ����ӿڡ����а���C++ STL����ʹ��ָ����ʽ or ����Qt�� d ָ����ʽ
//////////////////////////////////////////////////////////////////////////
#ifdef _MSC_VER

# if MODULE_LINKED_AS_SHARED_LIBRARY
#  define MODULE_API __declspec(dllimport)
# elif MODULE_CREATE_SHARED_LIBRARY 
#  define MODULE_API __declspec(dllexport)
# else
# define MODULE_API
# endif


// gtest�����÷�ʽ
# if MODULE_LINKED_AS_SHARED_LIBRARY
#  define GTEST_LINKED_AS_SHARED_LIBRARY 1
# else
#  define GTEST_LINKED_AS_SHARED_LIBRARY 0
# endif


#endif  // _MSC_VER

#endif  // LIBCOMMON_H