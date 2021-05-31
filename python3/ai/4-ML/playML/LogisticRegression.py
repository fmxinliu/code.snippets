import numpy as np
from .metrics import accuracy_socre
from .transform import to_ndarray, hstack_1


class LogisticRegression:

    def __init__(self):
        """初始化Logistic Regression模型"""
        self.coef_ = None
        self.intercept_ = None
        self._theta = None

    def _sigmoid(self, t):
        return 1. / (1. + np.exp(-t))

    def fit(self, X_train, y_train, eta=0.01, n_iters=1e4):
        """根据训练数据集X_train, y_train，使用梯度下降法训练Logistic Regression模型"""
        X_train = to_ndarray(X_train)
        y_train = to_ndarray(y_train)

        assert X_train.shape[0] == y_train.shape[0], \
            "the size of X_train must equal to the size of y_train"

        def J(theta, X_b, y):
            """计算theta处的损失值"""
            y_hat = self._sigmoid(X_b.dot(theta))
            return - np.sum(y * np.log(y_hat) + (1 - y) * np.log(1 - y_hat)) / len(y)

        def dJ(theta, X_b, y):
            """计算theta处的梯度值"""
            return X_b.T.dot(self._sigmoid(X_b.dot(theta)) - y) / len(X_b)

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

    def predict_proba(self, X_predict):
        """给定待预测数据集X_predict，返回表示X_predict的结果概率向量"""
        X_predict = to_ndarray(X_predict)
        feature_num = X_predict.shape[1] if X_predict.ndim > 1 else 1

        assert self.intercept_ is not None and self.coef_ is not None, \
            "must fit before predict!"
        assert feature_num == len(self.coef_), \
            "the feature number of X_predict must equal to the size of X_train"

        X_b = hstack_1(X_predict)
        return self._sigmoid(X_b.dot(self._theta))

    def predict(self, X_predict):
        """给定待预测数据集X_predict，返回表示X_predict的结果向量"""
        X_predict = to_ndarray(X_predict)
        feature_num = X_predict.shape[1] if X_predict.ndim > 1 else 1

        assert self.intercept_ is not None and self.coef_ is not None, \
            "must fit before predict!"
        assert feature_num == len(self.coef_), \
            "the feature number of X_predict must equal to the size of X_train"

        proba = self.predict_proba(X_predict)
        return np.array(proba >= 0.5, dtype='int')

    def score(self, X_test, y_test):
        """根据测试数据集X_test和y_test，确定当前模型的准确度"""
        y_predict = self.predict(X_test)
        return accuracy_socre(y_test, y_predict)

    def __repr__(self):
        return "LogisticRegression()"
