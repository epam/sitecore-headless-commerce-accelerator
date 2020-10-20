# escape=`
# hca folder should contains HCA-InventorySets.zip, HCA-Catalogs.zip, HCA-commerce-images-1.zip files

FROM mcr.microsoft.com/windows/nanoserver:1809

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY .\hca\* C:\hca\