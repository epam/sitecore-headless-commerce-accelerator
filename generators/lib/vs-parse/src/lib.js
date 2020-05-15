'use strict';

const semverUtils = require('semver-utils');

const nthIndexOfChar = (haystack, needle, n) => {
  let count = 0;

  for(let i = 0; i < haystack.length; i++) {
    if(haystack[i] === needle) {
      count++;
    }

    if(count === n) {
      return i;
    }
  }

  return -1;
}

const parseSemverSync = (versionString) => {
  if(!versionString) {
    return null;
  }

  const i = nthIndexOfChar(versionString, '.', 3);
  const myVersionString = i === -1 ? versionString : versionString.substring(0, i);
  const parsed = semverUtils.parse(myVersionString);

  return parsed ? {
    major: parsed.major,
    minor: parsed.minor,
    patch: parsed.patch,
    version: parsed.version,
    originalString: versionString,
    toString: () => parsed.version
  } : null;
};


module.exports = {
  parseSemverSync,
};
