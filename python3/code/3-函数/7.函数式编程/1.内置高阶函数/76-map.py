# 1.准备列表数据
list1 = [1, 2, 3, 4, 5]


# 2.准备2次方计算的函数
def func(x):
    return x ** 2


# 3.调用map
result = map(func, list1)

# 4.数据转换
print(result)
print(type(result))  # map
print(list(result))  # list

# 将list中的所有数字转为字符串
print(list(map(str, list1)))
