const argv = require('yargs').argv;

const prodConfigFactory = require('./scripts/webpack/environments/production');

// HCA
const hcaManifest = require('./src/bootstrap/manifest.json');
const manifests = [hcaManifest];

module.exports = prodConfigFactory(manifests);
