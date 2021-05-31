age = 18
name = 'TOM'
weight = 75.5
stu_id = 1

print('今年我的年龄是%d岁' % age)
print('我的名字是%s' % name)
print('我的体重是%.2f公斤' % weight)
print('我的学号是%03d' % stu_id)
print('我的名字是%s，今年%d岁了' % (name, age))
print('我的名字是%s，明年%d岁了' % (name, age + 1))
print('我的名字是%s，年龄%d，体重%.1fkg，学号%06d' % (name, age, weight, stu_id))

print(name, age, weight)
