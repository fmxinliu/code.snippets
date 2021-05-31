# 实例方法：包含一个参数(通常命名self)，python会自动将【调用此方法的实例对象】绑定给cls参数


class Dog:
    def __init__(self):
        self.__name = '旺财'

    def get_name(self):
        return self.__name


dog = Dog()
print(dog.get_name())
