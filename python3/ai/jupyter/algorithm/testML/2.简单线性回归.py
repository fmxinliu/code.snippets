import numpy as np
import matplotlib.pyplot as plt
from sklearn.datasets import load_boston
from playML.model_selection import train_test_split
from playML.SimpleLinearRegression import SimpleLinearRegression
from playML.metrics import mean_squared_error, root_mean_squared_error, mean_absolute_error, r2_score


def test():
    # 1.获取数据集
    boston = load_boston()
    x = boston.data[:, 5]
    y = boston.target

    # plt.scatter(x, y)
    # plt.show()

    # 2.过滤数据集
    y_max = np.max(y)
    x = x[y < y_max]
    y = y[y < y_max]

    # plt.scatter(x, y)
    # plt.show()

    # 3.数据集划分
    x_train, x_test, y_train, y_test = train_test_split(x, y, test_ratio=0.2, seed=222)

    # 4.训练
    estimater = SimpleLinearRegression()
    estimater.fit(x_train, y_train)

    # 5.预测
    y_predict = estimater.predict(x_test)

    # 6.评价
    print("均方误差MSE：", mean_squared_error(y_test, y_predict))
    print("均方根误差RMSE：", root_mean_squared_error(y_test, y_predict))
    print("平均绝对误差MAE：", mean_absolute_error(y_test, y_predict))
    print("R Squared error：", r2_score(y_test, y_predict))

    plt.scatter(x, y)
    plt.scatter(x_test, y_predict, color='g')
    plt.plot(x_train, estimater.predict(x_train), color='r')
    plt.show()


if __name__ == '__main__':
    test()
