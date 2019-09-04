var path = require("path");

module.exports = {
    cwd: path.join(__dirname, '..'),
    pattern: '**/serialization/Templates/**/*.yml',
    Using: ['System.CodeDom.Compiler', 'Epam.Sc.EngX.CodeGeneration.Domain'],
    templatePath: path.join(__dirname, 'codegeneration.tmpl'),
}