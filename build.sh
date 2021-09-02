#!/bin/bash
currentVer=`pcregrep -o1  "version>([0-9\.]+).*" AmigosSDK.nuspec`
nextVer=`echo "$currentVer" | perl -pe 's/^((\d+\.)*)(\d+)(.*)$/$1.($3+1).$4/e'`
sed -i '' "s/$currentVer/$nextVer/" AmigosSDK.nuspec
ls -1 *.nupkg | xargs -L1 -I{} git rm --ignore-unmatch --quiet {};
rm -rf AmigosSDK.*.nupkg && nuget pack AmigosSDK.nuspec