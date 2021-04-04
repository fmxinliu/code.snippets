class Father(object):
    def __init__(self):
        self.skill = '经商'

    def show_skill(self):
        print(f'继承Father的技能：{self.skill}')


class Mother(object):
    def __init__(self):
        self.skill = '厨艺'

    def show_skill(self):
        print(f'继承Mother的技能：{self.skill}')
