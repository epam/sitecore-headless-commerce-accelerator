'use strict';
const Generator = require('yeoman-generator');
const fs = require('fs');
const rename = require('gulp-rename');
const chalk = require('chalk');
const yosay = require('yosay');
const path = require('path');

const settings = require('../configs/settings.json');
const msg = require('../configs/messages.json');
const helixLayers = require('../configs/helix-layers.json');
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
    this.solutionFileTemplatePath = () => path.join(this.templatePath('../'), 'templates/SolutionTemplate.sln');
    this.solutionFilePath = () => path.join(this.rootTemplatePath(), 'src/SolutionName.sln');
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
        store: true
      },
    ]);

    this.options = { ...this.options, ...answers };
  }

  writing() {
    this.log('Starting: Update solution.');
    this._updateSolutionFile();

    this.log('Starting: Rename paths.');
    this.registerTransformStream(
      rename((path) => {
        path.basename = path.basename.replace(/SolutionName/g, this.options.solutionName);
        path.dirname = path.dirname.replace(/SolutionName/g, this.options.solutionName);
      }),
    );

    this.log('Starting: Copy templates.');
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
    const sourceSolution = new SolutionFileExplorer(fs.readFileSync(this.solutionFilePath(), 'utf8'));
    const projects = sourceSolution.getProjects(this.options.helixLayers);

    const templateSolution = new SolutionFileExplorer(fs.readFileSync(this.solutionFileTemplatePath(), 'utf8'));
    templateSolution.addProjects(projects, settings);

    fs.writeFileSync(this.solutionFilePath(), templateSolution.toString(), {
      encoding: 'utf8',
    });
  }

  async end() {
    this.log('Solution "' + chalk.green.bold(this.options.solutionName) + '" has been created.');
  }
};
