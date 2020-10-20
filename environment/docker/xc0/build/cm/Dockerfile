# escape=`

ARG BASE_IMAGE
ARG SPE_IMAGE
ARG JSS_IMAGE
ARG HCA_IMAGE

FROM ${SPE_IMAGE} as spe
FROM ${JSS_IMAGE} as jss
FROM ${HCA_IMAGE} as hca
FROM ${BASE_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

COPY --from=hca C:\hca C:\hca

WORKDIR C:\inetpub\wwwroot

# Add SPE module
COPY --from=spe C:\module\cm\content C:\inetpub\wwwroot

# Add JSS module
COPY --from=jss C:\module\cm\content C:\inetpub\wwwroot
COPY --from=jss C:\module\tools C:\module\tools
RUN C:\module\tools\Initialize-Content.ps1 -TargetPath C:\inetpub\wwwroot; `
    Remove-Item -Path C:\module -Recurse -Force;

COPY .\transforms C:\transforms
RUN C:\transforms\modifySpeConfig.ps1