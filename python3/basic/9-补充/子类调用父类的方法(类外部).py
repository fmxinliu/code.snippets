class Father:
    def get_name(self):
        return '父亲'


class Child(Father):
    def get_name(self):
        return '子女'


child = Child()
print(child.get_name())
print(Father.get_name(child))
print(super(Child, child).get_name())
