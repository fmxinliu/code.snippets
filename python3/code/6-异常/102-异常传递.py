# -*- coding: gb2312 -*-

# ����:
# 1.����ֻ����ʽ��1.txt����������ʾ�û������½�
# 2.��ӡ�ļ����ݡ����������ֹ����ʾ�û�

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
        # ��������ʾ���У�����Ctrl + C
        print('������ֹ')
    finally:
        f.close()
        os.remove('1.txt')  # ɾ���ļ�
except:
    print('�ļ������ڣ��½�')
    f = open('1.txt', 'w')
    f.writelines(('111\n', '222\n', '333\n'))
    f.close()
finally:
    print('����')

