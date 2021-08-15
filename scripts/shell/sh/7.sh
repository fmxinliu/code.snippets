#!/bin/bash
# 批量压缩与解压缩

dir=/root/sh
tardir="$dir"/tar


# 压缩
cd "$dir"
ls *.sh > ls.log

mkdir "$tardir" &> /dev/null

for i in $(cat ls.log)
    do
         tar -zcf "$tardir"/"$i".tar.gz "$i" &> /dev/null
    done

rm -rf ls.log


# 解压缩
cd "$tardir"
ls *.tar.gz > ls.log

for i in $(cat ls.log)
    do
         tar -zxf "$i" &> /dev/null
    done

rm -rf ls.log
