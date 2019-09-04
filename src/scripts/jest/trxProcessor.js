const builder = require('jest-trx-results-processor');

const processor = builder({
  outputFile: './../output/tests/JestTestResults.xml'
});

module.exports = processor;
