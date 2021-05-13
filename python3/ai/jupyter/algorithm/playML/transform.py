import numpy as np


def to_ndarray(X):
    """检查list并转为np.ndarray"""
    return X if type(X) is np.ndarray else np.array(X)


def hstack_1(X):
    "第1列扩展为1"
    X = to_ndarray(X)

    # 如果是行向量，扩展为列向量
    if X.ndim == 1:
        X = X.reshape(-1, 1)
    X_b = np.hstack([np.ones((len(X), 1)), X])
    return X_b
