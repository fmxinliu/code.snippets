def fact(n):
    """求阶乘（尾递归）"""
    return fact_iter(n, 1)


def fact_iter(n, product):
    if n == 1:
        return product
    return fact_iter(n - 1, n * product)


"""
===> fact_iter(5, 1)
===> fact_iter(4, 5)
===> fact_iter(3, 20)
===> fact_iter(2, 60)
===> fact_iter(1, 120)
===> 120
"""
print(fact(5))
