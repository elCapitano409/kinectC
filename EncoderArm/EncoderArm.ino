#include <Encoder.h>

//digital input pins
const int input_1 = 3;
const int input_2 = 2;

unsigned long currTime, prevTime, loopTime;
const float sensitivity =0.17578125;
const float frequency = 116.0; //in Hz
float encVal;
boolean boolPrinting = false;

int counter = 0;
Encoder encoder(input_1, input_2);

void setup() {
  prevTime = 0;
  Serial.begin(19200); //opens serial
  boolPrinting = true;
  loopTime = (unsigned long)calculateLoop(frequency);
  //Serial.println(loopTime);
}

void loop() {
  currTime = millis();
  //Serial.println(currTime - prevTime);
  if(currTime - prevTime >= (unsigned long) 9){
    encVal = encoder.read();
    if(boolPrinting){
      Serial.println(encVal * sensitivity,DEC);
    }
    prevTime = currTime;
  }
}

//caculates period
float calculateLoop(float frequency){
  float value = (1.0/frequency)*1000.0;
  //Serial.println(value);
  return float(value);
}


