@rem =====================
@rem  Debug:   导入静态库
@rem  Release: 导入动态库
@rem =====================
@ set MSBuild="C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"


@rem 构建目录
@ rmdir release /Q /S 1>nul 2>nul
@ mkdir release

@ rmdir build /Q /S 1>nul 2>nul
@ mkdir build
@ mkdir build\v1.0
@ mkdir build\v1.1
@ mkdir build\v2.0
@ mkdir build\v2.1
@ mkdir build\A
@ mkdir build\B
@ mkdir build\C
@ mkdir build\test


@ set BuildDir=%cd%\build\
@ set ReleaseDir=%cd%\release\
@ set TestDir=%cd%\build\test\

@ set AppDir=%cd%\CuteApp\
@ set LibDir=%cd%\WidgetLib\

@rem ======
@rem  Lib
@rem ======

@ call :BuildLib V1_0  v1.0
@ if not %errorlevel%==0 goto :end

@ call :BuildLib V1_1  v1.1
@ if not %errorlevel%==0 goto :end

@ call :BuildLib V2_0  v2.0
@ if not %errorlevel%==0 goto :end

@ call :BuildLib V2_1  v2.1
@ if not %errorlevel%==0 goto :end


@rem =======
@rem   App
@rem =======

@ call :BuildApp v1.0  A
@ if not %errorlevel%==0 goto :end

@ call :BuildApp v2.0  B
@ if not %errorlevel%==0 goto :end

@ call :BuildApp v2.1  C
@ if not %errorlevel%==0 goto :end


@rem ==========
@rem  TESTING
@rem ==========
@ call :Test A  v1.0  nopause
@ call :Test A  v1.1  nopause

@ call :Test B  v2.0  nopause
@ call :Test B  v2.1  nopause
@ call :Test C  v2.1  nopause
@ goto :end


:Test
@ echo testing %1 + %2...
@ copy "%BuildDir%%1\CuteApp.exe"    "%TestDir%"   /Y  >nul
@ copy "%BuildDir%%2\WidgetLib.dll"  "%TestDir%"   /Y  >nul
@ "%TestDir%CuteApp.exe" %3
@ goto :eof



:BuildApp
@ copy "%BuildDir%%1\libversion.h"   "%LibDir%"     /Y  >nul
@ copy "%BuildDir%%1\WidgetLib.lib"  "%ReleaseDir%" /Y  >nul

@ echo building %1...
@ %MSBuild% /t:ReBuild /property:Configuration=Release "%AppDir%CuteApp.vcxproj"
@ rem if not %errorlevel%==0 exit /b %errorlevel%
@ if not %errorlevel%==0 goto :eof

@ copy "%AppDir%Release\CuteApp.exe"   "%BuildDir%%2"  >nul
@ exit /b 0



:BuildLib
@ echo #ifndef LIBVERSION_H >  %LibDir%libversion.h
@ echo #define LIBVERSION_H >> %LibDir%libversion.h
@ echo #define %1 1         >> %LibDir%libversion.h
@ echo #endif               >> %LibDir%libversion.h

@ echo building WidgetLib %2...
@ %MSBuild% /t:ReBuild /property:Configuration=Release "%LibDir%WidgetLib.vcxproj"
@ if not %errorlevel%==0 exit /b %errorlevel%
@ rem if not %errorlevel%==0 goto :eof

@ copy "%LibDir%libversion.h"            "%BuildDir%%2"  >nul
@ copy "%LibDir%Release\WidgetLib.lib"   "%BuildDir%%2"  >nul
@ copy "%LibDir%Release\WidgetLib.dll"   "%BuildDir%%2"  >nul
@ exit /b 0



:end
@ echo end!
@ pause
