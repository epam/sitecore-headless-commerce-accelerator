var path = require("path");
var codeGenTsUtils = require("../../../scripts/code-generation/codeGenTsUtils");

module.exports = {
    cwd: path.join(__dirname, '..'),
    pattern: '**/serialization/Templates/**/*.yml',
    reactJssModule: 'Foundation/ReactJss/client',
    modules: [ 
        //{ Name: 'ReactJssModule', Path: 'Foundation/ReactJss/client'}
    ],
    templatePath: path.join(__dirname, 'codegenerationts.tmpl'),
    ToClass: codeGenTsUtils.toClass,
    ToInterface: codeGenTsUtils.toInterface,
    ToProperty: codeGenTsUtils.toProperty,
    ToPropertyType: codeGenTsUtils.toPropertyType
}
