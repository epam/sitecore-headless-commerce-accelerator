'use strict';
const Generator = require('yeoman-generator');
const fs = require('fs');
const rename = require('gulp-rename');
const chalk = require('chalk');
const yosay = require('yosay');
var path = require('path');
const settings = require('../configs/settings.json');

const msg = require('../configs/messages.json');
const helixLayers = require('../configs/helixLayers.json');
const SolutionFileExplorer = require('../../lib/solution-explorer');

module.exports = class SolutionGenerator extends Generator {
  constructor(args, opts) {
    super(args, opts);

    this.option('solutionName', {
      type: String,
      required: true,
      desc: 'The name of the solution.',
      default: opts.solutionName,
    });

    this.option('helixLayers', {
      type: String,
      required: true,
      desc: 'The layers (Feature|Foundation|Project) in a solution.',
      default: helixLayers[0].value,
    });

    this.rootTemplatePath = () => path.join(this.templatePath('../../'), 'templates');
    this.solutionTemplatePath = () => path.join(this.templatePath('../'), 'templates/SolutionTemplate.sln');
    this.solutionPath = () => path.join(this.rootTemplatePath(), 'src/SolutionName.sln');
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
      },
      {
        type: 'list',
        name: 'helixLayers',
        message: msg.helixLayers.prompt,
        choices: helixLayers,
        store: true,
        store: !this.options.helixLayers,
      },
    ]);

    this.options = { ...this.options, ...answers };
  }

  writing() {
    console.log('Starting: Update solution.');
    this._updateSolutionFile();

    console.log('Starting: Rename paths.');
    this.registerTransformStream(
      rename((path) => {
        path.basename = path.basename.replace(/SolutionName/g, this.options.solutionName);
        path.dirname = path.dirname.replace(/SolutionName/g, this.options.solutionName);
      }),
    );

    console.log('Starting: Copy templates.');
    this.fs.copyTpl(
      this.rootTemplatePath(),
      this.destinationPath(),
      { solutionName: this.options.solutionName },
      {},
      {
        globOptions: {
          ignore: this._getExcludedLayers().map((layer) => '**/src/' + layer + '/**/*'),
          dot: true,
        },
      },
    );
  }

  _getExcludedLayers() {
    return ['Foundation', 'Feature', 'Project'].filter((layer) => !this.options.helixLayers.includes(layer));
  }

  _updateSolutionFile() {
    const sourceSolution = new SolutionFileExplorer(fs.readFileSync(this.solutionPath(), 'utf8'));
    const projects = sourceSolution.getProjects(this.options.helixLayers);

    const templateSolution = new SolutionFileExplorer(fs.readFileSync(this.solutionTemplatePath(), 'utf8'));
    templateSolution.addProjects(projects, settings);

    fs.writeFileSync(this.solutionPath(), templateSolution.toString(), {
      encoding: 'utf8',
    });
  }

  async end() {
    console.log('Solution "' + chalk.green.bold(this.options.solutionName) + '" has been created.');
  }
};
