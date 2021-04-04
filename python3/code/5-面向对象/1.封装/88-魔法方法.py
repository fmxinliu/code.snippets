# 0. self       : 类似于 this
# 1. __init__() : 类似于构造函数，但不能被重载
# 2. __str__()  : 类似于toString
# 3. __del__()  : 类似于析构函数


class Student:
    def __init__(self, name, age):  # 带参数的初始化函数
        self.name = name
        self.age = age

    def __str__(self):  # print输出对象时，调用的函数
        return f'name={self.name}, age={self.age}'

    def __del__(self):  # 手动/自动删除对象时，默认调用
        print('对象已经被删除')


stu = Student('Tom', 21)
print(stu)
del stu
