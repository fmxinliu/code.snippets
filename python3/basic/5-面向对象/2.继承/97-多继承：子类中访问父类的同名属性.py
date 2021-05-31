from __基类__ import Father, Mother


class Child(Father, Mother):
    def __init__(self):
        self.skill = '互联网电商'

    def show_self_skill(self):
        self.__init__()
        print(f'拥有的技能：{self.skill}')

    def show_father_skill(self):
        Father.__init__(self)
        Father.show_skill(self)

    def show_mother_skill(self):
        Mother.__init__(self)
        Mother.show_skill(self)


child = Child()
child.show_father_skill()  # 调用：Father的属性
child.show_mother_skill()  # 调用：Mother的属性
child.show_self_skill()  # 调用：自定义的属性
