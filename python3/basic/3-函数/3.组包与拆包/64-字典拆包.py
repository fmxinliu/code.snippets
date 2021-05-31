dict1 = {'name': 'Tom', 'age': 20, 'gender': '男'}

a, b, c = dict1

# 对字典拆包，取出来的是字典的key
print(a)  # name
print(b)  # age
print(c)  # gender

print(dict1[a])  # Tom
print(dict1[b])  # 20
print(dict1[c])  # 男
