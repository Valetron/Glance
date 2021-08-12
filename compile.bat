echo off

dotnet build --configuration Release

assoc .jpg=jpegfile
assoc .jpeg=jpegfile
assoc .png=pngfile
assoc .gif=giffile
assoc .bmp=bmpfile

ftype jpegfile="%~dp0src\bin\Release\net5.0-windows\Glance.exe" "%%1" %*
ftype pngfile="%~dp0src\bin\Release\net5.0-windows\Glance.exe" "%%1" %*
ftype giffile="%~dp0src\bin\Release\net5.0-windows\Glance.exe" "%%1" %*
ftype bmpfile="%~dp0src\bin\Release\net5.0-windows\Glance.exe" "%%1" %*

pause null