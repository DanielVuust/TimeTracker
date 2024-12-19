// File: DeviceManager.cpp
#include "DeviceManager.h"

DeviceManager::DeviceManager(WiFiManager &manager) : wifiManager(manager) {}

void DeviceManager::handleSerial() {
    if (Serial.available()) {
        String input = Serial.readStringUntil('\n');
        input.trim();

        if (input.startsWith("ECMD:")) {
            String command = input.substring(5);
            if (command == "DISCONNECT") {
                wifiManager.debugPrintln("EDBG: DISCONNECT command received.");
                wifiManager.handleDisconnect();
                wifiManager.debugPrintln("EDBG: WiFi disconnected and AP started.");
            } else {
                Serial.println("EDBG: Unknown command: " + command);
            }
        } else if (input.startsWith("EOBJ:")) {
            String jsonString = input.substring(5);
            JsonDocument doc;
            DeserializationError error = deserializeJson(doc, jsonString);

            if (error) {
                wifiManager.debugPrint("EDBG: Failed to parse JSON for object data: ");
                wifiManager.debugPrintln(error.c_str());
            } else {
                DataObject newObject = DataObject::fromJson(doc);
                // Validate extracted data if needed
                if (newObject.timestamp.length() > 0) {
                    objectList.push_back(newObject);
                    wifiManager.debugPrintln("EDBG: Object added - Timestamp: " + newObject.timestamp + ", Status: " + String(newObject.status));
                } else {
                    wifiManager.debugPrintln("EDBG: Invalid object data received.");
                }
            }
        } else if (input.startsWith("ADBG:")) {
            wifiManager.debugPrintln(input.substring(5));
        }
    }
}

const std::vector<DataObject>& DeviceManager::getObjectList() const {
    return objectList;
}

void DeviceManager::clearObjectList() {
    objectList.clear();
    wifiManager.debugPrintln("EDBG: Object list cleared.");
}
