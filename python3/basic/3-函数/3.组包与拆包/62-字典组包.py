def user_info(**kwargs):
    print(kwargs)
    print(type(kwargs))  # dict类型
    print(kwargs.__len__())  # 元素个数


user_info(name='Tom', age=20)
