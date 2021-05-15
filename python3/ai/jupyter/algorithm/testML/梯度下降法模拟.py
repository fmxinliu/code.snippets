import numpy as np
import matplotlib.pyplot as plt


def dJ(theta):
    """损失函数在theta处的导数"""
    return 2 * (theta - 2.5)


def J(theta):
    """损失函数在theta处的值"""
    return (theta - 2.5) ** 2 - 1


def gradient_descent(initial_theta, eta, n_iters=1e4, epsilon=1e-8):
    """
    梯度下降法
    :param initial_theta: 迭代初始值
    :param eta: 学习率
    :param n_iters: 迭代次数
    :param epsilon: 精度
    :return: 最终迭代值，历史迭代值列表
    """
    theta = initial_theta
    theta_history = [initial_theta]
    i_iter = 0

    while i_iter < n_iters:
        gradient = dJ(theta)
        last_theta = theta
        theta = theta - eta * gradient
        theta_history.append(theta)

        if abs(last_theta - theta) < epsilon:
            break

        i_iter += 1

    return theta, theta_history


def plot_theta_history(x, theta_history, title=''):
    """
    绘制梯度下降过程曲线
    :param x: x范围
    :param theta_history: 迭代过程值列表
    """
    plt.title(title)
    plt.plot(x, J(x))
    plt.plot(theta_history, J(np.array(theta_history)), color='r', marker='<')
    plt.show()


# 损失函数
plot_x = np.linspace(-1, 6, 141)
plot_y = (plot_x - 2.5) ** 2 - 1


def learning_rate_small():
    eta = 0.1
    theta, theta_history = gradient_descent(0, eta)
    plot_theta_history(plot_x, theta_history, "学习率太小")


def learning_rate_large():
    eta = 1.1
    theta, theta_history = gradient_descent(0, eta, 10)
    plot_theta_history(plot_x, theta_history, "学习率太大")


learning_rate_small()
learning_rate_large()
