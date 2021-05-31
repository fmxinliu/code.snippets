# 需求：不修改函数定义，在函数调用前后自动打印日志

import functools


# 1.无参数 decorator
def log(func):
    @functools.wraps(func)  # 修改wrapper函数名字，等价于：wrapper.__name__ = func.__name__
    def wrapper(*args, **kwargs):
        print('begin call %s()' % func.__name__)
        r = func(*args, **kwargs)
        print('end call %s()' % func.__name__)
        return r

    return wrapper


@log
def show_time(h, m, s):
    print(f'{h}:{m}:{s}')


# @log 相当于:
# show_time = log(show_time)
# print(show_time.__name__)

show_time(23, 12, 1)
# print(show_time.__name__)


# 2.有参数 decorator
def log(text):
    def decorator(func):
        @functools.wraps(func)
        def wrapper(*args, **kwargs):
            print('%s: begin call %s()' % (text, func.__name__))
            r = func(*args, **kwargs)
            print('%s: end call %s()' % (text, func.__name__))
            return r

        return wrapper

    return decorator


@log('DEBUG')
def show_time(h, m, s):
    print(f'{h}:{m}:{s}')


# @log('DEBUG') 相当于:
# show_time = log('DEBUG')(show_time)
# print(show_time.__name__)

show_time(23, 31, 44)
# print(show_time.__name__)
