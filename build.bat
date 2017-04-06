cd src
msbuild /t:clean
msbuild /t:restore
msbuild /t:build
msbuild /t:vstest

pause 

msbuild /t:clean
msbuild /t:build,pack /p:configuration=release
cd ..
