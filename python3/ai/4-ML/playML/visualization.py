import numpy as np
import matplotlib.pyplot as plt


def show_predict(y_true, y_predict, title="real vs predict"):
    plt.title(title)
    plt.scatter(np.arange(len(y_true)), y_true)
    plt.scatter(np.arange(len(y_predict)), y_predict, color='g')
    plt.show()


def plot_predict_curve_2d(model, x, y, axis=None):
    assert len(x) == len(y), "the size of x must equal to the size of y"
    assert np.array(x).ndim == 1, "只能绘制2d回归曲线，x只能包含一个特征"

    # 绘制真实的拟合曲线
    X_plot = np.linspace(np.min(x), np.max(x), int((np.max(x) - np.min(x)) * 100)).reshape(-1, 1)
    y_plot = model.predict(X_plot)
    plt.plot(X_plot, y_plot, color='r')
    if axis is None:
        axis = [np.min(x), np.max(x), np.min(y), np.max(y)]
    plt.axis(axis)

    # plt.plot(np.sort(x), model.predict(np.sort(x).reshape(-1, 1)), color='g')  # x从小到大的顺序
    plt.scatter(x, y)
    plt.show()


def plot_learning_curve(algo, X_train, X_test, y_train, y_test, axis=None):
    """绘制学习曲线"""
    assert len(X_train) == len(y_train), \
        "the size of X_train must equal to the size of y_train"

    assert len(X_test) == len(y_test), \
        "the size of X_test must equal to the size of y_test"

    def mean_squared_error(y_true, y_predict):
        """计算y_true和y_predict之间的均方误差MSE"""
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        y_true, y_predict = np.array(y_true), np.array(y_predict)
        return np.sum((y_true - y_predict) ** 2) / len(y_true)

    train_score = []
    test_score = []
    for i in range(1, len(X_train) + 1):
        try:
            algo.fit(X_train[:i], y_train[:i])
        except np.linalg.LinAlgError:
            continue

        y_train_predict = algo.predict(X_train[:i])
        train_score.append(mean_squared_error(y_train[:i], y_train_predict))

        y_test_predict = algo.predict(X_test)
        test_score.append(mean_squared_error(y_test, y_test_predict))

    plt.plot([i for i in range(1, len(train_score) + 1)], np.sqrt(train_score), label='train')
    plt.plot([i for i in range(1, len(test_score) + 1)], np.sqrt(test_score), label='test')
    plt.legend()
    if axis is not None:
        plt.axis(axis)
    plt.title('learning curve')
    plt.show()


def plot_confusion_matrix(cfm):
    """绘制混淆矩阵"""
    plt.figure(figsize=(9, 5), dpi=100)

    ax1 = plt.subplot(1, 2, 1)
    ax1.matshow(cfm, cmap=plt.cm.gray)
    plt.title("正确预测")

    ax2 = plt.subplot(1, 2, 2)
    row_sums = np.sum(cfm, axis=0)
    err_matrix = cfm / row_sums  # 百分比
    np.fill_diagonal(err_matrix, 0)  # 对角线元素置为0
    ax2.matshow(err_matrix, cmap=plt.cm.gray)
    plt.title("错误预测")

    plt.show()


def plot_roc_curve(y_true, decision_scores):
    """绘制roc曲线"""
    assert len(np.unique(y_true)) <= 2, "只能绘制二分类ROC曲线"

    def TN(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 0) & (y_predict == 0))

    def FP(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 0) & (y_predict == 1))

    def FN(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 1) & (y_predict == 0))

    def TP(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 1) & (y_predict == 1))

    def TPR(y_true, y_predict):
        tp = TP(y_true, y_predict)
        fn = FN(y_true, y_predict)
        try:
            return tp / (tp + fn)
        except:
            return 0.

    def FPR(y_true, y_predict):
        fp = FP(y_true, y_predict)
        tn = TN(y_true, y_predict)
        try:
            return fp / (fp + tn)
        except:
            return 0.

    fprs = []
    tprs = []
    thresholds = np.arange(np.min(decision_scores), np.max(decision_scores), 0.1)
    for threshold in thresholds:
        y_predict = np.array(decision_scores >= threshold, dtype=int)
        fprs.append(FPR(y_true, y_predict))
        tprs.append(TPR(y_true, y_predict))

    plt.figure(figsize=(9, 4), dpi=100)
    plt.subplot(1, 2, 1)
    plt.plot(thresholds, fprs, label='fprs')
    plt.plot(thresholds, tprs, label='tprs')
    plt.xlabel("分类阈值")
    plt.ylabel("指标")
    plt.legend()
    plt.subplot(1, 2, 2)
    plt.plot(fprs, tprs)
    plt.xlabel("fprs")
    plt.ylabel("tprs")
    plt.show()


def plot_pr_curve(y_true, decision_scores):
    """绘制pr曲线"""
    assert len(np.unique(y_true)) <= 2, "只能绘制二分类PR曲线"

    def TN(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 0) & (y_predict == 0))

    def FP(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 0) & (y_predict == 1))

    def FN(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 1) & (y_predict == 0))

    def TP(y_true, y_predict):
        assert len(y_true) == len(y_predict), \
            "the size of y_true must equal to the size of y_predict"
        return np.sum((y_true == 1) & (y_predict == 1))

    def precision_score(y_true, y_predict):
        fp = FP(y_true, y_predict)
        tp = TP(y_true, y_predict)
        try:
            return tp / (fp + tp)
        except:
            return 0.

    def recall_score(y_true, y_predict):
        fn = FN(y_true, y_predict)
        tp = TP(y_true, y_predict)
        try:
            return tp / (fn + tp)
        except:
            return 0

    precisions = []
    recalls = []
    thresholds = np.arange(np.min(decision_scores), np.max(decision_scores), 0.1)
    for threshold in thresholds:
        y_predict = np.array(decision_scores >= threshold, dtype=int)
        precisions.append(precision_score(y_true, y_predict))
        recalls.append(recall_score(y_true, y_predict))

    plt.figure(figsize=(9, 3), dpi=100)
    plt.subplot(1, 2, 1)
    plt.plot(thresholds, precisions, label='精准率')
    plt.plot(thresholds, recalls, label='召回率')
    plt.xlabel("分类阈值")
    plt.ylabel("指标")
    plt.legend()
    plt.subplot(1, 2, 2)
    plt.plot(precisions, recalls)
    plt.xlabel("精准率")
    plt.ylabel("召回率")
    plt.show()


def plot_decision_boundary_2d(model, X, y, axis=None):
    """绘制决策边界"""
    assert np.array(X).ndim == 2 and np.array(X).shape[1] == 2, \
        "绘制2d决策边界要求：X只能存在2个特征"

    def axis_range(X):
        """生成绘制范围"""
        return [np.min(X[:, 0]), np.max(X[:, 0]), np.min(X[:, 1]), np.max(X[:, 1])]

    def generate_x_y_f(model, axis):
        """生成2d等高线数据"""
        # 1.生成绘制点
        x0, x1 = np.meshgrid(
            np.linspace(axis[0], axis[1], int((axis[1] - axis[0]) * 100)),
            np.linspace(axis[2], axis[3], int((axis[3] - axis[2]) * 100))
        )

        # 2.对绘制点进行预测
        X_new = np.c_[x0.ravel(), x1.ravel()]
        y_predict = model.predict(X_new)
        f = y_predict.reshape(x0.shape)

        return x0, x1, f

    def plot_contour(x0, x1, f):
        """填充等高线"""
        from matplotlib.colors import ListedColormap
        custom_cmap = ListedColormap(['#EF9A9A', '#FFF59D', '#90CAF9'])
        if len(np.unique(f)) > 3:
            custom_cmap = None
        plt.contourf(x0, x1, f, cmap=custom_cmap)
        # plt.show()

    def plot_scatter(X, y):
        """绘制散点图"""
        for i in np.unique(y):
            plt.scatter(X[y == i, 0], X[y == i, 1])
        # plt.show()

    if axis is None:
        axis = axis_range(X)
    plot_contour(*generate_x_y_f(model, axis))
    plot_scatter(X, y)
    plt.show()
