#include <Arduino.h>
#include <ArduinoJson.h>
#include <WiFiManager.h>
#include <DeviceManager.h>
#include "DataObject.h"

WiFiManager wifiManager;
DeviceManager deviceManager(wifiManager);

// UnitID to determine cadence
const int unitId = 1;  
// Total slots in 20 minutes (20 * 60 seconds)
const int slotInterval = 1200; 
unsigned long previousMillis = 0;

void setup() {
    Serial.begin(115200);
    wifiManager.begin();
}

void loop() {
    wifiManager.handle();
    deviceManager.handleSerial();

    unsigned long assignedSecond = unitId % slotInterval;
    unsigned long currentMillis = millis();
    unsigned long currentSecond = (currentMillis / 1000) % slotInterval; 

    if (currentSecond == assignedSecond && wifiManager.isConnected()) {
        const auto& objects = deviceManager.getObjectList();
        if (!objects.empty()) {
            bool allSent = true;

            for (const auto &obj : objects) {
                if (!wifiManager.sendDataToUrl("http://localhost:61083/api/arduino/" + obj.deviceId + "/log", obj, "deviceId")) {
                    allSent = false;
                    break;
                }
            }

            if (allSent) {
                wifiManager.debugPrintln("EDBG: All data successfully sent to API.");
                deviceManager.clearObjectList();
            } else {
                wifiManager.debugPrintln("EDBG: Failed to send all data to API.");
            }
        }
    }

    delay(100);
}


// TODO: {"row":1,"col":1,"status":"Pause"} at ESP8266