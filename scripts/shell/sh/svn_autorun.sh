#!/bin/bash
# 重启svn服务

killall svnserve

/usr/bin/svnserve -d -r /opt/svn --listen-port 3690

#/usr/bin/svnserve --daemon --root /opt/svn --listen-port 3690 --pid-file=/var/run/svnserve.pid
