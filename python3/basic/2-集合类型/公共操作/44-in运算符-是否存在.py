str1 = 'abcd'
list1 = ['hello', 'world']
t1 = ('world',)
dict1 = {'name': 'Tome'}
set1 = {1, 2, 3, 4}

print('a' in str1)
print('hello' in list1)
print('world' in t1)

# 字典
print('name' in dict1)
print('name' not in dict1)
print('name' in dict1.keys())  # True
print('name' in dict1.items())  # False
print(('name', 'Tome') in dict1.items())  # True

# 2-集合类型
print(1 not in set1)
