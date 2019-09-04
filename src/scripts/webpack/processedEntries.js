const path = require('path');
const glob = require('glob');

module.exports = processedEntries = (globPath, buildFolder) => {
    const files = glob.sync(globPath);
    const entries = {};
  
    for (let i = 0; i < files.length; i++) {
      const entry = files[i];
      const buildFileName = path.basename(entry, path.extname(entry)).split('.')[0];
      const buildPath = path.join(path.dirname(entry), buildFolder, buildFileName);
      entries[buildPath] = entry;
    }
  
    return entries;
};