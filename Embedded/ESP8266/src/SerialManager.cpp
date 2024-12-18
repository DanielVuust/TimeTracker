#include "SerialManager.h"

SerialManager::SerialManager(WiFiManager &manager) : wifiManager(manager) {}

void SerialManager::handleSerial() {
    if (Serial.available()) {
        String input = Serial.readStringUntil('\n');
        input.trim();

        if (input.startsWith("CMD:")) {
            String command = input.substring(4);
            if (command == "DISCONNECT") {
                Serial.println("DBG: DISCONNECT command received.");
                wifiManager.handleDisconnect();
                Serial.println("DBG: WiFi disconnected and AP started.");
            } else {
                Serial.println("DBG: Unknown command: " + command);
            }
        } else if (input.startsWith("OBJ:")) {
            String jsonString = input.substring(4);
            JsonDocument doc;
            DeserializationError error = deserializeJson(doc, jsonString);

            if (error) {
                Serial.print("DBG: Failed to parse JSON for object data: ");
                Serial.println(error.c_str());
            } else {
                DataObject newObject = DataObject::fromJson(doc);
                // Validate extracted data if needed
                if (newObject.timestamp.length() > 0) {
                    objectList.push_back(newObject);
                    Serial.println("DBG: Object added - Timestamp: " + newObject.timestamp + ", Status: " + String(newObject.status));
                } else {
                    Serial.println("DBG: Invalid object data received.");
                }
            }
        }
    }
}

const std::vector<DataObject>& SerialManager::getObjectList() const {
    return objectList;
}

void SerialManager::clearObjectList() {
    objectList.clear();
    Serial.println("DBG: Object list cleared.");
}
