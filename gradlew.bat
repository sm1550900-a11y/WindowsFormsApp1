@ECHO OFF
SET DIR=%~dp0
IF EXIST "%DIR%\gradle\wrapper\gradle-wrapper.jar" (
  gradle %*
) ELSE (
  gradle %*
)
