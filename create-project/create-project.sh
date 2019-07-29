#!/usr/bin/env bash

function usage()
{
   cat << HEREDOC

   Usage: dt-$progname [--name PROJECT_NAME] [--type PROJECT_KIND]

   optional arguments:
     -h, --help                       show this help message and exit
     -n, --name       PROJECT_NAME    pass a valid project name
     -t, --type       PROJECT_KIND    available kinds [react, netcore, mkdocs]

HEREDOC
}

# initialize variables
progname=$(basename $0 | cut -d. -f1)
name=
type=

# use getopt and store the output into $OPTS
# note the use of -o for the short options, --long for the long name options
# and a : for any option that takes a parameter
OPTS=$(getopt -o "hn:t:" --long "help,name:,type:" -n "$progname" -- "$@")
if [ $? != 0 ] ; then echo "Error in command line arguments." >&2 ; usage; exit 1 ; fi
if [ $# -eq 0 ]; then usage; exit 1; fi

eval set -- "$OPTS"

while true; do
  # uncomment the next line to see how shift is working
  # echo "\$1:\"$1\" \$2:\"$2\""
  case "$1" in
    -h | --help ) usage; exit; ;;
    -n | --name ) name="$2"; shift 2 ;;
    -t | --type ) type="$2"; shift 2 ;;
    -- ) shift; break ;;
    * ) break ;;
  esac
done

echo Creating project "$name"...

if [ ! -d "/app/template/$type" ]; then usage; exit 1; fi

cp -r /app/template/$type ./$name

shopt -s globstar nullglob

cd ./$name
echo Applying substitutions...

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
