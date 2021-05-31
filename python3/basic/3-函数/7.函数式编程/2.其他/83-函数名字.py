# 获取函数名字


def now(y, m, d):
    def show_date():
        print(f'{y}-{m}-{d}')

    return show_date


f1 = now
print(f1.__name__)  # 'now'

f2 = now(2021, 3, 25)
print(f2.__name__)  # 'show_date'
