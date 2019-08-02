const aesjs = require('aes-js');
const crypto = require('crypto');

var key, iv;

var importKey = function (hex) {
    hex = hex.replace(/-/g, '');
    key = Buffer.from(hex, 'hex');
}

var importIV = function (hex) {
    hex = hex.replace(/-/g, '');
    iv = Buffer.from(hex, 'hex');
}

var encrypt = function (message) {
    var textBytes = aesjs.utils.utf8.toBytes(message);

    var aesCbc = new aesjs.ModeOfOperation.cbc(key, iv);
    var encryptedBytes = aesCbc.encrypt(textBytes);

    var encryptedHex = aesjs.utils.hex.fromBytes(encryptedBytes);

    return encryptedHex;
}

var decrypt = function (cipherText) {
    ///var encryptedBytes = aesjs.utils.hex.toBytes(cipherText);
    if (key === undefined || iv === undefined)
        return "";

    cipherText = cipherText.replace(/-/g, '');

    var encryptedBytes = Buffer.from(cipherText, 'hex');

    // The cipher-block chaining mode of operation maintains internal
    // state, so to decrypt a new instance must be instantiated.
    var aesCbc = new aesjs.ModeOfOperation.cbc(key, iv);
    var decryptedBytes = aesCbc.decrypt(encryptedBytes);

    decryptedBytes = decryptedBytes.filter(function (number) {
        return (number >= 32 && number <= 127);
    });

    // Convert our bytes back into text
    var decryptedText = aesjs.utils.utf8.fromBytes(decryptedBytes);

    console.log(decryptedBytes);

    return decryptedText;
}

module.exports.importKey = importKey;
module.exports.importIV = importIV;
module.exports.encrypt = encrypt;
module.exports.decrypt = decrypt;
