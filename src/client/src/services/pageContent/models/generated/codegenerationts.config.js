var path = require('path');
var codeGenTsUtils = require('../../../../../scripts/code-generation/codeGenTsUtils');

module.exports = {
    cwd: path.join(__dirname, '../../../../../../'),
    pattern: 'Feature/PageContent/serialization/Templates/**/*.yml',
    reactJssModule: 'Foundation/ReactJss',
    modules: [],
    templatePath: path.join(__dirname, 'codegenerationts.tmpl'),
    ToClass: codeGenTsUtils.toClass,
    ToInterface: codeGenTsUtils.toInterface,
    ToProperty: codeGenTsUtils.toProperty,
    ToPropertyType: codeGenTsUtils.toPropertyType
}
