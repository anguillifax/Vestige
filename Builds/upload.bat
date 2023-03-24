@echo off

set suffix=

CALL :Push win64
@REM CALL :Push osx
@REM CALL :Push web

pause

:Push
echo Pushing folder `%~1` as %~1%suffix%
butler push "%~1" sturmdesign/vestige:%~1%suffix%
EXIT /B 0