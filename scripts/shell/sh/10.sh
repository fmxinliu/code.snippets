#!/bin/bash
# shell函数


#######################################################
#
# return：必须是整型 or 数值型字符串, 使用 echo $? 获取
#
#######################################################

function fun1(){
  echo "shell function: 1st"
  return 1
}

function fun2(){
  echo "shell function: 2st"
  return $(( 1+1 ))
}

function fun3(){
  echo "shell function: 3st"
  return "3"
}

function fun4(){
  echo "shell function: 4st"
  return "4abc"                 # error
}

function fun5(){
  echo "shell function: 5st"
  a=$(( 2+3 ))                  # 执行成功，返回 0
}

function fun6(){
  echo "shell function: 6st"
  b=$(2+4)                      # 执行失败，返回非 0
}


####################################################
#
# expr: 任意类型，直接返回，不能使用 echo $? 获取到
#
###################################################

function fun7(){
  echo "shell function: 7st"
  expr 3+4                      # 返回 3+4
}

function fun8(){
  echo "shell function: 8st"
  expr 4 + 4                    # 返回 8
}

function fun9() {
  echo "shell function: 9st"
  expr abc                      # 返回 abc
}

fun10(){
  echo "shell function: 10st"
  echo "$0: $@"
}


fun1
echo $?

fun2
echo $?

fun3
echo $?


fun4
echo $?

fun5
echo $?

fun6
echo $?

fun7
#echo $?                   # expr表达式，无法通过echo $? 获取到

fun8
#echo $?

fun9
echo $?

fun10 I love you
