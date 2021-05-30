import numpy as np
from visualization import plot_predict_curve_2d, plot_learning_curve
from SimplePolynomialRegression import SimplePolynomialRegression
from model_selection import train_test_split
from metrics import mean_squared_error


def test(degree):
    # 1.生成数据
    np.random.seed(666)
    x = np.random.uniform(-3, 3, size=100)
    y = 0.5 * x ** 2 + x + 2 + np.random.normal(0, 1, size=100)

    # 2.多项式拟合
    simple_ploy_regression = SimplePolynomialRegression(degree=degree)
    simple_ploy_regression.fit(x, y)
    y_predict = simple_ploy_regression.predict(x)
    print("MSE:", mean_squared_error(y, y_predict))

    # 3.拟合曲线
    plot_predict_curve_2d(simple_ploy_regression, x, y, axis=[-3, 3, -1, 10])

    # 4.学习曲线
    X = x.reshape(-1, 1)
    X_train, X_test, y_train, y_test = train_test_split(X, y, seed=66)
    plot_learning_curve(simple_ploy_regression, X_train, X_test, y_train, y_test, axis=[0, len(X_train) + 1, 0, 4])


if __name__ == '__main__':
    test(degree=2)
