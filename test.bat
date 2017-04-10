cd src
msbuild /t:clean
msbuild /t:restore
msbuild /t:build
msbuild /t:vstest
cd ..
