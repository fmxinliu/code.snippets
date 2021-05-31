L = [36, 5, -12, 9, -21]
print(sorted(L))  # 升序
print(sorted(L, key=abs))  # 升序
print(sorted(L, key=abs, reverse=True))  # 降序

# 按照字母序排序
L = ['bob', 'about', 'Zoo', 'Credit']
print(sorted(L))
print(sorted(L, key=str.lower))  # 忽略大小写
print(sorted(L, key=str.lower, reverse=True))  # 忽略大小写

L = [('Bob', 75), ('Adam', 92), ('Bart', 66), ('Lisa', 88)]


def by_name(t):
    name, age = t
    return name


def by_age(t):
    name, age = t
    return age


print(sorted(L, key=by_name))  # 按名字排序
print(sorted(L, key=by_age, reverse=True))  # 按年龄降序排列
