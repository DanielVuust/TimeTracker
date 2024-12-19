// File: main.cpp
#include <Arduino.h>
#include "DeviceManager.h"

// Define GPIO pins for the button matrix
uint8_t rowPins[4] = {2, 3, 4, 5};
uint8_t colPins[4] = {6, 7, 8, 9};

// Create an instance of DeviceManager
DeviceManager deviceManager;

void setup() {
    deviceManager.begin(rowPins, colPins);

    deviceManager.setButtonTask(0, 0, "Start");
    deviceManager.setButtonTask(1, 1, "Stop");
    deviceManager.setButtonTask(2, 2, "Reset");
    deviceManager.setButtonTask(3, 3, "Disconnect");

    Serial.begin(115200);
    Serial.println("ADBG: Device ID: " + deviceManager.getDeviceId());
}

void loop() {
    deviceManager.handleSerial(); // Process serial input
    deviceManager.handleButtons(); // Handle button presses
}