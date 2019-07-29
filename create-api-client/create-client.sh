#!/usr/bin/env bash

# usage function
function usage()
{
   cat << HEREDOC

   Usage: $progname [--swag SWAGGER_ENDPOINT] [--lang LANG] [--output OUTPUT_FILE] [--namespace C# NAMESPACE]

   optional arguments:
     -h, --help                         show this help message and exit
     -s, --swag       SWAGGER_ENDPOINT  pass a valid endpoint to a swagger file
     -l, --lang       LANGUAGE          pass 'ts' for typescript, 'cs' for C#
     -n, --namespace  C# NAMESPACE      pass a valid c# namespace name to be used in the output
     -o, --output     OUTPUT_FILE       output file

HEREDOC
}

# initialize variables
progname=$(basename $0)
swag=
lang=
ns="MGLib"
vlang=
slang=
output=
tplExtCode=
swagCodeOpts=

# use getopt and store the output into $OPTS
# note the use of -o for the short options, --long for the long name options
# and a : for any option that takes a parameter
OPTS=$(getopt -o "hs:l:n:o:" --long "help,swag:,lang:,namespace:,output:" -n "$progname" -- "$@")
if [ $? != 0 ] ; then echo "Error in command line arguments." >&2 ; usage; exit 1 ; fi
eval set -- "$OPTS"

while true; do
  # uncomment the next line to see how shift is working
  # echo "\$1:\"$1\" \$2:\"$2\""
  case "$1" in
    -h | --help ) usage; exit; ;;
    -s | --swag ) swag="$2"; shift 2 ;;
    -l | --lang ) lang="$2"; shift 2 ;;
    -n | --namespace ) ns="$2"; shift 2;;
    -o | --output ) output="$2"; shift 2 ;;
    -- ) shift; break ;;
    * ) break ;;
  esac
done

tplExtCode="/app/tpl/${lang}/template.${lang}"

if [[ "$output" == "" ]]; then output="$PWD/$(basename $swag)_client.$lang"; fi

if [[ "$swag" != "" ]] && [[ "$lang" != "" ]]; then
  if [ "$lang" == "ts" ]; then
    vlang="Type Script"
    slang="swagger2tsclient"
    swagCodeOpts='/ClassName:{controller}Client /NullValue:Undefined /UseTransformOptionsMethod:true /ClientBaseClass:BaseClientProxy /Template:Fetch /PromiseType:Promise /DateTimeType:string /GenerateClientClasses:true /GenerateDtoTypes:true /OperationGenerationMode:MultipleClientsFromOperationId /MarkOptionalProperties:true /TypeStyle:Interface'
    swagCodeOpts="$swagCodeOpts /ExtensionCode:$tplExtCode"

  else
    if [ "$lang" == "cs" ]; then
      vlang="C#"
      slang="swagger2csclient"
      swagCodeOpts="/Namespace:$ns /ClassStyle:Poco /ArrayType:System.Collections.Generic.IEnumerable /UseHttpClientCreationMethod:true"
    fi
  fi
fi

if [ "$vlang" == "" ]; then usage; exit 1; fi

echo "$vlang client reading schema from: $swag and writing output to: $output"
#echo "[DEBUG] $swagCodeOpts"

mono /app/nswag/NSwag.exe $slang /Input:"$swag" /Output:"$output" $swagCodeOpts

if [ "$lang" == "cs" ]; then

  mv $output $output.temp
  echo "namespace $ns {" > $output
  cat $tplExtCode >> $output
  echo "}" >> $output
  cat $output.temp >> $output
  rm $output.temp

fi
