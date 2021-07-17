#ifndef GLOBAL_H
#define GLOBAL_H

// qGetPtrHelper(ptr) 触发ptr(智能)指针的隐式转换
template <typename T> static inline T *qGetPtrHelper(T *ptr) { return ptr; }
template <typename Wrapper> static inline typename Wrapper::pointer qGetPtrHelper(const Wrapper &p) { return p.data(); }

#define QT_DECLARE_PRIVATE(Class, func, ptr) \
    inline Class* func() { return reinterpret_cast<Class *>(qGetPtrHelper(ptr)); } \
    inline const Class* func() const { return reinterpret_cast<const Class *>(qGetPtrHelper(ptr)); } \
    friend class Class;

#define D_DECLARE_PRIVATE(Class) \
protected: \
    QT_DECLARE_PRIVATE(Class##Private, d_func, d_ptr) \
    Class(Class##Private &d); \
private: \
    Class##Private* d_ptr; \

#define Q_DECLARE_PRIVATE(Class) \
protected: \
    QT_DECLARE_PRIVATE(Class, q_func, q_ptr) \
private: \
    Class* q_ptr;

#define D_PTR(Class) Class##Private * const d = const_cast<Class##Private *>(reinterpret_cast<const Class##Private *>(d_func()))
#define Q_PTR(Class) Class * const q = const_cast<Class *>(reinterpret_cast<const Class *>(q_func()))

#define QT_DISABLE_COPY(Class) \
    Class(const Class &);\
    Class &operator=(const Class &);

#endif // GLOBAL_H
