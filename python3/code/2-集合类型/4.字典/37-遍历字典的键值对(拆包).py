dict1 = {'name': 'Tom', 'age': 20, 'gender': '男'}

# items() -- 可迭代对象
print(dict1.items())


# 1.遍历items()
for item in dict1.items():
    print(item)  # 每个元素是一个元组


# 2.4.可变与不可变类型，遍历key-value
for key, value in dict1.items():
    print(f'{key}={value}')
