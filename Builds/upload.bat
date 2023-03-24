@echo off

set suffix=

echo Deleting Vestige_BurstDebugInformation_DoNotShip
rmdir /s /q "win64\Vestige_BurstDebugInformation_DoNotShip"
echo.

CALL :Push win64
@REM CALL :Push osx
@REM CALL :Push web

echo.
echo Upload complete.
echo.
pause

:Push
echo Pushing folder `%~1` as %~1%suffix%
butler push "%~1" sturmdesign/vestige:%~1%suffix%
EXIT /B 0