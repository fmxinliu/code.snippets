def user_info(*args):
    print(args)
    # print(type(args))  # tuple类型
    # print(args.__len__())  # 元素个数


user_info()
user_info('Tom')
user_info('Tom', 20)
user_info('Tom', 20, '男')


# 1
def calc(numbers):
    sum1 = 0
    for n in numbers:
        sum1 += n
    return sum1


nums = [1, 2, 3]
print(calc([]), end=',')
print(calc(nums))


# 2
def calc(*numbers):
    sum1 = 0
    for n in numbers:
        sum1 += n
    return sum1


print(calc(), end=',')
print(calc(1, 2, 3), end=',')
print(calc(nums[0], nums[1], nums[2]), end=',')
print(calc(*nums))  # *nums - 表示把nums这个list的所有元素作为可变参数传进去。
