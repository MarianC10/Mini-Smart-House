var express = require('express');
const SerialPort = require('serialport');
var fs = require('fs');
var app=express();

var port_opened=0;
const port = new SerialPort('COM3', { baudRate: 9600 });

app.get('/',(req,res)=>{
    res.sendFile(__dirname+'/index.html');
});


app.post('/sapte',(res,req)=>{
    if (port_opened==1)
    {
        console.log("7 sent");
        port.write("r");
    }
});

app.post('/opt',(res,req)=>{
    if (port_opened==1)
    {
        console.log("8 sent");
        port.write("g");
    }
});

port.on('open',()=>{
    port_opened=1;
  console.log('port open');

});

function interval_read(){
    fs.readFile('SpeechToText.txt',(err,data)=>{
        if (err)
        {
            console.log(err);
        }
        if (data.toString().trim()!=''){
            switch(data.toString().trim())
            {
                case "livingroom light on":
                    console.log("Livingroom light on, Sir");
                    port.write("q");
                    break;
                case "livingroom light off":
                    console.log("Livingroom light off, Sir");
                    port.write("w");
                    break;
                case "bedroom light on":
                    console.log("Bedroom light on, Sir");
                    port.write("e");
                    break;
                case "bedroom light off":
                    console.log("Bedroom light off, Sir");
                    port.write("r");
                    break;
                case "open garage":
                    console.log("Garage opened, Sir");
                    port.write("t");
                    break;
                case "close garage":
                    console.log("Garage closed, Sir");
                    port.write("y");
                    break;
            }
            fs.writeFile('SpeechToText.txt','',()=>{
                console.log("Am facut ce mi-ai spus STAPANEEE");
            });
        }
    });
}
setInterval(interval_read,500);



server.listen(3000);