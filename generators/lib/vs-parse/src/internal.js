'use strict';

const fs = require('fs-extra');
const isBuffer = require('is-buffer');
const os = require('os');
const path = require('path');
const upath = require('upath');
const StringDecoder = require('string_decoder').StringDecoder;

const newLine = /\r|\n/g;
const defaultOptions = {
  encoding: 'utf-8',
};

const fileExistsSync = (filePath) => fs.pathExistsSync(filePath);
const fileExists = (filePath) => fs.pathExists(filePath);

const getFileDirectory = (filePath, options) => {
  const dir = isVsFileContents(filePath) || isBuffer(filePath) ? options.dirRoot : path.dirname(filePath);

  if (!dir) {
    throw new Error("Could not determine root directory. Please specify 'dirRoot' if doing a deep parse");
  }

  return dir;
};

const normalizePath = (pathStr) => (os.platform() == 'win32' ? pathStr : upath.normalize(pathStr));

const isVsFileContents = (file) => {
  // Naive way to determine if string is a path or vs proj/sln file
  return typeof file === 'string' && newLine.test(file);
};

const getFileContentsOrFail = (file, options) => {
  return new Promise((resolve, reject) => {
    if (isVsFileContents(file)) {
      resolve(file);
      return;
    }

    const myOptions = (options && Object.assign({}, options, defaultOptions)) || defaultOptions;

    if (isBuffer(file)) {
      const decoder = new StringDecoder(myOptions.encoding);
      const result = decoder.write(file);
      resolve(result);
      return;
    }

    return fs.readFile(file, myOptions).then(
      (result) => resolve(result),
      (err) => {
        if (err.code === 'ENOENT') reject(new Error('File not found: ' + file));
        else reject(err);
      },
    );
  });
};

const getFileContentsOrFailSync = (file, options) => {
  if (isVsFileContents(file)) {
    return file;
  }

  const myOptions = (options && Object.assign({}, options, defaultOptions)) || defaultOptions;

  if (isBuffer(file)) {
    const decoder = new StringDecoder(myOptions.encoding);
    return decoder.write(file);
  }

  try {
    return fs.readFileSync(file, myOptions);
  } catch (e) {
    if (typeof file === 'string' && !fileExistsSync(file)) {
      throw new Error('File not found: ' + file);
    } else {
      throw e;
    }
  }
};

module.exports = {
  getFileContentsOrFailSync,
  getFileContentsOrFail,
  fileExistsSync,
  fileExists,
  normalizePath,
  getFileDirectory,
};
