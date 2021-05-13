import numpy as np
from transform import to_ndarray


class StandardScaler:
    """均值方差归一化"""

    def __init__(self):
        self.mean_ = None
        self.scale_ = None

    def fit(self, X):
        """根据训练数据集X，获得数据的均值和方差"""
        self.mean_ = np.mean(X, axis=0)
        self.scale_ = np.std(X, axis=0)
        return self

    def transform(self, X):
        """将X根据这个StandardScaler进行均值方差归一化处理"""
        assert X.ndim == 2, "The dimension of X must be 2"
        assert self.mean_ is not None and self.scale_ is not None, \
            "must fit before tranform!"

        X = to_ndarray(X)
        assert X.shape[1] == len(self.mean_), \
            "the feature number of X must be equal to mean_ and std_"

        resX = np.empty(shape=X.shape, dtype=float)
        for col in range(X.shape[1]):
            resX[:, col] = (X[:, col] - self.mean_[col]) / self.scale_[col]
        return resX

    def fit_transform(self, X):
        """将X根据这个StandardScaler进行均值方差归一化处理"""
        self.fit(X)
        return self.transform(X)
