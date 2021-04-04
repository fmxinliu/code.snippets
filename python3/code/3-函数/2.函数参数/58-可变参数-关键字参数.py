def user_info(**kwargs):
    print(kwargs)
    # print(type(kwargs))  # dict类型
    # print(kwargs.__len__())  # 元素个数


user_info()
user_info(name='Tom')
user_info(name='Tom', age=20)
user_info(name='Tom', age=20, gender='男')

#
extra = {'city': 'Beijing', 'job': 'Engineer'}
user_info(city=extra['city'], job=extra['job'])
user_info(**extra)  # **extra表示把extra这个dict的所有key-value用关键字参数传入到函数的**kwargs参数


def person(**kwargs):
    # print(id(kwargs))
    if 'city' in kwargs:
        kwargs['city'] = 'Taiyuan'  # kwargs获得的dict是extra的一份拷贝，对kwargs的改动不会影响到函数外的extra
    print(kwargs)


# print(id(extra))
person(**extra)
print(extra)

