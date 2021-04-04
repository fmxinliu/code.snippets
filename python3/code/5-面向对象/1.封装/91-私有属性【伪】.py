# 1.私有属性：属性名前加__
# 2.实现方式：解释器将__XXX属性名，转换为：_YYY__XXX (YYY为类名)。因此，可通过转换后的名字访问


class Person:
    __money = 10000

    def get_money(self):
        return self.__money


person = Person()
# print(person.__money)  # 无法直接访问

# 尝试修改
person.__money = 999
print(person.__money)
print(person.get_money())  # 修改失败了，新增了一个属性__money

# 正确修改方式
person._Person__money = -404  # 修改(伪)私有属性
print(person.get_money())
