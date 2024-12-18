// File: DeviceManager.cpp
#include "DeviceManager.h"

DeviceManager::DeviceManager(WiFiManager &manager) : wifiManager(manager) {}

void DeviceManager::handleSerial() {
    if (Serial.available()) {
        String input = Serial.readStringUntil('\n');
        input.trim();

        if (input.startsWith("ECMD:")) {
            String command = input.substring(4);
            if (command == "DISCONNECT") {
                Serial.println("EDBG: DISCONNECT command received.");
                wifiManager.handleDisconnect();
                Serial.println("EDBG: WiFi disconnected and AP started.");
            } else {
                Serial.println("EDBG: Unknown command: " + command);
            }
        } else if (input.startsWith("EOBJ:")) {
            String jsonString = input.substring(4);
            JsonDocument doc;
            DeserializationError error = deserializeJson(doc, jsonString);

            if (error) {
                Serial.print("EDBG: Failed to parse JSON for object data: ");
                Serial.println(error.c_str());
            } else {
                DataObject newObject = DataObject::fromJson(doc);
                // Validate extracted data if needed
                if (newObject.timestamp.length() > 0) {
                    objectList.push_back(newObject);
                    Serial.println("EDBG: Object added - Timestamp: " + newObject.timestamp + ", Status: " + String(newObject.status));
                } else {
                    Serial.println("EDBG: Invalid object data received.");
                }
            }
        }
    }
}

const std::vector<DataObject>& DeviceManager::getObjectList() const {
    return objectList;
}

void DeviceManager::clearObjectList() {
    objectList.clear();
    Serial.println("EDBG: Object list cleared.");
}
