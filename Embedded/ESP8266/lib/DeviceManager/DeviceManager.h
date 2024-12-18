// File: DeviceManager.h
#ifndef DEVICE_MANAGER_H
#define DEVICE_MANAGER_H

#include <Arduino.h>
#include <vector>
#include <ArduinoJson.h>
#include "WiFiManager.h"
#include "DataObject.h"

class DeviceManager {
private:
    WiFiManager &wifiManager;
    std::vector<DataObject> objectList;

public:
    DeviceManager(WiFiManager &manager);
    void handleSerial();
    const std::vector<DataObject>& getObjectList() const;
    void clearObjectList();
};

#endif
