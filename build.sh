#!/bin/bash
rm AmigosSDK.latest.nupkg && nuget pack AmigosSDK.nuspec && ls -1 *.nupkg | xargs -L1 -I{} mv {} AmigosSDK.latest.nupkg ;
