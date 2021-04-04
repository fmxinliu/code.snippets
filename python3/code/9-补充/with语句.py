import os

# 使用完毕，自动清理资源:
with open('test.txt', 'w') as f:
    pass

os.remove('test.txt')
