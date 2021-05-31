########################################
# break 跳出 while 循环，不会执行 else 块
########################################


#
# 1.执行循环，正常退出
#
i = 1
while i <= 5:
    print('1.媳妇，我错了')
    i += 1
else:
    print('1.原谅你了')  # 会执行


#
# 2.不执行循环
#
i = 100
while i <= 5:
    print('2.媳妇，我错了')
    i += 1
else:
    print('2.原谅你了')  # 会执行


#
# 3.break 跳出循环
#
i = 1
while i <= 5:
    if i == 5:
        print('3.态度不真诚')
        break
    print('3.媳妇，我错了')
    i += 1
else:
    print('3.原谅你了')  # 不会执行


# 4.continue 跳过循环
i = 1
while i <= 5:
    if i == 5:
        print('4.这遍说的态度不够真诚')
        i += 1
        continue
    print('4.媳妇，我错了')
    i += 1
else:
    print('4.原谅你了')  # 会执行
