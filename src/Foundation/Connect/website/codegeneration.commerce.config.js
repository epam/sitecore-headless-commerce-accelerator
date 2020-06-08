var path = require("path");
var codeGenCsUtils = require('../../../scripts/code-generation/codeGenCsUtils');

module.exports = {
    cwd: path.join(__dirname, '..'),
    pattern: '**/serialization.commerce/Templates.Commerce*/**/*.yml',
    Using: ['System.CodeDom.Compiler', 'HCA.Foundation.GlassMapper.Models'],
    templatePath: path.join(__dirname, 'codegeneration.commerce.tmpl'),
    ToPropertyType: codeGenCsUtils.toPropertyType,
}