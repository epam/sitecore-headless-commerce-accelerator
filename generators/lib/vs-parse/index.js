'use strict';

const lib = require('./src/lib.js');
const project = require('./src/csproj.js');
const sln = require('./src/sln.js');

module.exports = {
  parseSemverSync: lib.parseSemverSync,
  parsePackages: project.parsePackages,
  parsePackagesSync: project.parsePackagesSync,
  parseProject: project.parseProject,
  parseProjectSync: project.parseProjectSync,
  parseSolution: sln.parseSolution,
  parseSolutionSync: sln.parseSolutionSync,
};