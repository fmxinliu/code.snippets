#
# 列表是可变的
#

name_list = ['Tom', 'Lily', 'Rose']

print(name_list)

# len()列表长度
print(len(name_list))

# 下标遍历列表元素
print(name_list[0])
print(name_list[1])
print(name_list[2])

print(name_list[-3])    # 0
print(name_list[-2])    # 1
print(name_list[-1])    # 2

#
# 元素是否在列表中
#
print('Tom' in name_list)  # True
print('Tim' in name_list)  # False
print('Tom' not in name_list)  # False
print('Tim' not in name_list)  # True

#
# 列表允许混合元素
#
name_list = ['AAA', 3, 2, 5, 4, 6]
name_list = ['AAA', 3, 2, ['小明', 23], 5, 4, 6]

print(name_list)
print(name_list[3])  # ['小明', 23]
print(name_list[3][0])  # 小明
