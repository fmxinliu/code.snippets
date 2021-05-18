from sklearn.datasets import load_digits
from playML.PCA import PCA
import matplotlib.pyplot as plt


def test():
    # 1.获取数据集
    digits = load_digits()
    X = digits.data
    y = digits.target
    print("降维前的数据集: ", X.shape)

    # 2.降维
    pca = PCA(n_components=2)
    pca.fit(X)
    X_reduction = pca.transform(X)
    print("降维后的数据集: ", X_reduction.shape)

    # 3.绘制
    for i in range(10):
        plt.scatter(X_reduction[y == i, 0], X_reduction[y == i, 1], alpha=0.8)
    plt.show()


if __name__ == '__main__':
    test()
