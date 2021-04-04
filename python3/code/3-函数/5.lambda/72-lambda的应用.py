# 1.带判断的lambda
fn1 = lambda a, b: a if a > b else b
print(fn1(1, 2))
print(fn1(2, 1))

# 2.列表数据按字典key的值排序
students = [
    {'name': 'TOM', 'age': 20},
    {'name': 'ROSE', 'age': 19},
    {'name': 'Jack', 'age': 22}
]

# 按name值升序排列
students.sort(key=lambda x: x['name'])
print(students)

# 按name值降序排列
students.sort(key=lambda x: x['name'], reverse=True)
print(students)

# 按age值升序排列
students.sort(key=lambda x: x['age'])
print(students)
