#!/bin/bash
# 批量添加指定数量的用户

read -p "please input user name: " name
read -p "please input the number of users: " num
read -p "please int the password of users: " -s passwd


# 判断输入是否为空
if [ -z "$name" -o -z "$num" -o -z "$passwd" ];then
   echo "some input empty"
   exit 1
fi

# 判断num是否为数字
y=$(echo "$num" | sed 's/[0-9]*//g')
if [ -n "$y" ];then
  echo "num error"
  exit 2
fi


# 添加
for (( i=1; i<=$num; i=i+1 ))
    do
         /usr/sbin/useradd "$name$i" > /dev/null &&  echo "$passwd" | /usr/bin/passwd --stdin "$name$i" > /dev/null
    done

# 查看
cat /etc/passwd | grep "$name"
