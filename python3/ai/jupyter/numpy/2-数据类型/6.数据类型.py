import numpy as np

# 1.标量类型
dt = np.dtype(np.int8)
a = np.array([10, 20, 30], dtype=dt)
print(a, a.dtype)


# 2.结构化数据类型
dt = np.dtype([('age', np.int8)])
a = np.array([(10,), (20,), (30,)], dtype=dt)
print(a)
print(a['age'])  # 类型字段名可以用于存取实际的 age 列


# 3.结构化数据类型student, 包含字段: name(str), age(int), marks(float)
student = np.dtype([('name', 'S20'), ('age', 'i1'), ('marks', 'f4')])
a = np.array([('abc', 21, 50), ('xyz', 18, 75)], dtype=student)
print(a)


# 4.自定义类型
a = np.zeros((2, 2), dtype=[('x', 'i4'), ('y', 'i4'), ('z', 'i4')])
print(a)
