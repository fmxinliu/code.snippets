def person(name, age, **kw):
    if 'city' in kw:
        # 有city参数
        pass
    if 'job' in kw:
        # 有job参数
        pass
    print('name:', name, 'age:', age, 'other:', kw)


# Q：我们希望检查是否有city和job参数，但是调用者仍可以传入不受限制的关键字参数
person('Jack', 24, city='Beijing', job='Engineer', zipcode=123456)


# A：如果要限制传入的关键字参数的名字，可以使用命名关键字参数：
def person(name, age, *, city, job):  # *后面的参数被视为命名关键字参数
    print(name, age, city, job)


person('Jack', 24, city='Beijing', job='Engineer')


# person('Jack', 24, city='Beijing', job='Engineer', zipcode=123456)  # 只接收city和job作为关键字参数
# person('Jack', 24, 'Beijing', 'Engineer') # 命名关键字参数必须传入参数名， 如果没有传入参数名，调用将报错


# 1.如果函数定义中已经有了一个可变位置参数，后面跟着的命名关键字参数就不再需要一个特殊分隔符*了：
def person(name, age, *args, city, job):
    print(name, age, args, city, job)


person('Jack', 24, city='Beijing', job='Engineer')


# 2.命名关键字参数可以有缺省值，从而简化调用：
def person(name, age, *, city='Beijing', job):  # 有默认值的命名关键字参数，不必是最后一个参数
    print(name, age, city, job)


person('Jack', 24, job='Engineer')


# 3.缺少 *，city和job被视为位置参数
def person(name, age, city, job):
    pass
