#
# 1.新增数据
#

s1 = {10, 20}

# 1.1 add() -- 增加单个元素
s1.add(100)  # 无序性
print(s1)

s1.add(100)  # 去重性
print(s1)

# s1.add([10, 20, 30])  # 报错

# 1.2 update() -- 增加序列
s1.update([10, 20, 30])
print(s1)

# s1.update(200)    # 报错


#
# 2.删除数据
#

# 2.1 remove() -- 数据不存在，报错
s1.remove(10)
print(s1)
# s1.remove(10)  # 报错

# 2.2 discard() -- 数据不存在，不报错
s1.discard(20)
print(s1)
s1.discard(20)  # 不报错

# 2.3 pop() -- 随机删除某个数据，并返回这个数据
s1 = {10, 20, 30, 40, 50}
print(s1.pop())
print(s1)


#
# 3.查找数据
#

s2 = {10, 20, 30, 40, 50}
print(10 in s2)
print(10 not in s2)
