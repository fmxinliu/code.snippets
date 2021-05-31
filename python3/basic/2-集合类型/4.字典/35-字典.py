# 1.有数据的字典
dict1 = {'name': 'Tom', 'age': 20, 'gender': '男'}
print(dict1)

# 2.空字典
dict2 = {}
print(dict2)

dict3 = dict()
print(dict3)

# 3.添加key-value：当key不存在时
dict3['name'] = 'Tom'
print(dict3)

# 4.修改key-value：当key存在时
dict3['name'] = 'Tom2'
print(dict3)

# 通过key获取value
print(dict1['name'])

# 1.in
# print(dict1['id'])  # key不存在，报错
if 'id' in dict1:
    print(dict1['id'])
else:
    print('id不存在')

# 2.get(key)
print(dict1.get('id'))  # None
if dict1.get('id'):
    print(dict1['id'])
else:
    print('id不存在')
