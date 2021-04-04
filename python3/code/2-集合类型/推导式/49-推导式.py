#
# 推导式的作用：简化代码
#

"""元组不支持推导式"""

# 1.列表推导式
list1 = [i for i in range(10)]
print(list1)

# 带if的列推导式
list2 = [i for i in range(0, 10, 2)]
print(list2)

list3 = [i for i in range(10) if i % 2 == 0]
print(list3)

# 多个for循环实现列表推导式
list4 = [(i, j) for i in range(1, 3) for j in range(3)]
print(list4)

# 2.字典推导式
dict1 = {i: i * i for i in range(5)}
print(dict1)

# 将两个列表合并为一个字典
list5 = ['name', 'age', 'gender']
list6 = ['Tom', 20, 'man', 1]
dict2 = {list5[i]: list6[i] for i in range(min(len(list5), len(list6)))}
print(dict2)

# 提取字典中目标数据
counts = {'MBP': 268, 'HP': 125, 'DELL': 201, 'Lenovo': 199, 'acer': 99}
count1 = {key: value for key, value in counts.items() if value >= 200}
print(count1)

# 3.集合推导式
list7 = [1, 1, 2]
set1 = {i ** 3 for i in list7}
print(set1)
