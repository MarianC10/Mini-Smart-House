var app = require('express')();
var server = require('http').Server(app);
const SerialPort = require('serialport');
const Readline = require('@serialport/parser-readline');
var io = require('socket.io')(server);
var fs = require('fs');
const rsaWrapper = require('./components/rsa-wrapper.js');
const aesWrapper = require('./components/aes-wrapper.js');

var port_opened = 0;
const port = new SerialPort('COM3', { baudRate: 9600 });

//PARSER PENTRU DATELE DIN SERIAL
const parser = port.pipe(new Readline({ delimiter: '\n' }));

server.listen(80);


//primire date meteo
parser.on('data', function(data) {
    console.log(data);
    fs.writeFile('date_meteo.txt',data,(er)=>{
        if(er){
            console.log(er);
        }
    })
});

port.on('open', () => {
    port_opened = 1;
    console.log('port open');
    //setInterval(interval_meteo,5000);

});

rsaWrapper.generate();

//Variablile case
var armata = false;
var usa_intrare = false;
var usa_garaj = false;
var bec_sufragerie = 0;
var bec_dormitor = 0;
var termostat = false;
var timp_dormitor = 0;
var timp_sufragerie=0;
var timp_total=0;

///Datele primite de la comanda vocala
function interval_read() {
    fs.readFile('./text/key.txt', 'utf-8', (err, data) => {
        if (err) throw err;
        data = data.trim();
        if (data !== '') {
            var key = rsaWrapper.decrypt(data);
            aesWrapper.importKey(key);
            console.log(key);
            fs.writeFileSync('./text/key.txt', '');
        }
      });
    fs.readFile('SpeechToText.txt', 'utf-8', (err, data) => {
        if (err) throw err;
        fs.readFile('./text/iv.txt', 'utf-8', (error, iv) => {
            if (error) throw error;

            aesWrapper.importIV(iv);

            let decrypted = aesWrapper.decrypt(data).trim();

            if (decrypted != '') {
                ///console.log('The decrypted message is: ' + decrypted);
                switch (decrypted) {
                    case "livingroom light off":
                        bec_sufragerie=0;
                        console.log("Livingroom light off, Sir");
                        port.write("w");
                        break;
                    case "bedroom light on":
                        bec_dormitor=true;
                        console.log("Bedroom light on, Sir");
                        port.write("e");
                        break;
                    case "bedroom light off":
                        bec_dormitor=false;
                        console.log("Bedroom light off, Sir");
                        port.write("r");
                        break;
                    case "open garage":
                        usa_garaj=true;
                        console.log("Garage opened, Sir");
                        port.write("t");
                        break;
                    case "close garage":
                        usa_garaj=false;
                        console.log("Garage closed, Sir");
                        port.write("y");
                        break;
                    case "study light":
                        bec_sufragerie=3;
                        console.log("Study light, Sir");
                        port.write("c");
                        break;
                    case "evening light":
                        bec_sufragerie=5;
                        console.log("evening light, Sir");
                        port.write("x");
                        break;
                    case "relaxing light":
                        bec_sufragerie=1;
                        console.log("relaxing light, Sir");
                        port.write("z");
                        break;
                    case "open the door":
                        usa_intrare=true;
                        console.log("door opened,Sir");
                        port.write("a");
                        break;
                    case "close the door":
                        usa_intrare=false;
                        console.log("closed door, Sir");
                        port.write("s");
                        break;

                }
                fs.writeFile('SpeechToText.txt', '', () => {
                    console.log("Done");
                });
            }
        });
    });
}
setInterval(interval_read, 500);

app.get('/', function (req, res) {
    res.sendFile(__dirname + '/index.html');
});

app.get('/consumption', function (req, res) {
    res.sendFile(__dirname + '/chart.html');
});

///Date transmise prin Socket.IO
io.on('connection', function (socket) {
    socket.emit('news', { hello: 'world' });
    socket.on('my other event', function (data) {
        console.log(data);
    });
    socket.on('casa armata', function (data) {
        armata=true;
        console.log(data);
        port.write("p");
        armata = 1;
    });
    socket.on('casa dezarmata', function (data) {
        armata=false;
        console.log(data);
        port.write("l");
        armata = 0;
    });
    socket.on('termostat pornit', (data) => {
        termostat=!termostat;
        console.log(data);
        port.write("f");

    });
    socket.on('lumina dormitor aprinsa', (data) => {
        bec_dormitor=true;
        console.log(data);
        port.write("e");
    });
    socket.on('lumina dormitor stinsa', (data) => {
        bec_dormitor=false;
        console.log(data);
        port.write("r");
    });
    socket.on('lumina sufragerie seara', (data) => {
        console.log(data);
        port.write("x");
        bec_sufragerie=5;
    });
    socket.on('lumina sufragerie relaxare',(data)=>{
        console.log(data);
        port.write("z");
        bec_sufragerie=1;
    });
    socket.on('lumina sufragerie studiu',(data)=>{
        console.log(data);
        port.write("c");
        bec_sufragerie=3;
    });
    socket.on('lumina sufragerie stinsa', (data) => {
        console.log(data);
        bec_sufragerie=0;
        port.write("w");
    });
    socket.on('garaj deschis', (data) => {
        console.log(data);
        port.write("t");
        usa_garaj=true;
    });
    socket.on('garaj inchis', (data) => {
        console.log(data);
        port.write("y");
        usa_garaj=false;
    });
    socket.on('usa deschisa', (data) => {
        console.log(data);
        port.write("a");
        usa_intrare=true;
    });
    socket.on('usa inchisa', (data) => {
        console.log(data);
        port.write("s");
        usa_intrare=false;
    });
    socket.on('reset', (data)=>{
        timp_total=0;
        timp_dormitor=0;
        timp_sufragerie=0;
    });


});

setInterval(function() {
    if (bec_sufragerie!=0) {
        timp_sufragerie+=5;
        timp_total+=5;
    }
    if (bec_dormitor!=0) {
        timp_dormitor+=5;
        timp_total+=5;
    }
    io.sockets.emit('usa_garaj', { val: usa_garaj });
    io.sockets.emit('usa_casa', { val: usa_intrare });
    io.sockets.emit('armata', { val: armata });
    io.sockets.emit('bec_dormitor', { val: bec_dormitor });
    io.sockets.emit('bec_sufragerie', { val: bec_sufragerie });
    io.sockets.emit('termostat', { val: termostat });
    io.sockets.emit('consum', {total:timp_total, dormitor:timp_dormitor, sufragerie:timp_sufragerie});
}, 5000);
