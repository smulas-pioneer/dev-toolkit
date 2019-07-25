#!/usr/bin/env bash

name=$1
template=$2

cp -r /app/template/$template ./$name

shopt -s globstar nullglob

sed -i -e "s/__DT_PROJECT_NAME/${name}/g" ./$name/**/*.*

rename "s/__DT_PROJECT_NAME/${name}/g" **/**
