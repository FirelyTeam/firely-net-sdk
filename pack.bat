cd src
msbuild /t:clean
msbuild /t:restore
msbuild /t:build,pack /p:configuration=release
cd ..
