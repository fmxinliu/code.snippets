def user_info(name, age, gender='男'):  # 默认参数必须位于位置参数之后
    print(f'您的名字：{name}, 年龄：{age}, 性别：{gender}')


user_info('Tom', 20)

user_info('Lucy', 26, '女')
