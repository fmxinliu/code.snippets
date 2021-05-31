a = 100
b = 100


def testA():
    a = 200  # 声明局部变量
    print('testA:  a=%d, b=%d' % (a, b))


def testB():
    global b  # 声明全局变量
    b = 200
    print('testB:  a=%d, b=%d' % (a, b))


testA()
testB()
print('\t\ta=%d, b=%d' % (a, b))
