// File: DeviceManager.cpp
#include "DeviceManager.h"
#include <ArduinoJson.h>
#include <RTClib.h>
#include <SPI.h>

DeviceManager::DeviceManager() : buttonTaskCount(0), rtc() {}

void DeviceManager::begin(uint8_t rowPins[4], uint8_t colPins[4]) {
    Serial.begin(9600);
    memcpy(this->rowPins, rowPins, sizeof(this->rowPins));
    memcpy(this->colPins, colPins, sizeof(this->colPins));

    for (uint8_t i = 0; i < 4; i++) {
        pinMode(this->rowPins[i], OUTPUT);
        digitalWrite(this->rowPins[i], HIGH);
    }

    for (uint8_t i = 0; i < 4; i++) {
        pinMode(this->colPins[i], INPUT_PULLUP);
    }

    loadGuidFromEeprom();
    if (deviceId.length() == 0) {
        deviceId = generateGuid();
        saveGuidToEeprom();
    }
}

String DeviceManager::getDeviceId() {
    return deviceId;
}

void DeviceManager::setButtonTask(uint8_t row, uint8_t col, String status) {
    if (buttonTaskCount < MAX_BUTTONS) {
        buttonTasks[buttonTaskCount++] = {row, col, status};
    }
}

void DeviceManager::handleButtons() {
    for (uint8_t row = 0; row < 4; row++) {
        digitalWrite(rowPins[row], LOW);
        for (uint8_t col = 0; col < 4; col++) {
            if (digitalRead(colPins[col]) == LOW) {
                for (uint8_t i = 0; i < buttonTaskCount; i++) {
                    if (buttonTasks[i].row == row && buttonTasks[i].col == col) {
                        DataObject obj;
                        obj.deviceId = deviceId;
                        obj.timestamp = getCurrentTimestamp();
                        obj.status = buttonTasks[i].status;

                        if (obj.status == "Disconnect") {
                            // If status is Disconnect, send ECMD: DISCONNECT
                            Serial.println("ECMD: DISCONNECT");
                        } else {
                            // Otherwise, send a normal data object
                            sendDataObject(obj);
                        }

                        delay(200); // Debounce delay
                        break;
                    }
                }
            }
        }
        digitalWrite(rowPins[row], HIGH);
    }
}

void DeviceManager::sendDataObject(const DataObject &obj) {
    JsonDocument doc;
    obj.toJson(doc);
    String jsonString;
    serializeJson(doc, jsonString);
    Serial.print("EOBJ: ");
    Serial.println(jsonString);
}

String DeviceManager::generateGuid() {
    String guid = "";
    for (int i = 0; i < GUID_LENGTH; i++) {
        int randVal = random(0, 16);
        if (randVal < 10) {
            guid += (char)('0' + randVal);
        } else {
            guid += (char)('a' + randVal - 10);
        }
    }
    return guid;
}

void DeviceManager::loadGuidFromEeprom() {
    char buffer[GUID_LENGTH + 1];
    for (int i = 0; i < GUID_LENGTH; i++) {
        buffer[i] = EEPROM.read(i);
    }
    buffer[GUID_LENGTH] = '\0';
    deviceId = String(buffer);
}

void DeviceManager::saveGuidToEeprom() {
    for (int i = 0; i < GUID_LENGTH; i++) {
        EEPROM.update(i, deviceId[i]);
    }
}

void DeviceManager::updateButtonTask(uint8_t row, uint8_t col, const String &status) {
    for (uint8_t i = 0; i < buttonTaskCount; i++) {
        if (buttonTasks[i].row == row && buttonTasks[i].col == col) {
            buttonTasks[i].status = status;
            Serial.print("ADBG: Updated button at (");
            Serial.print(row);
            Serial.print(", ");
            Serial.print(col);
            Serial.print(") to status: ");
            Serial.println(status);
            return;
        }
    }
    // Add new button if not found
    if (buttonTaskCount < MAX_BUTTONS) {
        buttonTasks[buttonTaskCount++] = {row, col, status};
        Serial.print("ADBG: Added new button at (");
        Serial.print(row);
        Serial.print(", ");
        Serial.print(col);
        Serial.print(") with status: ");
        Serial.println(status);
    } else {
        Serial.println("Maximum number of buttons reached.");
    }
}

void DeviceManager::handleSerial() {
    if (Serial.available()) {
        String input = Serial.readStringUntil('\n');
        input.trim();

        if (input.startsWith("ACMD:")) {
            String command = input.substring(4);
            // Handle command
        } else if (input.startsWith("AOBJ:")) {
            String jsonString = input.substring(4);

            JsonDocument doc;

            uint8_t row = doc["row"] | 255;
            uint8_t col = doc["col"] | 255;
            String status = doc["status"] | "";

            if (row < 4 && col < 4 && status.length() > 0) {
                updateButtonTask(row, col, status);
            } else {
                Serial.println("Invalid button configuration.");
            }
        }
    }
}

String DeviceManager::getCurrentTimestamp() {
    DateTime now = rtc.now();
    char buffer[20];
    snprintf(buffer, sizeof(buffer), "%04d-%02d-%02dT%02d:%02d:%02d",
             now.year(), now.month(), now.day(),
             now.hour(), now.minute(), now.second());
    return String(buffer);
}
