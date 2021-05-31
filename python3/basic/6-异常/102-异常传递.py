# -*- coding: gb2312 -*-

# 需求:
# 1.尝试只读方式打开1.txt，不存在提示用户，并新建
# 2.打印文件内容。如果意外终止，提示用户

import os
import time

try:
    f = open('1.txt')
    try:
        while True:
            line = f.readline()
            if len(line) == 0:
                break
            time.sleep(2)
            print(line, end='')
    except:
        # 在命令提示符中，按下Ctrl + C
        print('意外终止')
    finally:
        f.close()
        os.remove('1.txt')  # 删除文件
except:
    print('文件不存在，新建')
    f = open('1.txt', 'w')
    f.writelines(('111\n', '222\n', '333\n'))
    f.close()
finally:
    print('结束')

