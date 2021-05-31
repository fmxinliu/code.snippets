#
# 1.创建有数据的集合
#

# 集合中的元素是无序的
s1 = {10, 20, 30, 40, 50}
print(s1)

# 集合中的元素是不重复的，重复元素自动被过滤
s2 = {10, 30, 20, 40, 30, 20}
print(s2)

# set()创建集合,
s3 = set('abcdefg')
print(s3)

s4 = set([1, 1, 2, 2, 3, 4])
print(s4)


#
# 2.创建空集合
#

s5 = set()
print(f'{s5}, {type(s5)}')

dic = {}  # dict 空字典!!!!!
print(f'{dic}, {type(dic)}')
