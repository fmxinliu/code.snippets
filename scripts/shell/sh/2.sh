#!/bin/bash
# 备份svn仓库

# 同步系统时间
ntpdate asia.pool.ntp.org &> /dev/null

# 当前日期
date=$(date +%y%m%d)

# 统计大小
size=$(du -sh /opt/svn)

# 开始备份
if [ ! -d /tmp/svn_bak ];then
    mkdir /tmp/svn_bak
fi

echo "Date : $date" > /tmp/svn_bak/repoinfo.txt
echo "Data size : $size" >> /tmp/svn_bak/repoinfo.txt
cd /tmp/svn_bak
tar -zcf svn-repo-$date.tar.gz  /opt/svn repoinfo.txt &> /dev/null
rm -rf /tmp/svn_bak/repoinfo.txt
