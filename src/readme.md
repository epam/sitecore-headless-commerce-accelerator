# Wooli

## Front-end

Front-end part of the project is build on the top of ReactJS and architectured by flux principles

### NPM development commands

In order to run the local dev server, you need to execute `npm start` in cmd or shell and pass required arguments, see more invormation in arguments table below
|  Argument Name | Description |
| --- | --- | --- |
| `project`| **name** from project's manifest file | Starts wepback local development server and use this first defined manifest file as per default. In order to |
| `env` | **name** of **env** from project's manifest file | |
| `static-content` | if specified local sitecore-context.json will be fetched | |

###### Examples

 `npm start -- --project=Wooli --env=local`, command runs local dev server and fetches a sitecore context from a url specified in **manifest** file of a project

`npm start -- --project=Wooli --env=local --static-content`, command runs local dev server and fetches a sitecore context from **sitecore-context.json**, but all calls to sitecore are forwarded to a url specified in **manifest** file of a project
