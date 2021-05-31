"""
  all列表：（只对 from xxx import * 导入方式有效）
1.只能导入all列表指定的功能
2.如果没有定义all列表，则导入全部功能
"""

__all__ = ['testA']


def testA():
    print('testA')


def testB():
    print('testB')
