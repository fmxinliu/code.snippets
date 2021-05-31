class School:
    def __init__(self):
        # 1.类内部添加
        self.school_address = '苏绣路'
        self.school_name = '希望小学'

    def __str__(self):
        return f'{self.school_name}，位于{self.school_address}'

    def info(self):
        return self.__str__() + f'，校龄{self.school_age}'


school = School()
school.school_age = 30  # 2.类外部添加
print(school.info())
