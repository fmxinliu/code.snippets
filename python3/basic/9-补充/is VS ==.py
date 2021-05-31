"""
1.is用于判断两个变量引用对象是否为同一个，类似 id(x) == id(y)
2.== 用于判断引用变量的值是否相等
"""

a, b = 10, 10
print(a is b)  # True 引用相等
print(a == b)  # True 值相等


lst1 = [1, 2, 3]
lst2 = [1, 2, 3]
print(lst1 is lst2)  # False
print(lst1 == lst2)  # True


lst1 = [1, 2, 3]
lst2 = lst1
print(lst1 is lst2)  # True
print(lst1 == lst2)  # True
