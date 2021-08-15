#!/bin/bash
# 求1~100和


# for 循环
s=0

for (( i=1; i<=100; i=i+1 ))
    do
         s=$(( $s+$i ))
    done

echo $s


# while 循环
i=1
s=0

while [ "$i" -le 100 ]
    do
         s=$(( $s+$i ))
         i=$(( $i+1 ))
    done

echo $s


# until 循环  <-- 与 while 循环相反
i=1
s=0

until [ "$i" -gt 100 ]
    do
         s=$(( $s+$i ))
         i=$(( $i+1 ))
    done

echo $s



##### 无限循环1 #####
i=0
while :
  do
      i=$(( $i+1 ))
      if [ $i -gt 100 ];then
          break  # 跳出循环          
      fi 
  done

echo $i



##### 无限循环2 #####
i=0
while true
  do
      i=$(( $i+1 ))
      if [ $i -gt 200 ];then
          break  # 跳出循环          
      fi 
  done

echo $i



##### 无限循环3 #####
i=0
for (( ;; ))
  do
      i=$(( $i+1 ))
      
      if [ $i -eq 300 ];then
          continue  # 跳过本次循环          
      fi 

      if [ $i -gt 300 ];then
          break  # 跳出循环          
      fi 
  done

echo $i
