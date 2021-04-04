# 类方法：使用装饰器＠classmethod修饰，包含一个参数(通常命名cls)，python会自动将【类本身】绑定给cls参数


class Dog:
    __type = '狗子'

    # 使用场景：方法中需要使用类对象(如：访问私有类属性)
    @classmethod
    def get_type(cls):
        return cls.__type


dog = Dog()
print(dog.get_type())  # 不推荐
print(Dog.get_type())  # 推荐【类名】访问
