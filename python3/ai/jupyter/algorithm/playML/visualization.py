import numpy as np
import matplotlib.pyplot as plt


def show_predict(y_true, y_predict, title="real vs predict"):
    plt.title(title)
    plt.scatter(np.arange(len(y_true)), y_true)
    plt.scatter(np.arange(len(y_predict)), y_predict, color='g')
    plt.show()
