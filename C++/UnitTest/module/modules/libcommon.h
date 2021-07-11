#ifndef LIBCOMMON_H
#define LIBCOMMON_H

//////////////////////////////////////////////////////////////////////////
// DLL导出: 如果接口、类中包含C++ STL，请使用指针形式 or 类似Qt的 d 指针形式
//////////////////////////////////////////////////////////////////////////
#ifdef _MSC_VER

# if MODULE_LINKED_AS_SHARED_LIBRARY
#  define MODULE_API __declspec(dllimport)
# elif MODULE_CREATE_SHARED_LIBRARY 
#  define MODULE_API __declspec(dllexport)
# else
# define MODULE_API
# endif


// gtest库引用方式
# if MODULE_LINKED_AS_SHARED_LIBRARY
#  define GTEST_LINKED_AS_SHARED_LIBRARY 1
# else
#  define GTEST_LINKED_AS_SHARED_LIBRARY 0
# endif


#endif  // _MSC_VER

#endif  // LIBCOMMON_H