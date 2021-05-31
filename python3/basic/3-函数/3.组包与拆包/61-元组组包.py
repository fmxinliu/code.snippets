def user_info(*args):
    print(args)
    print(type(args))  # tuple类型
    print(args.__len__())  # 元素个数


user_info('Tom', 20)
