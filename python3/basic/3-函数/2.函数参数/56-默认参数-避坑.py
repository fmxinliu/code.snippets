"""
   (1)挖坑

# 1.Python函数在定义的时候，默认参数L的值就被计算出来了，即[]
# 2.默认参数L也是一个变量，它指向对象[]，
# 3.每次调用该函数，如果改变了L的内容，则下次调用时，默认参数的内容就变了，不再是函数定义时的[]了
"""


def add_end(list1=[]):  # !!!
    list1.append('END')
    return list1


L1 = add_end([1, 2, 3])  # [1, 2, 3, 'END']
print(L1)

L2 = add_end(L1)  # [1, 2, 3, 'END', 'END']
print(L2)

"""
    (2)踩坑
"""
L3 = add_end()  # ['END']
print(L3)

L4 = add_end()  # ['END', 'END'] !!!!!!!!!!!
print(L4)

"""
    (3)避坑
"""


def add_end(list1=None):  # 定义默认参数要牢记一点：默认参数必须指向不变对象！
    if list1 is None:
        list1 = []
    list1.append('END')
    return list1


L5 = add_end()  # ['END']
print(L5)

L6 = add_end()  # ['END']
print(L6)
