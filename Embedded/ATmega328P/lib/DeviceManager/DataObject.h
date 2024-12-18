#ifndef DATA_OBJECT_H
#define DATA_OBJECT_H

#include <Arduino.h>
#include <ArduinoJson.h>

struct DataObject {
    String deviceId;
    String timestamp;
    String status;

    void toJson(JsonDocument &doc) const {
        doc["deviceId"] = deviceId;
        doc["timestamp"] = timestamp;
        doc["status"] = status;
    }

    static DataObject fromJson(const JsonDocument &doc) {
        DataObject obj;
        obj.deviceId = doc["deviceId"] | "";
        obj.timestamp = doc["timestamp"] | "";
        obj.status = doc["status"] | 0;
        return obj;
    }
};

#endif