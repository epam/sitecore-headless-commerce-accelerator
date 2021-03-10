const builder = require('jest-trx-results-processor');
const fs = require('fs');
const path = require('path');

const outputFile = './../../output/tests/JestTestResults.xml';

const outputFilePath = path.resolve(__dirname, '../..', outputFile);
const outputDirname = path.dirname(outputFilePath);

if (!fs.existsSync(outputDirname)) {
  fs.mkdirSync(outputDirname, { recursive: true });
}

const processor = builder({
  outputFile,
});

module.exports = processor;
