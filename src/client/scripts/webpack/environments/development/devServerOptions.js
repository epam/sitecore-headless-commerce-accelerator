module.exports = (envUrl, publicPath, host, port) => ({
  publicPath,
  host,
  port,
  contentBase: './scripts/webpack/environments/development/',
  hot: true,
  historyApiFallback: true,
  proxy: {
    // TODO: improve manifest with project specific proxy
    '/HCA/api/**': {
      target: envUrl,
      changeOrigin: true,
      secure: false,
    },
    '/apix/**': {
      target: envUrl,
      changeOrigin: true,
      secure: false,
    },
    '/sitecore/**': {
      target: envUrl,
      changeOrigin: true,
      secure: false,
    },
    '/-/{media,jssmedia}/**': {
      target: envUrl,
      changeOrigin: true,
      secure: false,
    },
    '/layouts/**': {
      target: envUrl,
      changeOrigin: true,
      secure: false,
    },
  },
  stats: {
    // fallback value for stats options when an option is not defined (has precedence over local webpack defaults)
    all: '*',
    // Add asset Information
    assets: true,
    // Sort assets by a field
    // You can reverse the sort with `!field`.
    assetsSort: 'field',
    // Add build date and time information
    builtAt: true,
    // Add information about cached (not built) modules
    cached: false,
    // Show cached assets (setting this to `false` only shows emitted files)
    cachedAssets: false,
    // Add children information
    children: false,
    // Add chunk information (setting this to `false` allows for a less verbose output)
    chunks: true,
    // Add built modules information to chunk information
    chunkModules: false,
    // Add the origins of chunks and chunk merging info
    chunkOrigins: true,
    // Sort the chunks by a field
    // You can reverse the sort with `!field`. Default is `id`.
    chunksSort: 'field',
    // Context directory for request shortening
    // context: '../src/',
    // `webpack --colors` equivalent
    colors: true,
    // Display the distance from the entry point for each module
    depth: false,
    // Display the entry points with the corresponding bundles
    entrypoints: true,
    // Add --env information
    env: true,
    // Add errors
    errors: true,
    // Add details to errors (like resolving log)
    errorDetails: true,
    // Add the hash of the compilation
    hash: true,
    // Set the maximum number of modules to be shown
    maxModules: 15,
    // Add built modules information
    modules: false,
    // Sort the modules by a field
    // You can reverse the sort with `!field`. Default is `id`.
    modulesSort: 'field',
    // Show dependencies and origin of warnings/errors (since webpack 2.5.0)
    moduleTrace: false,
    // Show performance hint when file size exceeds `performance.maxAssetSize`
    performance: true,
    // Show the exports of the modules
    providedExports: false,
    // Add public path information
    publicPath: false,
    // Add information about the reasons why modules are included
    reasons: false,
    // Add the source code of modules
    source: false,
    // Add timing information
    timings: true,
    // Show which exports of a module are used
    usedExports: false,
    // Add webpack version information
    version: false,
    // Add warnings
    warnings: true,
  },
});
