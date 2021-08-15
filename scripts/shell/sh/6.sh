#!/bin/bash
# 查看目录中的文件

dir=/root/sh

cd "$dir"
ls > ls.log
sed -i '/ls.log/d' ls.log

n=1

for i in $(cat ls.log)
    do
         echo -e "$n\t$i"
         n=$(( $n + 1))
    done

rm ls.log
