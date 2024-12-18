#include <Arduino.h>
#include "DeviceManager.h"

// Define GPIO pins for the button matrix
uint8_t rowPins[4] = {2, 3, 4, 5}; // Example row pins
uint8_t colPins[4] = {6, 7, 8, 9}; // Example column pins

// Create an instance of DeviceManager
DeviceManager deviceManager;

void setup() {
    deviceManager.begin(rowPins, colPins);

    deviceManager.setButtonTask(0, 0, "Start");   // Button at row 0, col 0 triggers "Start"
    deviceManager.setButtonTask(1, 1, "Stop");    // Button at row 1, col 1 triggers "Stop"
    deviceManager.setButtonTask(2, 2, "Reset");   // Button at row 2, col 2 triggers "Reset"
    deviceManager.setButtonTask(3, 3, "Confirm"); // Button at row 3, col 3 triggers "Confirm"

    Serial.begin(9600);
    Serial.println("Device ID: " + deviceManager.getDeviceId());
}

void loop() {
    deviceManager.handleSerial(); // Process serial input
    deviceManager.handleButtons(); // Handle button presses
}



// TODO: {"row":1,"col":1,"status":"Pause"} at ESP8266