s = set()

s.add((1, 2))
print(s)

s.update([(1, 2)])
print(s)


s.add((3, (4, 5)))
print(s)

s.update((3, (4, 5)))
print(s)

"""
    set和dict:
    1. 区别：set没有存储对应的value
    2. 原理：不可以放入可变对象，因为无法判断两个可变对象是否相等，也就无法保证内部“不会有重复元素”
            因此，无法放入键值为list的对象

"""
# 1.无法放入包含list的元素
# s.add((6, [7, 8]))  # 报错
# s.update((6, [7, 8]))  # 报错


# 2.放入set
s.update({6, 7, 8})
print(s)


# 3.放入dict -- 只放入key
s.update({11: '11', '22': 22})
print(s)
