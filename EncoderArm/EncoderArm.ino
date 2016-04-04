#include <Encoder.h>

//digital input pins
const int input_1 = 3;
const int input_2 = 2;

unsigned long currTime, prevTime, loopTime;
const float sensitivity =0.17578125;
const int frequency = 120; //in Hz
float encVal;
boolean boolPrinting = false;
Encoder encoder(input_1, input_2);

void setup() {
  Serial.begin(9600); //opens serial
  boolPrinting = true;
  loopTime = calculateLoop(frequency);
}

void loop() {
  currTime = millis();
  if(currTime - prevTime >= loopTime){
    encVal = encoder.read();
    if(boolPrinting){
      Serial.println(encVal * sensitivity,DEC);
    }
  }
  prevTime = currTime;
}

//caculates period
unsigned long calculateLoop(int frequency){
  int value = (1/frequency)*1000;
  return long(value);
}


