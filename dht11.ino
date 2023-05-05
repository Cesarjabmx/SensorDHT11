#include "DHT.h"  //Añadimos la libreria con la cual trabaja nuestro sensor

#define DHTPIN 2     // Indicamos el pin donde conectaremos la patilla data de nuestro sensor

// Descomenta el tipo de sensor que vas a emplear. En este caso usamos el DHT11
#define DHTTYPE DHT11   // DHT 11 

DHT dht(DHTPIN, DHTTYPE);  //Indica el pin con el que trabajamos y el tipo de sensor
int maxh=0, minh=100, maxt=0, mint=100;  //Variables para ir comprobando maximos y minimos

void setup() 
{
  //Inicio comunicacion serie para ver los datos en el ordenador
  Serial.begin(9600); 
  //Mensaje de inicio
  Serial.println("Comprobacion sensor DHTxx:");
  //Iniciamos el sensor
  dht.begin();
}

void loop() 
{

  int h = dht.readHumidity();  //Guarda la lectura de la humedad en la variable float h
  int t = dht.readTemperature();  //Guarda la lectura de la temperatura en la variable float t

  // Comprobamos si lo que devuelve el sensor es valido, si no son numeros algo esta fallando
  if (isnan(t) || isnan(h)) // funcion que comprueba si son numeros las variables indicadas 
  {
    Serial.println("Fallo al leer del sensor DHT"); //Mostramos mensaje de fallo si no son numeros
  } else {
    //Mostramos mensaje con valores actuales de humedad y temperatura, asi como maximos y minimos de cada uno de ellos
    Serial.print("Humedad relativa: "); 
    Serial.print(h);
    Serial.print(" %\t");
    Serial.print("Temperatura: "); 
    Serial.print(t);
    Serial.println(" *C");

  }
  delay(1000);
}
