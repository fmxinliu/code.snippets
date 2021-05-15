import numpy as np
from metrics import r2_score
from transform import to_ndarray, hstack_1


class LinearRegression:

    def __init__(self):
        """初始化 Linear Regression模型"""
        self.coef_ = None
        self.intercept_ = None
        self._theta = None

    def fit_normal(self, X_train, y_train):
        """根据训练数据集X_train, y_train，使用正规方程训练Linear Regression模型"""
        X_train = to_ndarray(X_train)
        y_train = to_ndarray(y_train)

        assert X_train.shape[0] == y_train.shape[0], \
            "the size of X_train must equal to the size of y_train"

        X_b = hstack_1(X_train)
        self._theta = np.linalg.inv(X_b.T.dot(X_b)).dot(X_b.T).dot(y_train)

        self.intercept_ = self._theta[0]
        self.coef_ = self._theta[1:]
        return self

    def fit_gd(self, X_train, y_train, eta=0.01, n_iters=1e4):
        """根据训练数据集X_train, y_train，使用梯度下降法训练Linear Regression模型"""
        X_train = to_ndarray(X_train)
        y_train = to_ndarray(y_train)

        assert X_train.shape[0] == y_train.shape[0], \
            "the size of X_train must equal to the size of y_train"

        def J(theta, X_b, y):
            """计算theta处的损失值"""
            return np.sum((y - X_b.dot(theta)) ** 2) / len(y)

        def dJ(theta, X_b, y):
            """计算theta处的梯度值"""
            return X_b.T.dot(X_b.dot(theta) - y) * 2 / len(X_b)

        def dJ_debug(theta, X_b, y, epsilon=0.01):
            """计算theta处梯度的近似值"""
            res = np.empty(len(theta))
            for i in range(len(theta)):
                theta_after = theta.copy()
                theta_after[i] += epsilon
                theta_before = theta.copy()
                theta_before[i] -= epsilon
                res[i] = (J(theta_after, X_b, y) - J(theta_before, X_b, y)) / (2 * epsilon)
            return res

        def gradient_descent(X_b, y, initial_theta, eta, n_iters=1e4, epsilon=1e-8):
            """
            梯度下降法
            :param X_b: [1:特征矩阵]
            :param y: 值向量
            :param initial_theta: 迭代初始值
            :param eta: 学习率
            :param n_iters: 迭代次数
            :param epsilon: 精度
            :return: 最终迭代值
            """
            theta = initial_theta
            i_iter = 0

            while i_iter < n_iters:
                gradient = dJ(theta, X_b, y)
                last_theta = theta
                theta = theta - eta * gradient

                if abs(J(theta, X_b, y) - J(last_theta, X_b, y)) < epsilon:
                    break

                i_iter += 1

            return theta

        X_b = np.hstack([np.ones((len(X_train), 1)), X_train])
        initial_theta = np.zeros(X_b.shape[1])
        self._theta = gradient_descent(X_b, y_train, initial_theta, eta, n_iters)

        self.intercept_ = self._theta[0]
        self.coef_ = self._theta[1:]
        return self

    def fit_sgd(self, X_train, y_train, n_iters=5, t0=5, t1=50):
        """根据训练数据集X_train, y_train，使用随机梯度下降法训练Linear Regression模型"""
        X_train = to_ndarray(X_train)
        y_train = to_ndarray(y_train)

        assert X_train.shape[0] == y_train.shape[0], \
            "the size of X_train must equal to the size of y_train"
        assert n_iters >= 1, \
            "the size of n_iters must greater than or equal to 1"

        def dJ_sgd(theta, X_b_i, y_i):
            """根据单个样本，计算theta处的搜索方向"""
            assert np.array(X_b_i).ndim == 1, "随机梯度法: 计算搜索方向时，要求输入单个样本"
            return X_b_i.T.dot(X_b_i.dot(theta) - y_i) * 2.

        def sgd(X_b, y, initial_theta, n_iters, t0=5, t1=50):
            """
            梯度下降法
            :param X_b: [1:特征矩阵]
            :param y: 值向量
            :param initial_theta: 迭代初始值
            :param n_iters: 迭代次数
            :param t0: 学习率参数1
            :param t1: 学习率参数2
            :return: 最终迭代值
            """

            def learning_rate(t):
                """动态学习率"""
                return t0 / (t + t1)

            theta = initial_theta
            m = len(X_b)

            for cur_iter in range(n_iters):
                indexs = np.random.permutation(m)
                X_b_new = X_b[indexs]
                y_new = y[indexs]
                for i in range(m):  # 随机获取一个样本
                    gradient_sgd = dJ_sgd(theta, X_b_new[i], y_new[i])  # 计算搜索方向
                    theta = theta - learning_rate(cur_iter * m + i) * gradient_sgd  # 计算下个迭代值

            return theta

        X_b = np.hstack([np.ones((len(X_train), 1)), X_train])
        initial_theta = np.zeros(X_b.shape[1])
        self._theta = sgd(X_b, y_train, initial_theta, n_iters, t0, t1)

        self.intercept_ = self._theta[0]
        self.coef_ = self._theta[1:]
        return self

    def predict(self, X_predict):
        """给定待预测数据集X_predict，返回表示X_predict的结果向量"""
        X_predict = to_ndarray(X_predict)
        feature_num = X_predict.shape[1] if X_predict.ndim > 1 else 1

        assert self.intercept_ is not None and self.coef_ is not None, \
            "must fit before predict!"
        assert feature_num == len(self.coef_), \
            "the feature number of X_predict must equal to the size of X_train"

        X_b = hstack_1(X_predict)
        return X_b.dot(self._theta)

    def score(self, X_test, y_test):
        """根据测试数据集X_test和y_test，确定当前模型的准确度"""
        y_predict = self.predict(X_test)
        return r2_score(y_test, y_predict)

    def __repr__(self):
        return "LinearRegression()"
