#!/usr/bin/env bash

name=$1
template=$2

echo Creating project "$name"...

cp -r /app/template/$template ./$name

shopt -s globstar nullglob

cd ./$name
echo Applying substitutions...
# sed -i -e "s/__DT_PROJECT_NAME/${name}/g" ./$name/**/(.)
#find . -type f -exec grep -Iq . {} \; -exec sed -i "s/__DT_PROJECT_NAME/${name}/g" {} +

find . -type f |
while read filename
 sed -i "s/__DT_PROJECT_NAME/${name}/g" $filename
do
done

echo Finalizing project...
# Rename files with __DT_PROJECT_NAME
find . -name *__DT_PROJECT_NAME* |
while read filename
do
    mv "$filename" "${filename/__DT_PROJECT_NAME/$name}"
done
# Rename _gitignore into .gitignore
for i in ./**/_gitignore
do
    mv "$i" "${i/_gitignore/.gitignore}"
done

echo Done.
