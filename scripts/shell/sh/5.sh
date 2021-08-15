#!/bin/bash
# 读入用户选择


read -t 30 -p "please choose yes/no: " cho

case "$cho" in
  yes)
       echo "you choose is yes"
       ;;
  no)
       echo "you choose is no"
       ;;
  *)
       echo  "you input is $cho, not in (yes/no)"
       ;;
esac

