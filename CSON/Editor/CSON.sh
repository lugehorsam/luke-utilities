#!/bin/sh
.  /etc/profile

echo "processing cson in folder $1 to output folder $2"

for filename in $2*.json; do
  echo "removing file $filename"
  rm -f filename
done

for filename in $1*.cson; do
  outputfile=$2$(basename $filename "cson")json
  cson2json "$filename" > "$outputfile"
  echo "output file: $outputfile"
done

