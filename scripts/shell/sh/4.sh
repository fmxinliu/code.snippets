#!/bin/bash
# 判断用户输入的文件类型


# 提示用户输入
read -p "Please input a filename: " file


# 判断文件类型
if [ -z "$file" ];then
  echo "Error, please input a filename"
  exit 1

elif [ ! -e "$file" ];then
  echo "You input is not a file"
  exit 2

elif [ -f "$file" ];then
  echo "$file is a regulare file!"

elif [ -d "$file" ];then
  echo "$file is a directory!"

elif [ -L "$file" ];then
  echo "$file is a soft link file!"

else
  echo "$file is an other file!"

fi
