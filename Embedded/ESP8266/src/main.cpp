#include <Arduino.h>
#include <ArduinoJson.h>
#include "WiFiManager.h"
#include "SerialManager.h"
#include "DataObject.h"

WiFiManager wifiManager;
SerialManager serialManager(wifiManager);

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
    serialManager.handleSerial();

    // Calculate the assigned second for this Arduino
    unsigned long assignedSecond = unitId % slotInterval;
    unsigned long currentMillis = millis();
    unsigned long currentSecond = (currentMillis / 1000) % slotInterval; 

    if (currentSecond == assignedSecond && wifiManager.isConnected()) {
        const auto& objects = serialManager.getObjectList();
        if (!objects.empty()) {
            bool allSent = true;

            for (const auto &obj : objects) {
                // Send data directly as a DataObject 
                if (!wifiManager.sendDataToUrl("http://url/api", obj)) {
                    allSent = false;
                    break;
                }
            }

            if (allSent) {
                Serial.println("DBG: All data successfully sent to API.");
                serialManager.clearObjectList();
            } else {
                Serial.println("DBG: Failed to send all data to API.");
            }
        }
    }

    delay(100);
}