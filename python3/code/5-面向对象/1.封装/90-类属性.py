# 类属性：归属于类对象，为所有实例对象所共有


class Dog:
    type = '狗子'


wangcai = Dog()
xiaohei = Dog()

# 访问类属性
print(Dog.type)  # 通过类对象访问
print(wangcai.type)  # 通过实例对象访问
print(xiaohei.type)

# 修改类属性
Dog.type = '哈士奇'  # 通过类对象修改
print(Dog.type, wangcai.type, xiaohei.type)

wangcai.type = '金毛'  # 通过实例对象修改，类属性修改失败!!!!!!!!!!!
print(Dog.type, wangcai.type, xiaohei.type)
