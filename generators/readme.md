# Headless Commerce Accelerator Scaffold

[![npm version](https://badge.fury.io/js/hca.svg)](https://badge.fury.io/js/hca)

## OVERVIEW
The Headless Commerce Accelerator scaffold is a generator based on [Yeoman](http://yeoman.io/). 
It is designed to simplify and unify initial solution setup of [Headless Commerce Accelerator](https://github.com/epam/sitecore-headless-commerce-accelerator). 
It can save you time on configuration on early stages of the project.

#### Sitecore Versions
* 9.3 Initial Release

## FEATURES
* Generate a repository with a Headless Commerce Accelerator code with replaced solution name.
* Generate a partial layer solution: Foundation, Foundation & Feature, Foundation & Feature & Project

## INSTALLATION
See readme of [Headless Commerce Accelerator](https://github.com/epam/sitecore-headless-commerce-accelerator)

#### Initial generation
1. There are two possible ways to get generator:
    1. [npmjs.org] `npm i hca`
    2. [Manual]
        * Clone repository or update to current version, if you have it already.
        * In repository `%ROOT%\generators` run Powershell (as Administrator) command `npm link`
3. Navigate to a location where you usually store your projects. Create a directory with a customer name (by default folder name would be used as a solution name. Do not include spaces or any special characters in it)
4. Inside the folder run following PS command (again as Administrator) and follow instructions of the generator.

``` powershell
npm i yo -g
npm i hca -g
yo hca
```
5. Follow the generator instructions.

#### Re-generation
It is possible to run generation once again. Yeoman will detect conflicts and provide you with options to discard or override changes.

## FEEDBACK
If you had some issues during installation, you have ideas or feedback, please, post them to [GitHub issues section of this project](https://github.com/epam/sitecore-headless-commerce-accelerator/issues)
