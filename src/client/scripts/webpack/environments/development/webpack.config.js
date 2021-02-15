'use strict';

const webpack = require('webpack');
const path = require('path');
const autoprefixer = require('autoprefixer');
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const StylelintPlugin = require('stylelint-webpack-plugin');

const extractSass = new MiniCssExtractPlugin({
  filename: '[name].css',
  chunkFilename: '[id].css',
});

module.exports = (compilerOptions) => ({
  mode: 'development',
  entry: {
    common: path.resolve(__dirname, `./../../../../src/Project/${compilerOptions.project}/${compilerOptions.entry}`)
  },
  output: {
    filename: '[name].bundle.js',
    path: '/scripts/webpack/environments/development',
    publicPath: '/',
  },
  devtool: 'source-map',
  resolve: {
    extensions: ['.tsx', '.ts', '.js', '.css', '.scss', '.json'],
    alias: {
      'data-api-alias': path.resolve(process.cwd(), compilerOptions.jssDataApiAlias),
      Foundation: path.resolve(process.cwd(), './src/Foundation/'),
      Project: path.resolve(process.cwd(), './src/Project/'),
      Feature: path.resolve(process.cwd(), './src/Feature/'),
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
        test: /\.scss$/,
        use: [
          {
            loader: MiniCssExtractPlugin.loader,
          },
          {
            loader: 'css-loader',
          },
          {
            loader: require.resolve('postcss-loader'),
            options: {
              // Necessary for external CSS imports to work
              // https://github.com/facebookincubator/create-react-app/issues/2677
              ident: 'postcss',
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
              ],
            },
          },
          {
            loader: require.resolve('sass-loader'),
          },
        ],
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
    ],
  },
  plugins: [
    new ForkTsCheckerWebpackPlugin({
      async: false,
      include: ['/src/Project/', '/src/Feature/', '/src/Foundation/'],
      tsconfig: './tsconfig.dev.json',
      tslint: './tslint.json',
    }),
    new webpack.DefinePlugin({
      'process.env.NODE_ENV': JSON.stringify('development'),
      'process.env.ENV_URL': JSON.stringify(compilerOptions.envUrl),
      'process.env.API_KEY': JSON.stringify(compilerOptions.apiKey),
      'process.env.STATIC_CONTENT': JSON.stringify(compilerOptions.staticContent),
    }),
    extractSass,
    new webpack.NamedModulesPlugin(),
    new webpack.HotModuleReplacementPlugin(),
    new StylelintPlugin(),
  ],
});
