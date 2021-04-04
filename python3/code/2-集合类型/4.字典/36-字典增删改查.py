# 1.新增、修改：如果key存在则修改值；如果key不存在则新增键值对
dict1 = {'name': 'Tom', 'age': 20, 'gender': '男'}
print(dict1)

dict1['name'] = 'Rose'  # 修改
print(dict1)

dict1['id'] = 110  # 新增
print(dict1)

# 2.删除 del/del()
# 2.1 删键值对
del dict1['id']
print(dict1)

# 2.2 删字典
# del dict1
# print(dict1)

# 2.3 清空
dict1.clear()
print(dict1)

# 3.查找
# 3.1 get(key, 默认值)
dict2 = {'name': 'Tom', 'age': 20, 'gender': '男'}
print(dict2.get('name'))  # Tom
print(dict2.get('id'))  # None
print(dict2.get('id', -1))  # -1

# 3.2 keys()
print(dict2.keys())

# 3.3 values()
print(dict2.values())

# 3.4 items()
print(dict2.items())
