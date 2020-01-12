#!/usr/bin/env bash

dotnet restore
dotnet build RawCMS.sln --verbosity minimal 
dotnet publish RawCMS/RawCMS.csproj --output /dist &&\
rm -rf RawCMS/Plugins/


ls -1 Plugins/*/*.csproj | while read folder; do 


    
    IFS='/' # hyphen (-) is set as delimiter
    read -ra parts <<<$folder # str is read into an array as tokens separated by IFS
    IFS=' ' # reset to default value after usage

echo "building $folder and plugin ${parts[1]}"
   dotnet publish $folder --output /plugins/${parts[1]}/
done

ls /plugins -lh

rm -rf /dist/Plugins
mkdir /dist/Plugins
cp -r /plugins/* /dist/Plugins

 echo "build output"
 ls /dist -lhs 
 echo "docker settings"
 cat /dist/appsettings.Docker.json  
 echo "plugins"
 ls /dist/Plugins -lhs 
 echo "plugin content" 
ls -1 /dist/Plugins | while read folder; do 
    echo "building $folder"
     ls  /dist/Plugins/$folder -lhs
done