#define led_dormitor 7
#define pin_garaj 5
#define pin_usa 6
#define pin_buzzer 4
#define pin_ultrasunete_triq 3
#define pin_ultrasunete_echo 2
#define senzor_umiditate 12
#define pin_termostat 8

#include <Servo.h>
#include <dht.h>

dht DHT;

Servo usa_garaj, usa_casa;

long duration;
int distance, numar_iterati=400, umiditate, temperatura;
char sir[100], auxsir[100];
int red=11;
int green=10;
int blue=9;
bool armat=false;

void setup(){
  usa_garaj.attach(pin_garaj);
  usa_casa.attach(pin_usa);
  usa_garaj.write(20);
  usa_casa.write(20);
  pinMode(red,OUTPUT);
  pinMode(blue,OUTPUT);
  pinMode(pin_termostat,OUTPUT);
  pinMode(green,OUTPUT);
  pinMode(led_dormitor,OUTPUT);
  pinMode(pin_ultrasunete_triq,OUTPUT);
  pinMode(pin_ultrasunete_echo,INPUT);
  Serial.begin(9600);
  delay(500);//Delay to let system boot
 
}

void pornire_dormitor()
{
  digitalWrite(led_dormitor,HIGH);
}

void oprire_dormitor()
{
  digitalWrite(led_dormitor,LOW);
}

void alarma()
{
  tone(pin_buzzer,1000,5000);
}

void citire_senzor_intrare()
{
    digitalWrite(pin_ultrasunete_triq, LOW);
    delayMicroseconds(2);
    digitalWrite(pin_ultrasunete_triq, HIGH);
    delayMicroseconds(10);
    digitalWrite(pin_ultrasunete_triq, LOW);

    duration = pulseIn(pin_ultrasunete_echo, HIGH);

    distance= duration*0.034/2;

    if (distance<15)
    {
      alarma();
      
    }
}

void deschis_garaj()
{
  usa_garaj.write(110);
  delay(1000);
}

void inchis_garaj()
{
  usa_garaj.write(20);
  delay(1000);
}

void deschis_casa()
{
  usa_casa.write(110);
  delay(1000);
}

void inchis_casa()
{
  usa_casa.write(20);
  delay(1000);
}

void efect_relaxare()
{
  analogWrite(red,255);
  analogWrite(blue,0);
  analogWrite(green,0);
}

void efect_citire()
{
  digitalWrite(red,LOW);
  digitalWrite(blue,HIGH);
  digitalWrite(green,HIGH);
}

void efect_seara()
{
  digitalWrite(red,LOW);
  digitalWrite(blue,LOW);
  digitalWrite(green,HIGH);
}

void oprire_sufragerie()
{
  analogWrite(red,0);
  analogWrite(blue,0);
  analogWrite(green,0);
}

void schimbare_stare_termostat()
{
  digitalWrite(pin_termostat,HIGH);
  delay(100);
  digitalWrite(pin_termostat,LOW);
}


 
void loop(){
  //Start of Program 
  if (Serial.available()>0)
  {
    String s=Serial.readString();
    char c=s[0];
    if (c=='z')
    {
      efect_relaxare();
    }
    else if (c=='x')
    {
      efect_seara();
    }
   else  if (c=='c')
    {
      efect_citire();
    }
    else if (c=='w')
    {
       oprire_sufragerie();
    }
    else if (c=='e')
    {
       pornire_dormitor();
    }
    else if (c=='r')
    {
      oprire_dormitor();
    }
    else if (c=='t')
    {
      deschis_garaj();
    }
    else if (c=='y')
    {
      inchis_garaj();
    }
    else if (c=='a')
    {
      deschis_casa();
    }
    else if (c=='s')
    {
      inchis_casa();
    }
    else if (c=='p')
    {
      armat=true;
    }
    else if (c=='l')
    {
      armat=false;
    }
    else if (c=='f')
    {
      schimbare_stare_termostat();
    }
  }
  if (armat==true)
  {
    citire_senzor_intrare();
  }
  if (numar_iterati==500)
  {
    int chk=DHT.read11(senzor_umiditate);
    umiditate=(int)DHT.humidity;
    temperatura=(int)DHT.temperature;
    sprintf(sir,"Current humidity is %d", umiditate);
    sprintf(auxsir,"%s percent and temperature is %d",sir,temperatura);
    sprintf(sir,"%s degrees Celsius",auxsir);
    Serial.println(sir);
    for (int i=0; i<100; i++)
    {
      sir[i]=0;
      auxsir[i]=0;
    }
    numar_iterati=0;
    delay(2000);
  }
  numar_iterati++;
  delay(100);
}// end loop() 
