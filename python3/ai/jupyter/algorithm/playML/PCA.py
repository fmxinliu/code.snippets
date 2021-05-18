import numpy as np
from transform import to_ndarray


class PCA:

    def __init__(self, n_components):
        """初始化PCA"""
        assert n_components >= 1, "n_components must be valid"
        self.n_components = n_components
        self.components_ = None

    def fit(self, X, eta=0.01, n_iters=1e4, epsilon=1e-8):
        """获得数据集X的前n个主成分"""
        X = to_ndarray(X)

        assert X.shape[1] >= self.n_components, \
            "n_components must not be greater than the feature number of X"

        def demean(X):
            """特征均值归零化"""
            return X - np.mean(X, axis=0)

        def f(w, X):
            """目标函数"""
            assert np.alltrue(np.mean(X, axis=0) < 1e-10), "必须先对X进行均值归零化"
            return np.sum(X.dot(w) ** 2) / len(X)

        def df(w, X):
            """计算目标函数的梯度"""
            return X.T.dot(X.dot(w)) * 2. / len(X)

        def direction(w):
            """向量单位化"""
            return w / np.linalg.norm(w)

        def first_component(X, initial_w, eta, n_iters=1e4, epsilon=1e-8):
            """利用梯度上升法，求解第一主成分"""

            cur_iter = 0
            w = direction(initial_w)

            while cur_iter < n_iters:
                gradient = df(w, X)
                last_w = w
                w = w + eta * gradient
                w = direction(w)  # 注意1: 每次求一个单位方向
                if abs(f(w, X) - f(last_w, X)) < epsilon:
                    break

                cur_iter += 1

            return w

        def remove_first_component(X, w):
            """去掉第一主成分"""
            return X - X.dot(w).reshape(-1, 1) * w

        X_pca = demean(X)
        self.components_ = np.empty(shape=(self.n_components, X_pca.shape[1]))
        for i in range(self.n_components):
            initial_w = np.random.random(X_pca.shape[1])
            w = first_component(X_pca, initial_w, eta, n_iters, epsilon)
            self.components_[i, :] = w
            X_pca = remove_first_component(X_pca, w)

        return self

    def transform(self, X):
        """将给定的X，映射到各个主成分分量中"""
        X = to_ndarray(X)

        assert self.components_ is not None, \
            "must fit before transform!"

        assert X.shape[1] == self.components_.shape[1], \
            "the feature number of X must be equal to the column number of components_"

        return X.dot(self.components_.T)

    def inverse_transform(self, X):
        """将给定的X，反向映射回原来的特征空间"""
        X = to_ndarray(X)

        assert self.components_ is not None, \
            "must fit before inverse_transform!"

        assert X.shape[1] == self.components_.shape[0], \
            "the feature number of X must be equal to the row number of components_"

        return X.dot(self.components_)

    def __repr__(self):
        return "PCA(n_components=%d)" % self.n_components
