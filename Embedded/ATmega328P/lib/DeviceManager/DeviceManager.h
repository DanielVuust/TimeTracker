// File: DeviceManager.h
#ifndef DEVICE_MANAGER_H
#define DEVICE_MANAGER_H

#include <Arduino.h>
#include <EEPROM.h>
#include <DataObject.h>

#define GUID_LENGTH 36
#define MAX_BUTTONS 12

class DeviceManager {
public:
    DeviceManager();

    void begin(uint8_t rowPins[4], uint8_t colPins[4]);
    String getDeviceId();
    void setButtonTask(uint8_t row, uint8_t col, String status);
    void handleSerial();
    void handleButtons();
    void sendDataObject(const DataObject &obj);

private:
    String generateGuid(); // For device ID
    void loadGuidFromEeprom();
    void saveGuidToEeprom();

    String deviceId;
    struct ButtonTask {
        uint8_t row;
        uint8_t col;
        String status;
    };

    ButtonTask buttonTasks[MAX_BUTTONS]; // Support up to 12 buttons
    uint8_t buttonTaskCount;

    uint8_t rowPins[4]; // GPIO pins for rows
    uint8_t colPins[4]; // GPIO pins for columns

    void updateButtonTask(uint8_t row, uint8_t col, const String &status);
    void initializeButtonMatrix();
    bool isButtonPressed(uint8_t row, uint8_t col);
    String getCurrentTimestamp();
    RTC_DS3231 rtc;
};

#endif