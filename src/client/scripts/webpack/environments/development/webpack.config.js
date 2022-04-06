'use strict';

const webpack = require('webpack');
const path = require('path');
const autoprefixer = require('autoprefixer');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

const extractSass = new MiniCssExtractPlugin({
  filename: '[name].css',
  chunkFilename: '[id].css',
});

module.exports = (compilerOptions) => {
  return {
    profile: true,
    mode: 'development',
    target: 'web',
    entry: {
      common: path.resolve(__dirname, `./../../../../src/bootstrap/${compilerOptions.entry}`),
    },
    output: {
      filename: '[name].bundle.js',
      path: '/scripts/webpack/environments/development',
      publicPath: '/',
    },
    devtool: 'source-map',
    resolve: {
      extensions: ['.tsx', '.ts', '.js', '.css', '.scss', '.json', '.webpack.js', '.web.js', '.mjs'],
      alias: {
        'data-api-alias': path.resolve(process.cwd(), compilerOptions.jssDataApiAlias),
        Foundation: path.resolve(process.cwd(), './src/Foundation/'),
        Project: path.resolve(process.cwd(), './src/Project/'),
        layouts: path.resolve(process.cwd(), './src/layouts/'),
        components: path.resolve(process.cwd(), './src/components/'),
        hooks: path.resolve(process.cwd(), './src/hooks/'),
        services: path.resolve(process.cwd(), './src/services/'),
        ui: path.resolve(process.cwd(), './src/ui/'),
        utils: path.resolve(process.cwd(), './src/utils/'),
        styles: path.resolve(process.cwd(), './src/styles/'),
        models: path.resolve(process.cwd(), './src/models/'),
        static: path.resolve(process.cwd(), './static/'),
        bootstrap: path.resolve(process.cwd(), './bootstrap/'),
      },
    },
    module: {
      rules: [
        {
          test: [/\.jpe?g$/, /\.png$/, /\.svg$/, /\.ttf$/, /\.otf$/, /\.eot/, /\.woff/, /\.gif/, /\.ico/],
          loader: require.resolve('url-loader'),
          options: {
            limit: 10000,
            name: '[name].[ext]',
          },
        },
        {
          test: /\.(ts|tsx)$/,
          exclude: /node_modules/,
          use: [
            {
              loader: 'babel-loader',
              options: {
                babelrc: false,
                plugins: ['react-hot-loader/babel'],
              },
            },
            {
              loader: require.resolve('ts-loader'),
              options: {
                // disable type checker - we will use it in fork plugin
                transpileOnly: true,
              },
            },
          ],
        },
        {
          test: /\.mjs$/,
          include: /node_modules/,
          type: 'javascript/auto',
        },
        {
          test: /\.css$/,
          use: [
            {
              loader: MiniCssExtractPlugin.loader,
            },
            {
              loader: 'css-loader',
            },
          ],
        },
        {
          test: /\.scss$/,
          use: [
            {
              loader: MiniCssExtractPlugin.loader,
            },
            {
              loader: 'css-loader',
              options: {
                minimize: {
                  // cssnano
                  preset: 'default',
                },
              },
            },
            {
              loader: require.resolve('postcss-loader'),
              options: {
                postcssOptions:{
                  plugins: () => [
                    require('postcss-flexbugs-fixes'),
                    require('postcss-object-fit-images'),
                    autoprefixer({
                      overrideBrowserslist: [
                        '>1%',
                        'last 4 versions',
                        'Firefox ESR',
                        'not ie < 9', // React doesn't support IE8 anyway
                      ],
                      flexbox: 'no-2009',
                    }),
                  ]
                },
              },
            },
            {
              loader: require.resolve('sass-loader'),
            },
          ],
        },
      ],
    },
    plugins: [
      new ForkTsCheckerWebpackPlugin({
        async: false,
        include: [
          '/src/Project/',
          '/src/Foundation/',
          '/src/components/',
          '/src/hooks/',
          '/src/services/',
          '/src/ui/',
          '/src/utils/',
          '/src/styles/',
          '/src/models/',
          '/src/bootstrap/',
        ],
        tsconfig: './tsconfig.dev.json',
        eslint: './.eslintrc',
      }),
      new webpack.DefinePlugin({
        'process.env.NODE_ENV': JSON.stringify('development'),
        'process.env.ENV_URL': JSON.stringify(compilerOptions.envUrl),
        'process.env.API_KEY': JSON.stringify(compilerOptions.apiKey),
        'process.env.STATIC_CONTENT': JSON.stringify(compilerOptions.staticContent),
        'process.env.TRACKING_ID': JSON.stringify('UA-187610105-1'),
      }),
      extractSass,
      new webpack.HotModuleReplacementPlugin(),
    ],
  };
};
