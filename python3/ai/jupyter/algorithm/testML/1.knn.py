from playML.kNN import kNNClassifier
# from playML.preprocessing import StandardScaler
# from playML.model_selection import train_test_split
from playML.metrics import accuracy_socre

from sklearn import datasets
from sklearn.preprocessing import StandardScaler
from sklearn.model_selection import train_test_split, GridSearchCV
from sklearn.neighbors import KNeighborsClassifier


def test():
    # 1.加载数据集
    iris = datasets.load_iris()
    # print(iris.DESCR)
    X = iris.data
    y = iris.target

    # 2.数据集分割：测试集、训练集
    # X_train, X_test, y_train, y_test = train_test_split(X, y, test_ratio=0.2, seed=222)
    X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=222)

    # 3.数据集归一化
    transfer = StandardScaler()
    transfer.fit(X_train)
    X_train_standard = transfer.transform(X_train)
    X_test_standard = transfer.transform(X_test)

    # 4.机器学习
    # estimater = kNNClassifier(k=5)
    # estimater.fit(X_train_standard, y_train)
    # y_predict = estimater.predict(X_test_standard)

    # 4.1.网格搜索与交叉验证
    estimater = KNeighborsClassifier()

    # 超参数
    param_grid = [{
        'weights': ['uniform'],
        'n_neighbors': [1, 3, 5, 7, 9]
    }, {
        'weights': ['distance'],  # 权重=距离的倒数
        'n_neighbors': [1, 3, 5, 7, 9],
        'p': [i for i in range(1, 6)]
    }]
    grad_search = GridSearchCV(estimater, param_grid=param_grid, cv=10, n_jobs=1, verbose=1)
    grad_search.fit(X_train_standard, y_train)
    print("最佳参数", grad_search.best_params_)

    # 4.2.预测
    y_predict = grad_search.predict(X_test_standard)

    # 5.模型评估
    print("预测值：", y_predict)
    print("真实值：", y_test)
    print("准确率：", accuracy_socre(y_test, y_predict))


if __name__ == '__main__':
    test()
