"""
1.type()不会认为子类是一种父类类型
2.isinstance()会认为子类是一种父类类型
"""


class A:
    pass


class B(A):
    pass


objA = A()
objB = B()

print(type(objA) == A)      # True
print(isinstance(objA, A))  # True

print(type(objB) == A)      # False
print(isinstance(objB, A))  # True
