cd src
msbuild /t:clean /p:configuration=release %*
msbuild /t:restore /p:configuration=release %*
msbuild /t:build,pack /p:configuration=release %*
cd ..
