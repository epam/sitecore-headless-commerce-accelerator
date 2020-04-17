# HCA

## Front-end

Front-end part of the project is build on the top of ReactJS and architectured by flux principles

### package.json commands

In [package.json](package.json) file you can find most frequently used commands for development. To start commands use **npm**

| Command | Description |
| --- | --- |
| `start` | Starts local development server with configuration for project **HCA** and env **local**. Configuration information stores in [manifest.json](Project/HCA/client/manifest.json) file |
| `build:Debug` | Builds client code in Debug mode |
| `sc:codegen` | Starts *.ts and *.cs models generation for sitecore templates |
| `test` | Runs client tests |
| `test-watch` | Starts watching for new client tests and reruns them automatically |
| `test-cover` | Runs client tests with generation of coverage results |

###### Examples

 ```
 npm run start
 npm run sc:codegen
 ```
