const argv = require('yargs').argv;

const prodConfigFactory = require('./scripts/webpack/environments/production');

// Wooli
const wooliManifest = require('./Project/Wooli/client/manifest.json');
const manifests = [wooliManifest];

module.exports = prodConfigFactory(manifests);
