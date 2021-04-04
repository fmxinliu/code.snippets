try:
    print(f)
# except NameError:                          # 捕获异常 NameError
#     print('变量未定义')
# except ZeroDivisionError as e:             # 捕获异常 ZeroDivisionError
#     print(e)
except (NameError, ZeroDivisionError) as e:  # 捕获多个异常(简写)
    print(e)
# except:                                    # 捕获所有异常(简写，不关心异常信息)
except Exception as e:                       # 捕获所有异常
    print(e)
else:                                        # 未发生异常，执行这里
    print('没有发生异常')
finally:                                     # 无论是否捕获到异常，都会执行
    print('整个流程结束')
