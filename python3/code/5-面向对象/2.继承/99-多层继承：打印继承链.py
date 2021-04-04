from __基类__ import Father, Mother


class Son(Father, Mother):
    pass


class GrandSon(Son):
    pass


print(GrandSon.__mro__)
