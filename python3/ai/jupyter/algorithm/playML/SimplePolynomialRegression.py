from metrics import r2_score
from transform import hstack_n
from LinearRegression import LinearRegression


class SimplePolynomialRegression:
    """简单多项式回归(x + x**2 + x**3 + ... + x**n)"""

    def __init__(self, degree=2):
        self._degree = degree
        self._linear_regression = None

    def fit(self, x_train, y_train):
        """根据训练数据集x_train, y_train训练Simple Polynomial Regression模型"""
        X_train = hstack_n(x_train, self._degree)
        self._linear_regression = LinearRegression()
        self._linear_regression.fit_normal(X_train[:, 1:], y_train)
        return self

    def predict(self, x_predict):
        """给定待预测数据集x_predict，返回表示x_predict的结果向量"""
        X_predict = hstack_n(x_predict, self._degree)
        return self._linear_regression.predict(X_predict[:, 1:])

    def score(self, x_test, y_test):
        """根据测试数据集x_test和y_test，确定当前模型的准确度"""
        y_predict = self.predict(x_test)
        return r2_score(y_test, y_predict)

    def __repr__(self):
        return "SimplePolynomialRegression(degree=%d)" % self._degree
