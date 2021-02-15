const autoprefixer = require('autoprefixer');
const { resolve } = require('path');

module.exports = {
  stories: ['../../src/**/stories/**/*.stories.mdx', '../../src/**/stories/**/*.stories.@(js|jsx|ts|tsx)'],
  webpackFinal: (config) => {
    config.resolve.alias['Foundation'] = resolve(__dirname, '../../src/Foundation');

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
