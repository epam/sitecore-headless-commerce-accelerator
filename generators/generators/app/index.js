'use strict';
const Generator = require('yeoman-generator');
const rename = require("gulp-rename");
const chalk = require('chalk');
const yosay = require('yosay');
var path = require('path');

const msg = require('../configs/messages.json');
const settings = require('../configs/settings.json');

module.exports = class SolutionGenerator extends Generator {

  constructor(args, opts) {
    super(args, opts);

    this.option('solutionName', {
      type: String,
      required: true,
      desc: 'The name of the solution.',
      default: opts.solutionName,
    });

    this.rootTemplatePath = () => path.join(this.templatePath('../../'), 'templates');
  }

  // yeoman events
  initializing() {
    this.log(yosay(`Welcome to ${chalk.red.bold('Headless Commerce Starter Kit')} generator!`));
  }

  async prompting() {

    let answers = await this.prompt([
      {
        name: 'solutionName',
        message: msg.solutionName.prompt,
        default: this.appname,
        store: true,
        when: !this.options.solutionName,
      }
    ]);

    this.options = { ...this.options, ...answers };
  }

  writing() {

    this.registerTransformStream(rename((path) => {
      path.basename = path.basename.replace(/SolutionName/g, this.options.solutionName);
      path.dirname = path.dirname.replace(/SolutionName/g, this.options.solutionName)
    }));

    this.fs.copyTpl(
      this.rootTemplatePath(),
      this.destinationPath(),
      { solutionName: this.options.solutionName }
    );

  }

  async end() {
    console.log('Solution name ' + chalk.green.bold(this.options.solutionName) + ' has been created.');
  }
};
