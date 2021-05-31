def test1(a):  # 实参通过引用传递
    if isinstance(a, (int,)):
        print(a, id(a))
    else:
        print(a, '\t\t', id(a))

    a += a
    print(a, id(a))


# int: 计算前后id值不同
b = 100
print('   ', id(b))
test1(b)
print(b)

# list: 计算前后id值相同
c = [11, 22]
print('\t\t\t\t', id(c))
test1(c)
print(c)
