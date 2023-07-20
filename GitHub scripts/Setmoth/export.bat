@echo off
REM

cd ..
cd ..
git add --all
set /p commitDescription=Enter a brief description on what you modified:
git commit -m "%commitDescription%"

git pull origin master

git switch setmoth
git push origin master