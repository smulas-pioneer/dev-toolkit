#!/usr/bin/env bash

function showusage(){
  echo "create-project [projectName] [template: react/netcore]";
}

name=$1
template=$2

if [[ "$name" == "" ]]; echo Mssing project name!; showusage(); exit;
if [[ "$template" == "" ]]; echo Mssing template!; showusage(); exit;

echo Creating project "$name"...

cp -r /app/template/$template ./$name

shopt -s globstar nullglob

cd ./$name
echo Applying substitutions...
# sed -i -e "s/__DT_PROJECT_NAME/${name}/g" ./$name/**/(.)
#find . -type f -exec grep -Iq . {} \; -exec sed -i "s/__DT_PROJECT_NAME/${name}/g" {} +

find . -type f |
while read filename
do
 sed -i "s/__DT_PROJECT_NAME/${name}/g" $filename
done

echo Finalizing project...

# Rename files with __DT_PROJECT_NAME (folders/files)
find . -type d -name '*__DT_PROJECT_NAME*' |
while read filename
do
    mv "$filename" "${filename/__DT_PROJECT_NAME/$name}"
done

find . -type f -name '*__DT_PROJECT_NAME*' |
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
