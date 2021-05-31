# 需求：将8位老师随机分配到3个办公室

"""
步骤：
1.准备数据
  1.1. 8位老师  -- 2.列表
  1.2. 3个办公室 -- 列表嵌套
2.分配老师到办公室
  2.1. 随机分配
  2.2. 把老师的名字写入办公室列表 -- 办公室列表追加老师名字
3.验证是否分配成功
  3.1. 打印办公室详细信息：每个办公室的人数和对应的老师名字
"""

import random

# 1.准备数据
teachers = ['AA', 'BB', 'CC', 'DD', 'EE', 'FF', 'GG', 'HH']
offices = [[], [], []]

# 2.分配老师到办公室
for name in teachers:
    # 列表追加数据 -- append extend insert
    num = random.randint(0, 2)
    offices[num].append(name)

print(offices)

# 3.验证是否分配成功
i = 1
for office in offices:
    print(f"办公室{i}的人数是: {len(office)}")
    for teacher in office:
        print(teacher)
    i += 1
