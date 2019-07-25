#!/usr/bin/env bash

name=$1
template=$2

echo Creating project "$name"...

cp -r /app/template/$template ./$name

shopt -s globstar nullglob

echo Applying substitutions...
sed -i -e "s/__DT_PROJECT_NAME/${name}/g" ./$name/**/*.*

echo Finalizing project...
# Rename files with __DT_PROJECT_NAME
for i in ./$name/**/*__DT_PROJECT_NAME*
do 
    mv "$i" "${i/__DT_PROJECT_NAME/$name}"
done 

# Rename _gitignore into .gitignore
for i in ./$name/**/_gitignore
do 
    mv "$i" "${i/_gitignore/.gitignore}"
done 

echo Done!
