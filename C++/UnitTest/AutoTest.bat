@rem =====================
@rem  Debug:   导入静态库
@rem  Release: 导入动态库
@rem =====================


@rem 清空编译目录
@ rmdir Debug /Q /S 1>nul 2>nul
@ rmdir Release /Q /S 1>nul 2>nul


@rem 编译
@ echo build debug...
@ "C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe" build.sln /Rebuild "Debug"
@ if not %errorlevel%==0 @ goto :end

@rem 测试
@ echo test debug...
@ Debug\test.exe --gtest_output=xml:Debug\



@ echo build release...
@ "C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe" build.sln /Rebuild "Release"
@ if not %errorlevel%==0 @ goto :end


@ echo test release...
@ Release\test.exe --gtest_output=xml:Release\


:end
@ pause
