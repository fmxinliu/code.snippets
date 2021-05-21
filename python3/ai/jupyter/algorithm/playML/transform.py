import numpy as np


def to_ndarray(X):
    """检查list并转为np.ndarray"""
    return X if type(X) is np.ndarray else np.array(X)


def hstack_1(X):
    "扩展为[1, X]"
    X = to_ndarray(X)

    # 如果是行向量，扩展为列向量
    if X.ndim == 1:
        X = X.reshape(-1, 1)
    X_b = np.hstack([np.ones((len(X), 1)), X])
    return X_b


def hstack_n(x, n):
    """扩展为[1, x, x**2...]"""
    x = to_ndarray(x)

    if x.ndim == 1:
        X = x.reshape(-1, 1)
    else:
        X = x

    assert X.shape[1] == 1, "X.shape[1] not equal to 1."

    X_b = np.ones((len(x), 1))
    for i in range(1, n + 1):
        X_b = np.hstack([X_b, X ** i])

    return X_b
