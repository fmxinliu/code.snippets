import numpy as np


def test():
    import matplotlib.pyplot as plt
    from sklearn.datasets import load_iris
    from playML.model_selection import train_test_split
    from playML.LogisticRegression import LogisticRegression

    # 1.获取数据集
    iris = load_iris()
    X = iris.data
    y = iris.target

    # 2.1.数据预处理，构建二分类数据
    X = X[y < 2, :2]  # <-- 方便可视化：取2列【2个特征】
    y = y[y < 2]

    # 2.2.数据集划分
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_ratio=0.2, seed=222)

    # 3.逻辑回归
    log_reg = LogisticRegression()
    log_reg.fit(X_train, y_train)
    y_predict = log_reg.predict(X_test)
    y_proba = log_reg.predict_proba(X_test)
    score = log_reg.score(X_test, y_test)

    print("y_true:    ", y_test)
    print("y_predict: ", y_predict)
    print("y_proba:   ", y_proba)
    print("准确度：    ", score)

    # 决策边界: theta_0 + theth_1 * x1 + theta2 * x2 = 0
    def x2(x1):
        theta_0 = log_reg.intercept_
        theta_1 = log_reg.coef_[0]
        theta_2 = log_reg.coef_[1]
        return (-theta_1 * x1 - theta_0) / theta_2

    # 4.绘制
    plt.scatter(X_train[y_train == 0, 0], X_train[y_train == 0, 1], color='red')
    plt.scatter(X_train[y_train == 1, 0], X_train[y_train == 1, 1], color='blue')
    plt.scatter(X_test[y_predict == 0, 0], X_test[y_predict == 0, 1], color='red', marker='*')
    plt.scatter(X_test[y_predict == 1, 0], X_test[y_predict == 1, 1], color='blue', marker='*')
    x1_plot = np.linspace(4, 8, 1000)
    x2_plot = x2(x1_plot)
    plt.plot(x1_plot, x2_plot)
    plt.show()


def test_2():
    from sklearn.datasets import load_iris, load_digits
    from sklearn.model_selection import train_test_split
    from sklearn.linear_model import LogisticRegression
    from playML.metrics import precision_score, recall_score, f1_score, confusion_matrix
    from playML.visualization import plot_confusion_matrix, plot_roc_curve, plot_pr_curve, plot_decision_boundary_2d

    def test_evaluate_score():
        """评价指标"""
        digits = load_digits()

        def test_bin_classification():
            """二分类"""
            X = digits.data
            y = digits.target.copy()

            # 产生极度偏斜数据【二分类】
            y[digits.target == 9] = 1
            y[digits.target != 9] = 0

            from sklearn.linear_model import LogisticRegression
            X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.8, random_state=666)
            log_reg = LogisticRegression()
            log_reg.fit(X_train, y_train)

            print(precision_score(y_test, log_reg.predict(X_test)))
            print(recall_score(y_test, log_reg.predict(X_test)))
            print(f1_score(y_test, log_reg.predict(X_test)))

            plot_roc_curve(y_test, log_reg.decision_function(X_test))
            plot_pr_curve(y_test, log_reg.decision_function(X_test))

        def test_mul_classification():
            """多分类"""
            X = digits.data
            y = digits.target.copy()

            X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.8, random_state=666)
            log_reg = LogisticRegression()
            log_reg.fit(X_train, y_train)

            print('混淆矩阵：\n', confusion_matrix(y_test, log_reg.predict(X_test)))

            plot_confusion_matrix(confusion_matrix(y_test, log_reg.predict(X_test)))

        test_bin_classification()
        test_mul_classification()

    def test_decision_boundary():
        """决策边界"""
        iris = load_iris()

        def test_bin_classification():
            """二分类"""
            X = iris.data
            y = iris.target
            a = np.unique(y)

            X = X[y < 2, :2]
            y = y[y < 2]

            log_reg = LogisticRegression()
            log_reg.fit(X, y)
            plot_decision_boundary_2d(log_reg, X, y)

        def test_mul_classification():
            """多分类"""
            X = iris.data
            y = iris.target

            # 三分类
            X = iris.data[:, :2]  # 可视化：2个特征变量
            y = iris.target

            # 四分类
            y[np.random.randint(0, 70, 50)] = 3

            log_reg = LogisticRegression()
            log_reg.fit(X, y)
            plot_decision_boundary_2d(log_reg, X, y)

        test_bin_classification()
        test_mul_classification()

    test_evaluate_score()
    test_decision_boundary()


if __name__ == '__main__':
    test()
    test_2()
