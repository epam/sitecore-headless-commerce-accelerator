const autoprefixer = require('autoprefixer');
const { resolve } = require('path');

const CaseSensitivePathsPlugin = require('case-sensitive-paths-webpack-plugin');

module.exports = {
  stories: ['../../src/**/stories/**/*.stories.mdx', '../../src/**/stories/**/*.stories.@(js|jsx|ts|tsx)'],
  webpackFinal: (config) => {
    config.resolve.extensions = ['.tsx', '.ts', '.js', '.css', '.scss'];
    config.resolve.alias['Feature'] = resolve(process.cwd(), './src/Feature');
    config.resolve.alias['Foundation'] = resolve(process.cwd(), './src/Foundation');
    config.resolve.alias['Project'] = resolve(process.cwd(), './src/Project');
    config.resolve.alias['data-api-alias'] = resolve(process.cwd(), './scripts/webpack/environments/development/JssDataApi');
    config.plugins = config.plugins.filter((plugin) => !(plugin instanceof CaseSensitivePathsPlugin));

    config.module.rules.push({
      test: /\.scss$/,
      use: [
        {
          loader: 'style-loader',
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
    });

    return config;
  },
};
