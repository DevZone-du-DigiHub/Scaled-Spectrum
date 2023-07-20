@echo off
REM

cd ..
cd ..

echo Selecting your personal branch....
git switch setmoth
cls

echo Adding modified files in cache. Please wait...
git add --all
cls

set /p commitDescription=Enter a brief description on what you modified: 
git commit -m "%commitDescription%"

cls

echo Importing file from GitHub. Please wait...
git pull origin master
cls

echo Exporting file to GitHub....
git push origin setmoth
echo Export completed.

PAUSE