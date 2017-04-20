cd src
msbuild /t:clean
msbuild /t:restore
msbuild /verbosity:minimal 
msbuild /t:vstest
cd ..
