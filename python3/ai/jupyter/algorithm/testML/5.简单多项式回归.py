import numpy as np
import matplotlib.pyplot as plt
from SimplePolynomialRegression import SimplePolynomialRegression
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

    # 3.绘制
    plt.scatter(x, y)
    # plt.plot(x, y_predict, color='r')
    # plt.plot(np.sort(x), y_predict[np.argsort(x)], color='r')  # x从小到大的顺序
    x_plot = np.linspace(-3, 3, 100)
    y_plot = simple_ploy_regression.predict(x_plot)
    plt.plot(x_plot, y_plot, color='r')  # 绘制真实的拟合曲线
    plt.axis([-3, 3, -1, 10])
    plt.show()


if __name__ == '__main__':
    test(degree=2)
