# 静态方法：使用装饰器＠staticmethod修饰，python不会对它做任何类或对象的绑定。因此，无法直接访问任何属性和方法


class Dog:
    __type = '狗子'

    @staticmethod
    def get_type():
        return Dog.__type  # 必须通过这种方式引用


dog = Dog()
print(dog.get_type())
print(Dog.get_type())
