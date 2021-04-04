from __基类__ import Father


class Child(Father):
    def __init__(self):
        self.skill = '互联网电商'

    def show_skill(self):
        print(f'拥有的技能：{self.skill}')


child = Child()
child.show_skill()  # 调用：自定义的属性
