def return_num():
    return 100, 200


# 元组类型
result = return_num()
print(result)

# 拆包
num1, num2 = return_num()
print(num1)  # 100
print(num2)  # 200

print(num1, num2)  # 100 200
