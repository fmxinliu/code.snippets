import sys
import numpy as np
from sklearn.datasets import load_boston
from playML.model_selection import train_test_split
from playML.LinearRegression import LinearRegression
from playML.preprocessing import StandardScaler
from playML.visualization import show_predict


def test(fit_method='normal'):
    # 1.获取数据集
    boston = load_boston()
    X = boston.data
    y = boston.target

    # 2.1 过滤数据集
    y_max = np.max(y)
    X = X[y < y_max]
    y = y[y < y_max]

    # 2.2 数据集划分
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_ratio=0.2, seed=222)

    # 3.数据集归一化
    transfer = StandardScaler()
    transfer.fit(X_train)
    X_train_standard = transfer.transform(X_train)
    X_test_standard = transfer.transform(X_test)

    # 4.训练
    estimater = LinearRegression()
    if fit_method == 'sgd':
        estimater.fit_sgd(X_train_standard, y_train)
    elif fit_method == 'gd':
        estimater.fit_gd(X_train_standard, y_train)
    else:
        estimater.fit_normal(X_train_standard, y_train)

    # 5.预测
    y_predict = estimater.predict(X_test_standard)

    # 5.1 绘制预测值与真实值
    if fit_method == 'sgd':
        show_predict(y_test, y_predict, "随机梯度下降法")
    elif fit_method == 'gd':
        show_predict(y_test, y_predict, "全梯度下降法")
    else:
        show_predict(y_test, y_predict, "正规方程法")

    # 6.评价
    print("R^2误差是：", estimater.score(X_test_standard, y_test))


if __name__ == '__main__':
    if len(sys.argv) > 1:
        test(sys.argv[1])
    else:
        test()
