name = 'Tom'
percent = 20.4

print('我的名字叫%s' % name)
print(f'我的名字叫{name}')

# 输出 %
print('成绩提高%.1f%%' % percent)
print(f'成绩提高{percent}%')
print(f'成绩提高{percent:.1f}%')

# format()
print('我的名字叫{0}，成绩提高{1}%'.format(name, percent))
