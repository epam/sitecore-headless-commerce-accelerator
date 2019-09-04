const colors = require('colors');
const webpack = require('webpack');
const WebpackDevServer = require('webpack-dev-server');
const yargs = require('yargs');

const constants = require('./../constants');
const configBuilder = require('./webpack.config');
const devServerOptionsBuilder = require('./devServerOptions');
const { ManifestsManager } = require('./utils');

// import project manifests
const wooliManifest = require('./../../../../Project/Wooli/client/manifest');

// get params
const { project, env } = yargs.argv;
const staticContent = !!yargs.argv['static-content'];

const manifestsManager = new ManifestsManager([wooliManifest]);

try {
  const DEFAULT_PORT = 8080;
  const entry = manifestsManager.getClientEntry(project);
  const envUrl = manifestsManager.getEnvUrl(project, env);
  const apiKey = manifestsManager.getApiKey(project, env);

  // use dev provider for static content
  const jssDataApiAlias = !staticContent
    ? constants.jssDataApiImplementationProdPath
    : constants.jssDataApiImplementationDevPath;

  const configOptions = {
    apiKey,
    entry,
    envUrl,
    project,
    staticContent,
    jssDataApiAlias,
  };
  const config = configBuilder(configOptions);
  const options = devServerOptionsBuilder(envUrl, '/', 'localhost', DEFAULT_PORT);

  WebpackDevServer.addDevServerEntrypoints(config, options);
  const compiler = webpack(config);
  const server = new WebpackDevServer(compiler, options);

  server.listen(DEFAULT_PORT, 'localhost', (err) => {
    if (err) {
      return console.log(err);
    }

    console.log(colors.green.bold(`Listening ${DEFAULT_PORT}...`));
  });
} catch (e) {
  console.error(colors.red(e.message || 'Error occured in server.js'));
}
