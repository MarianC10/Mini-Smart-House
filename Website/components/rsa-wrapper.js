const NodeRSA = require('node-rsa');
const fs = require('fs');

var key;

var generate = function () {
    key = new NodeRSA();
    key.generateKeyPair(2048, 65537);
    fs.writeFileSync('./keys/public-key.pem', key.exportKey('pkcs8-public-pem'));
};

var test = function () {
    let encrypted = encrypt('This is a test.');
    console.log('The encrypted message is: ' + encrypted);
    let decrypted = decrypt(encrypted);
    console.log('The decrypted message is: ' + decrypted);
}

var encrypt = function (message) {
    let encrypted = key.encrypt(message, 'base64');
    return encrypted;
};

var decrypt = function (cipherText) {
    let decrypted = key.decrypt(cipherText, 'utf8');
    return decrypted;
}

module.exports.generate = generate;
module.exports.test = test;
module.exports.encrypt = encrypt;
module.exports.decrypt = decrypt;
