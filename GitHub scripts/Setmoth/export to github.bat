@echo off
REM

cd ..
cd ..

git switch setmoth
git add --all
set /p commitDescription=Enter a brief description on what you modified:
git commit -m "%commitDescription%"

git pull origin master

git push origin setmoth