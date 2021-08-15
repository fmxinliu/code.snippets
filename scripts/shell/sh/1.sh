#!/bin/bash
# 统计根分区使用率

rate=$(df -h | grep "/dev/sda5" | awk '{print $5}' | cut -d "%" -f 1)

if [ $rate -ge 80 ];then
  echo "Waring: /dev/sda5 is full !!!"
fi
