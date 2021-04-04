# 需求：
# 1.警犬分2种：追击丢人和巡查毒品
# 2.警务人员和警犬一起工作。携带不同的警犬，执行不同的工作


# (1)定义父类
class Dog(object):
    # 父类方法
    def work(self):
        pass


# (2)定义子类
class ArmDog(Dog):
    # 重写父类的方法
    def work(self):
        print('追击丢人...')


class DrugDog(Dog):
    # 重写父类的方法
    def work(self):
        print('巡查毒品...')


# (3)传入不同对象，实现不同功能
class Person(object):
    # def work_with_dog(self, dog):
    def work_with_dog(self, dog: Dog):
        dog.work()


ad = ArmDog()
dd = DrugDog()

p = Person()
p.work_with_dog(ad)
p.work_with_dog(dd)
