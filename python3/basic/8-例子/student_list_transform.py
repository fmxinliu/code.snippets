from __对象转换__ import *
from student import Student


def create_student_list():
    """创建对象列表"""
    return [
        Student('张三', 18),
        Student('李四', 22),
        Student('王五', 21),
    ]


def print_object(obj):
    print(obj, type(obj))


if __name__ == '__main__':
    def test_student_to_dict():
        student = Student('老王', 40)
        print(student)
        print(obj_to_dict(student))
        print()

    def test_convert_to_student_str():
        student_list = create_student_list()                        # 对象列表
        student_list_dict_list = obj_list_to_dict(student_list)     # 属性字典列表
        student_list_str_list = obj_list_to_str_list(student_list)  # 字符串列表
        student_list_str = obj_list_to_str(student_list)            # 字符串

        print_object(student_list)
        print_object(student_list_dict_list)  # 列表
        print_object(student_list_str_list)
        print_object(student_list_str)        # 字符串
        print()

    def test_convert_to_student_list():
        student_list_str = obj_list_to_str(create_student_list())
        student_list_dict_list = str_to_obj_dict_list(student_list_str)
        student_list = [Student(d['name'], d['age'])for d in student_list_dict_list]

        print_object(student_list_str)
        print_object(student_list_dict_list)
        print_object(student_list)

    test_student_to_dict()
    test_convert_to_student_str()
    test_convert_to_student_list()
