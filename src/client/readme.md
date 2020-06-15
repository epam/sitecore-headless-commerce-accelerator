# HCA Front-end

### package.json commands

In [package.json](package.json) file you can find most frequently used commands for development. To start commands use **npm**

| Command | Description |
| --- | --- |
| `start` | Starts local development server with configuration for project **HCA** and env **local**. Configuration information stores in [manifest.json](src/Project/HCA/manifest.json) file |
| `build:Debug` | Builds client code in Debug mode |
| `sc:codegen` | Starts *.ts models generation for sitecore templates |
| `sc:codegen:commerce` | Starts *.ts models generation for sitecore templates from commerce |
| `test` | Runs client tests |
| `test-watch` | Starts watching for new client tests and reruns them automatically |
| `test-cover` | Runs client tests with generation of coverage results |

###### Examples

 ```
 npm run start
 npm run sc:codegen
 ```
