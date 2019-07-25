#!/usr/bin/env bash

name=$1
template=$2

echo Creating project "$name"...

cp -r /app/template/$template ./$name

shopt -s globstar nullglob

echo Applying substitutions...
sed -i -e "s/__DT_PROJECT_NAME/${name}/g" ./$name/**/*.*

echo Finalizing project...
#rename "s/__DT_PROJECT_NAME/${name}/g" **/**

echo Done!
