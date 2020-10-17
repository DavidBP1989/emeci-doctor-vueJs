const htmlWebpackPlugin = require('html-webpack-plugin');
const vueLoaderPlugin = require('vue-loader/lib/plugin');
const fs = require('fs');
const path = require('path');
const webpack = require('webpack');

const appBasePath = './ClientApp/src/views/';
//var jsEntries = {};

//fs.readdirSync(appBasePath).forEach(function (name) {
//    var indexFile = appBasePath + name + '/app.js';
//    if (fs.existsSync(indexFile))
//        jsEntries[name] = indexFile;
//});

module.exports = {
    mode: 'production',
    //entry: jsEntries,
    entry: './ClientApp/src/app.js',
    output: {
        path: path.resolve(__dirname, './src/dist'),
        publicPath: '/src/dist',
        filename: 'app.js'
    },
    module: {
        rules: [
            { test: /\.js$/, use: 'babel-loader' },
            { test: /\.vue$/, use: 'vue-loader' },
            { test: /\.css$/, use: ['vue-style-loader', 'css-loader'] },
            {
                test: /\.s[ac]ss$/i,
                use: [
                    'vue-style-loader',
                    'css-loader',
                    'sass-loader'
                ]
            },
            {
                test: /\.(jpg|png)$/i,
                use: {
                    loader: 'url-loader'
                }
            }
        ]
    },
    plugins: [
        new htmlWebpackPlugin(),
        new vueLoaderPlugin(),
        //new webpack.DefinePlugin({
        //    'API_URL_AUTHENTICATION': JSON.stringify('https://localhost:44323'),
        //    'API_URL': JSON.stringify('https://localhost:44323/api')
        //})
        new webpack.DefinePlugin({
            'API_URL_AUTHENTICATION': JSON.stringify('https://emeci.com/doctorapi'),
            'API_URL': JSON.stringify('https://emeci.com/doctorapi/api')
        })
    ]
};