from student import Student

student = Student('张三', 18)
print(student)
print(student.__dict__)
print(Student.__dict__)
print()

# 构建对象列表
student_list = [
    Student('张三', 18),
    Student('李四', 22)
]

# 打印列表
print(student_list)  # 打印的是：每个对象的内存地址
print([stu.__dict__ for stu in student_list])  # 打印的是：每个对象属性构成的字典
print([str(stu.__dict__) for stu in student_list])  # 打印的是：每个对象属性构成的字典，转换为字符串的形式
