global_num = 0


def test_modify_global_num():
    def modify_global_num():
        global global_num       # 修改全局变量
        global_num = 100

    print(global_num)
    modify_global_num()
    print(global_num)


def test_modify_nonlocal_num():
    nonlocal_num = 250

    def modify_nonlocal_num():
        nonlocal nonlocal_num   # 修改外层非全局变量
        nonlocal_num = 999

    print(nonlocal_num)
    modify_nonlocal_num()
    print(nonlocal_num)


test_modify_global_num()
test_modify_nonlocal_num()
