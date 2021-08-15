#!/bin/bash
# 判断svnserve是否启动


# 安装nmap包
package=$(rpm -qa | grep nmap)
if [ -z "$package" ];then
  yum -y install nmap
fi


# 使用nmap扫描服务器，检查svn服务状态
port=$(nmap -sT 192.168.244.250 | grep tcp | grep 'svn' | awk '{print $2}')

if [ "$port" == "open" ];then
    echo "$(date) svn running ok" >> /tmp/autostart-svn.log
else
#    /etc/rc.d/init.d/httpd start &> /dev/null
    /usr/bin/svnserve -d -r /opt/svn --listen-port 3690
    echo "$(date) restart svn" >> /tmp/autostart-svn-err.log
fi
