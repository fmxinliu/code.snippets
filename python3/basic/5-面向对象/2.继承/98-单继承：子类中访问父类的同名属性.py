from __基类__ import Father


class Child(Father):
    def __init__(self):
        self.skill = '互联网电商'

    def show_skill(self):
        print(f'拥有的技能：{self.skill}')

    # 方法1:
    # 优点：可以用于多继承，多个父类存在同名属性的情况
    # 缺点：必须指定父类名，存在“硬编码”
    def show_father_skill_1(self):
        Father.__init__(self)
        Father.show_skill(self)

    # 方法2:
    # 优点：子类不必知道确切的父类
    # 缺点：不能用于多继承，多个父类存在同名属性的情况
    def show_father_skill_2(self):
        super(Child, self).__init__()
        super(Child, self).show_skill()

    def show_father_skill_2_brief(self):
        super().__init__()
        super().show_skill()


child = Child()
child.show_skill()  # 调用：自定义的属性
child.show_father_skill_1()  # 调用：Father的属性
child.show_father_skill_2()  # 调用：Father的属性(完整写法：super(当前类名, self))
child.show_father_skill_2_brief()  # 调用：Father的属性(精简写法：super())
