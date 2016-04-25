#include <Encoder.h>

//digital input pins
const int input_1 = 2;
const int input_2 = 3;

const int frequency = 120; //in Hz
float encVal, loopTime, currTime, prevTime;
float milliseconds = 0, microseconds = 0, seconds = 0;
boolean boolPrinting = false;
const float fEncoderSensitivity=0.17578125;
Encoder encoder(input_1, input_2);

int counter = 0;
float start, finish;


void setup() {
  Serial.begin(19200);
  boolPrinting = true;
  loopTime = calculateLoop((float)frequency);
  prevTime = getTime();
  start = getTime();
}

void loop() {
  currTime = getTime();
  if(currTime - prevTime >= loopTime){
    encVal = encoder.read()*fEncoderSensitivity;
    if(boolPrinting){
      Serial.println(encVal,DEC);
      /*counter++;
      if(counter >= 120){
        finish = getTime() - start;
        
        Serial.println(finish);
        Serial.println(start);
        Serial.print("FREQUENCY: ");
        Serial.println((finish / 1000)/counter);
        start = getTime();
        counter = 0;
      }*/
    }
    prevTime = currTime;
  }
  
}
float calculateLoop(float frequency){
  float value = (1/frequency)*1000;
 // Serial.print("LOOP TIME: ");
  //Serial.println(value);
  return value;
}

float getTime (){
  milliseconds = millis();
  microseconds = micros();
  return milliseconds + (microseconds/1000);
}



