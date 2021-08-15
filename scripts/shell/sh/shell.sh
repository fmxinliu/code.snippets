#!/bin/bash

# 1.脚本执行
# (1)
# chmod 755 shell.sh
# ./shell.sh
#
# (2)
# bash shell.sh



# 2.查询历史命令
# history  ->  保存在 ~/.bash_history
# !!       ->  执行上一条命令
# !n       ->  执行第n条命令
# !xx      ->  执行以xx开头的最后一条命令



# 3.快捷键
# ctrl+A   -> home
# ctrl+E   -> end
# ctrl+C   -> 强制终止
# ctrl+U   -> 剪切光标之前的字符
# ctrl+K   -> 剪切光标之后的字符
# ctrl+Y   -> 粘贴
# ctrl+D   -> logout
# ctrl+L   -> clear
# ctrl+R   -> 在历史命令中搜索



# 4.别名
# alias vi='vim'
# unalias vi
# 保存到~/.bashrc中，永久生效



# 5.输出重定向
# (1)标准输出
#    ls >  log.txt
#
# (2)标准错误
#    l 2>  log.txt
#
# (3)保存到一个文件中
#    ls &> log.txt
#    l  &> /dev/null  <-- 丢到垃圾箱，不输出
#
# (4)保存到不同文件中
#    ls >> log.txt 2>> err.txt



# 6.多命令顺序执行
# (1) 命令1 ; 命令2   <-- 即使命令1执行失败，命令2也会执行
#
#     date ; dd if=/dev/zero of=~/testfile bs=1k count=100000 ; date
#
# (2) 命令1 && 命令2
#
#
# (3) 命令1 || 命令2
#
#     命令 && echo yes || echo no  <-- 判断命令执行是否成功



# 7.管道： 命令1 | 命令2
# (1) 1的正确输出作为2的操作对象
# (2) 1执行错误，2不会执行
#
#     netstat -an | grep --color=auto "ESTABLISHED"



# 8.通配符
#   ?          <- 单个字符
#   *          <- 0个 or 多个字符
#   []         <- []中指定范围内的单个字符



# 9.特殊符号
# (1) 单引号   <- 所有特殊符号都没有特殊含义
# (2) 双引号   <- 大部分特殊符号没有特殊含义，但是 $ ` \ ！除外
# (3) 反引号   <- 系统命令
# (4) $(xx)    <- 同3
# (5) #        <- 注释
# (6）$xx      <- 引用变量的值
# (7) ${xx}    <- 同6
# (7) \        <- 转义



# 10.变量
# (1)用户自定义变量  <- 只在当前 shell 有效
#    uservar=123     <- 等号左右不能有空格
#    set             <- 查看所有变量
#    unset uservar   <- 取消定义
#    readonly s=abcd <- 只读变量
#
# (2)环境变量        <- 当前shell + 子shell有效。写入配置文件，在所有 shell 有效
#   export USER_PATH=~
#   env              <- 查看环境变量
#   unset  USER_PATH <- 取消定义
#
#   echo $PATH       <- 搜索路径
#   echo $PS1        <- 系统提示符
#
#   source ~/.bashrc <- 立即执行.bashrc，使.bashrc的修改立即生效，而不用注销重新登录
#
# (3)位置参数变量    <- 命令行参数
#   $0    - 命令本身
#   $1-$9 - 第n个参数
#   ${10} - 第10个参数
#   $*    - 所有参数，看作一个整体
#   $@    - 所有参数，参数列表
#   $#    - 参数个数
#
# (4)预定义变量
#   $?    - 上条命令执行状态，0-代表执行成功
#   $$    - 当前进程 pid



# 11.变量声明
#a=11
#declare -p a   # 字符串
#export a
#declare -p a   # 环境变量
#declare -i a
#declare -p a   # 数值



# 12.数值运算
aa=11
bb=22
# (1) 方法1
#echo $aa+$bb
#declare -i cc=$aa+$bb
#echo $cc
#
# (2) 方法2
#dd=$(expr $aa + $bb)  # +两边必须有空格
#echo $dd
#
# (3) 方法3
#ff=$(( $aa+$bb ))     # 两边可不留空格
#echo $ff
#
# (4) 方法4
#gg=$[ $aa+$bb ]
#echo $gg
#
#echo $(( (11+3)*3/2 + 1 ))



# 13.正则表达式
#  *    <- 前面一个字符出现0次或任意次 !!!
#  .    <- 任意一个字符
#  ^    <- 行首
#  $    <- 行尾
#  []   <- 指定范围的任意一个字符
#  \    <- 转义字符
#  \{n\}
#  \{n,\}
#  \{n,m\}  <- 前面一个字符出现的次数

# 通配符
ls q    # 匹配文件为q的文件(精确匹配)
ls q*   # 匹配文件以q开头的文件

# 正则符
#grep 'q*'  shell.sh   # 匹配任意字符，包含空行
#grep 'qq*' shell.sh   # 匹配包含q的字符行

echo -e 'qqq\n\naaa' | grep 'q*'
echo -e 'qqq\n\naaa' | grep 'qq'



# 14.字符截取命令
# grep   <- 行
# cut    <- 列（不适用于空格分隔的字符串）
# awk    <- 列

echo -e "a\tb\tc" | cut -f 2
echo -e "a;b;c;d" | cut -d ';' -f 2,3

# 以多个空格作为分隔符
echo -e "a  b  c" | cut -d ' ' -f 2        # 无法分隔
echo -e "a  b  c" | awk '{printf $2 "\n"}'

# awk指定分隔符
echo -e "a;b;c;d" | awk 'BEGIN{FS=";"} {print $2}'
echo -e "a;b;c;d" | awk -F ';' '{print $2}'

# 多行截取某一列
cat /etc/passwd | grep '/bin/bash' | grep -v 'root' | cut -d ':' -f 1,3
cat /etc/passwd | grep '/bin/bash' | grep -v 'root' | awk -F ':' '{print $1 "\t" $3}'       # print 自动换行
cat /etc/passwd | grep '/bin/bash' | grep -v 'root' | awk -F ':' '{printf $1 "\t" $3 "\n"}'

# printf 错误使用示例
printf  %s           a b c d e f  # abcde
printf  %s %s %s     a b c d e f  # %s%sabcdef

# printf 正确使用示例（每行3列）
printf '%s %s %s'    a b c d e f  # a b cd e f
printf '%s-%s-%s\n'  a b c d e f  # a-b-c d-e-f

# awk 格式： awk '条件1{动作1} 条件2{动作2}...' 文件名
echo -e "1:2:3\n4:5:6\n7:8:9" | awk -F ':'      '{print $0}'
echo -e "1:2:3\n4:5:6\n7:8:9" | awk -F ':' '$1==1{print $0}'
echo -e "1\t2\t3\n4\t5\t6\n7\t8\t9" | awk 'BEGIN{print "start"} END{print "end"}      {print $0}'
echo -e "1\t2\t3\n4\t5\t6\n7\t8\t9" | awk 'BEGIN{print "start"} END{print "end"} $1==4{print $0}'



# 15.sed 字符编辑
# 格式：sed [选项] '[动作]' 文件名
# 选项：
#       -n  只输出 sed 选择的数据，一般与 p 动作连用
#       -e  执行多条命令
#       -i  写入文件
# 动作：
#       a  追加                           echo -e '123\n456\n789' | sed '2a append'
#       i  插入
#       d  删除                           echo -e "00\n11\n22\n33\n44\n55\n66" | sed '/^55$/d' # 删除匹配到的行
#                                         echo -e '123\n456\n789' | sed    '2,3d'   # 删除2-3行
#
#       p  打印                           echo -e '123\n456\n789' | sed -n '2,3p'
#       c  行替换
#       s  字串替换 "ns/old/new/g"        echo -e '123\n456\n789' | sed    '2s/5/000/g'
#                                         echo -e '123\n456\n789' | sed -e '2s/5/000/g ; s/0/-/g'i  # 执行多条命令



# 16.sort 字符排序
sort    -t ":" -k 3,3 /etc/passwd  # 以:分割，使用第3个字符排序(字符顺序)
sort -n -t ":" -k 3,3 /etc/passwd  # 以:分割，使用第3个字符排序(数字顺序)



# 17.wc 字符统计




# 18.条件判断
#  格式一： test -e /root/install.log
#  格式二： [ -e /root/install.log ]
#
#      ==>  [ -d /root ] && echo yes || echo no
#
# (1)文件是否存在
#  -d  目录
#  -e  文件
#  -f  普通文件
#  -L  符号链接文件(软链接)
#
# (2)文件权限【ugo只要一个有权限，返回真】
#  -r
#  -w
#  -x
#
# (3)文件比较
#  -nt  文件1的修改时间比文件2的新，返回真
#  -ot
#  -ef  inode是否一致(硬链接)
#
# (4)整数比较
#  -eq    ==
#  -ne    !=
#  -gt    >
#  -lt    <
#  -ge    >=
#  -le    <=
#
# (5)字符串判断
#  -z     是否为空
#  -n     是否非空
#  ==
#  !=
#
# (6)多重条件判断
#  -a       &&
#  -o       ||
#  ! 判断   !

#   [ "$aa"=="$bb" ]             && echo equal || echo 'no equal'
#   [ ! "$aa"!="$bb" ]           && echo equal || echo 'no equal'
#   [ -n "$aa" -a "$aa"=="$bb" ] && echo equal || echo 'no equal'



# 19.字符串
# (1)拼接
a="my name is"
b="$a linux shell"
c="$a"" linux shell"
d="$a"-linux" "shell
echo -e "$a\n$b\n$c\n$d"

# (2)获取长度
echo ${#b}

# (3)提取子串
echo ${b:0:2}

# (4)查找子串
echo $(expr index "$b" is)

# (5)是否为空
[ -z "$b" ] && echo empty || echo "not empty"   <- 为空返回真
[ -n "$b" ] && echo "not empety" || echo empty  <- 非空返回真
[    "$b" ] && echo "not empety" || echo empty



# 20.数组
#  (1)定义：
#     a=(xx yy zz)
#     a[100]=dd
#
#  (2)引用数组元素：
#     ${a[0]}
#     ${a[100]}
#     ${a[*]}     <- 所有元素
#     ${a[@]}
#     ${#a[*]}    <- 元素个数
#     ${#a[@]}



# 21.函数
#  (1)定义：
#   [function] funcname(){
#     函数主体
#
#     [return intvalue]   <- 可省，如果省略，以最后一条命令运行结果作为返回值
#   }
#
#  (2)调用：
#    funcname xx1 xx2 xx3 xx4
#
#  (3)获取return指定的返回值
#    echo $?



# 22.source命令
#  (1)用法：
#            source FileName  或 . FileName
#
#  (2)作用：
#            在当前bash环境下，读取并执行FileName中的命令
#
#  (3)示例：
#         -> 在~/.bashrc添加坏境变量osname=centos6.2
#         -> 执行source ~/.bashrc, 使修改立即生效
#         -> 执行echo $osname, 打印变量名
#
#  (4)与其他方式执行脚本的区别：
#     ./script.sh      -> 要求脚本具有可执行权限，“.”用来指示当前脚本的绝对路径。
#                         新建一个子shell，在子shell中执行脚本（继承父shell的环境变量，
#                         但子shell新建的、改变的变量不会被带回父shell，除非使用export)
#
#     bash script.sh   -> 不要求脚本具有可执行权限，其他同上
#
#     source script.sh -> 不要求脚本具有可执行权限，也不新建子shell，脚本在当前shell中执行，直接影响当前shell
