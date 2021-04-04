class Father:
    def __init__(self):
        print('Father.__init__()')


class Son(Father):                        # 1.子类需要自动调用父类的方法：子类不重写__init__()
    pass


class Daughter(Father):
    def __init__(self):                   # 2.子类不需要自动调用父类的方法：子类重写__init__()
        print('Daughter.__init__()')


class GrandSon(Daughter):
    def __init__(self):                   # 3.子类重写了父类方法，又需要调用父类的方法：子类重写__init__()，调用父类__init__()
        super().__init__()
        print('GrandSon.__init__()')


son = Son()
daughter = Daughter()
grandSon = GrandSon()
