#!/bin/node

const { resolve } = require('path');
const { createReadStream } = require('fs');
const { createInterface } = require('readline');

const regex = /^(((US)\d+(\-((TA)|(SB))\d+)?)|(BUG\d+))( \[P\])? [A-Z]{1}[\w\s\d,.'\-\+=\!\?\|]*$/;

const run = async () => {
  const path = resolve(__dirname, '../../..', process.env.HUSKY_GIT_PARAMS);
  const firstLine = await getFirstLine(path);

  if (!regex.test(firstLine)) {
    console.log('ERROR: Bad commit message.');
    console.log(
      `The proper format is:
        "US<Number> | BUG<Number> <Message>"
        "US<Number>-TA<Number> | US<Number>-SB<Number> <Message>"

       Where Message starts from capital letter.`,
    );

    process.exit(1);
  }
};

const getFirstLine = async (pathToFile) => {
  const readable = createReadStream(pathToFile);
  const reader = createInterface({ input: readable });

  const line = await new Promise((resolve) => {
    reader.on('line', (line) => {
      reader.close();
      resolve(line);
    });
  });

  readable.close();

  return line;
};

run();
