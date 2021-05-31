def user_info(name, age, gender):
    print(f'您的名字：{name}, 年龄：{age}, 性别：{gender}')


user_info('Tom', 20, '男')

# user_info('Tom', 20)  # 少传递一个参数，error

user_info('Tom', '男', 20)  # 参数类型传递错误，不报错，却无意义
