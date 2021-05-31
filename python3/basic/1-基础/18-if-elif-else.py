age = int(input('请输入年龄：'))
print(f'年龄{age}：', end='')

if age <= 0 or age > 100:
    print('输入不合法')

elif age >= 18:
    print('已成年，', end='')
    print('可以上网')

else:
    print('未成年，', end='')
    print('不能上网')


print('===========')  # 未缩进，不属于if条件分支
