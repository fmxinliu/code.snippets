#
# 列表是可变的，元组是不可变的列表
#


t = (10, 20, 30)
print(t)
print(t[-1])

# 1.多个数据元组
t1 = (10, 20, 30)
print(f'{t1}, {type(t1)}')

# 2.单个数据元组
t2 = (10,)
print(f'{t2}, {type(t2)}')

# !!!!!
t3 = (10)
print(f'{t3}, {type(t3)}')  # int

t4 = ('aaa')
print(f'{t4}, {type(t4)}')  # str

# 3，空tuple
t5 = ()
print(f'{t5}, {len(t5)}')
