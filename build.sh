#!/bin/bash
rm -rf AmigosSDK.*.nupkg && nuget pack AmigosSDK.nuspec && ls -1 *.nupkg | xargs -L1 -I{} cp {} AmigosSDK.latest.nupkg ;