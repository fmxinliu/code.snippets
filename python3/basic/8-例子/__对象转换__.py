def obj_to_dict(obj: object) -> dict:
    """对象转换为属性字典"""
    return obj.__dict__


def obj_list_to_dict(lst: list) -> dict:
    """对象列表转换为字典列表"""
    return [stu.__dict__ for stu in lst]


def obj_list_to_str_list(lst: list) -> list:
    """对象列表转换为字符串列表"""
    return [str(stu.__dict__) for stu in lst]


def obj_list_to_str(lst: list) -> str:
    """对象列表转换为字符串"""
    return str([stu.__dict__ for stu in lst])


def str_to_obj_dict_list(s: str) -> list:
    """字符串转换为对象列表"""
    return eval(s)
