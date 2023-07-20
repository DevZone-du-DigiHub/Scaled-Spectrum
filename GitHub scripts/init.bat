@echo off
REM

cd ..

echo Initilizing folder.
git init

cls

echo Adding project path.
git remote add origin git@github.com:DevZone-du-DigiHub/Scaled-Spectrum.git

cls

echo Importing project.
git pull origin master

cls

echo Importing branch name.
git fetch --all

cls

echo Initilization completed.
PAUSE