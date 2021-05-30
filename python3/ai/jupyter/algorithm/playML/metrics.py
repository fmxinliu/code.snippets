import numpy as np
from transform import to_ndarray


def __check__(y_true, y_predict):
    y_true = to_ndarray(y_true)
    y_predict = to_ndarray(y_predict)
    assert y_true.shape[0] == y_predict.shape[0], \
        "the size of y_true must equal to the size of y_predict"
    return y_true, y_predict


def accuracy_socre(y_true, y_predict):
    """计算y_true和y_predict之间的准确度"""
    y_true, y_predict = __check__(y_true, y_predict)
    return sum(y_true == y_predict) / len(y_true)


def mean_squared_error(y_true, y_predict):
    """计算y_true和y_predict之间的均方误差MSE"""
    y_true, y_predict = __check__(y_true, y_predict)
    return np.sum((y_true - y_predict) ** 2) / len(y_true)


def root_mean_squared_error(y_true, y_predict):
    """计算y_true和y_predict之间的均方根误差RMSE"""
    return np.sqrt(mean_squared_error(y_true, y_predict))


def mean_absolute_error(y_true, y_predict):
    """计算y_true和y_predict之间的平均绝对误差MAE"""
    y_true, y_predict = __check__(y_true, y_predict)
    return np.sum(np.fabs(y_true - y_predict)) / len(y_true)


def r2_score(y_true, y_predict):
    """计算y_true和y_predict之间的R Squared"""
    return 1 - mean_squared_error(y_true, y_predict) / np.var(y_true)


def confusion_matrix(y_true, y_predict):
    """根据y_true和y_predict生成混淆矩阵"""
    assert len(y_true) == len(y_predict), \
        "the size of y_true must equal to the size of y_predict"
    unique_values = np.unique(y_true)
    lst = [np.sum((y_true == i) & (y_predict == j)) for i in unique_values for j in unique_values]
    return np.array(lst).reshape(len(unique_values), -1)


def precision_score(y_true, y_predict, return_value_when_binary=True):
    """计算y_true和y_predict之间的精准率"""
    cm = confusion_matrix(y_true, y_predict)
    a = np.diagonal(cm)
    b = np.sum(cm, axis=0)
    try:
        s = a / b
    except (ZeroDivisionError, OverflowError):
        s = np.zeros(len(cm))
    finally:
        if (len(s) <= 2) and return_value_when_binary:
            return s[1]
        return s


def recall_score(y_true, y_predict, return_value_when_binary=True):
    """计算y_true和y_predict之间的召回率"""
    cm = confusion_matrix(y_true, y_predict)
    a = np.diagonal(cm)
    b = np.sum(cm, axis=1)
    try:
        s = a / b
    except (ZeroDivisionError, OverflowError):
        s = np.zeros(len(cm))
    finally:
        if (len(s) <= 2) and return_value_when_binary:
            return s[1]
        return s


def f1_score(y_true, y_predict, return_value_when_binary=True):
    """计算y_true和y_predict之间的调和平均值"""
    ps = precision_score(y_true, y_predict, False)
    rs = recall_score(y_true, y_predict, False)
    try:
        s = 2 * ps * rs / (ps + rs)
    except (ZeroDivisionError, OverflowError):
        s = np.zeros(len(ps))
    finally:
        if (len(s) <= 2) and return_value_when_binary:
            return s[1]
        return s
