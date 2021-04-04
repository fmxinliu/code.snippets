# ord() 获取(单个)字符的整数表示
print(ord('A'))

# chr() 把编码转换为对应的(单个)字符
print(chr(65))

#
# 区分: 'ABC' 和 b'ABC'
#
print('ABC')    # str类型，每个字符占若干个字节
print(b'ABC')   # bytes类型，每个字符占一个字节


# str -> bytes
print('ABC'.encode('ascii'))    # 纯英文: ASCII编码
print('中文'.encode('utf-8'))   # 含中文: UTF-8编码


# bytes -> str
print(b'ABC'.decode('ascii'))
print(b'\xe4\xb8\xad\xe6\x96\x87'.decode('utf-8'))


#
# 区分：“字符串包含的字符个数” 和 “字符串所占的字节数”
#
print(len('中文'))                    # 包含 2 个字符
print(len('中文'.encode('utf-8')))    # 占 6 个字节（1个中文字符经过UTF-8编码后通常会占用3个字节）
