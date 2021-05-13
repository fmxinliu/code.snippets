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
