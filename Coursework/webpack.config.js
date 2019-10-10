let entryPoint = __dirname + '/App/index.jsx';
let bundleFolder = __dirname + '/wwwroot/assets';

module.exports = {
  entry: {
    main: entryPoint
  },
  devtool: 'source-map',
  output: {
    filename: 'bundle.js',
    publicPath: 'assets',
    path: bundleFolder
  },
  module: {
    rules: [
      {
        test: /\.jsx?$/,
        exclude: /node_modules/,
        loader: 'babel-loader'
      },
      {
        test: /\.s?css$/,
        loaders: ['style-loader', 'css-loader', 'sass-loader']
      },
      {
        test: /\.(jpe?g|gif|png|svg)$/,
        loader: 'file-loader'
      }
    ]
  },
  plugins: []
};
