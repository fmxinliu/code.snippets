import os.path
from student_list_transform import *


def read_from_file(file):
    # 创建学生列表
    student_list = []

    # 读取文件
    if os.path.exists(file):
        f = open(file, 'r')
        student_list_str = f.read()
        f.close()

        # 转换为字典列表
        student_dict_list = str_to_obj_dict_list(student_list_str)

        # 转换为对象列表
        student_list = [Student(d['name'], d['age'])for d in student_dict_list]
    else:
        print(file + '文件不存在')

    return student_list


def write_to_file(file, lst):
    # 转换为字符串
    student_list_str = obj_list_to_str(lst)

    # 写入文件
    try:
        f = open(file, 'w')
        f.writelines(student_list_str)
    except:
        print('写入失败')
    else:
        print('写入成功')
    finally:
        f.close()


if __name__ == '__main__':
    file_name = 'student.txt'
    stu_list = read_from_file(file_name)

    if len(stu_list) == 0:
        write_to_file(file_name, create_student_list())
        stu_list = read_from_file(file_name)

    for index, stu in enumerate(stu_list, start=1):
        print(f'{index}) {stu}')
