#ifndef SERIAL_MANAGER_H
#define SERIAL_MANAGER_H

#include <Arduino.h>
#include <vector>
#include <ArduinoJson.h>
#include "WiFiManager.h"
#include "DataObject.h"

class SerialManager {
private:
    WiFiManager &wifiManager;
    std::vector<DataObject> objectList;

public:
    SerialManager(WiFiManager &manager);
    void handleSerial();
    const std::vector<DataObject>& getObjectList() const;
    void clearObjectList();
};

#endif
