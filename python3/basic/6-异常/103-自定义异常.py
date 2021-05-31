class ShortInputError(Exception):               # 1.自定义异常类
    def __init__(self, length, min_len):
        self.length = length
        self.min_len = min_len

    def __str__(self):
        return f'ShortInputError: 你输入的密码长度是{self.length}，不能少于{self.min_len}个字符'


def input_password():
    password = input('请输入密码：')
    length = len(password)
    if length < 3:
        raise ShortInputError(length, 3)        # 2.抛出异常
    print('输入完成')


def test():
    try:
        input_password()
    except ShortInputError as e:                # 3.捕获异常
        print(e)


test()
