#!/bin/bash
cd /var/www/API
dotnet restore
dotnet publish -c Release -o output
