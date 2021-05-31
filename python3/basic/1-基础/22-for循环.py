str1 = 'hello world! 你好'
for i in str1:
    print(i, end='')

# 恢复默认结束符：换行
print()

for i in [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]:
    if i == 10:
        break

    if i % 3 == 0:
        print(i)
    else:
        print(i, end=',')




#
# 2.不执行循环
#
print('============')
i = 1000
for i in range(1, 10):  # 无论给 i 赋什么值，都不影响循环 !!!
    print(i, end=',')
    i += 10  # 不使用跳出语句，只修改 i，无法退出循环 !!!
