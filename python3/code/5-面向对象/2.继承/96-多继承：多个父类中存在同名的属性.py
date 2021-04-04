from __基类__ import Father, Mother


class Child(Father, Mother):  # 如果2个父类中有同名属性，只能继承第一个父类的属性
    pass


child = Child()
child.show_skill()  # 只继承了Father的技能
