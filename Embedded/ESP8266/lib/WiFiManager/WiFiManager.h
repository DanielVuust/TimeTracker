// File: WiFiManager.h
#ifndef WIFI_MANAGER_H
#define WIFI_MANAGER_H

#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include <EEPROM.h>
#include <ArduinoJson.h>

#define EEPROM_SIZE 96
#define AP_SSID "Time_Tracker"
#define AP_PASSWORD "12345678"

class WiFiManager {
private:
    String ssid;
    String password;
    bool connected;
    ESP8266WebServer server;

    void saveCredentials(const String &ssid, const String &password);
    void loadCredentials();
    bool tryConnect();
    void startAccessPoint();
    void setupWebServer();
    void handleRoot();
    void handleSetup();

    String generateSSIDDropdown();

public:
    WiFiManager();
    void begin();
    void handle();
    bool isConnected();
    void handleDisconnect();

    template <typename T>
    bool sendDataToUrl(const String &url, const T &data, const String &excludeField = "");
};

template <typename T>
bool WiFiManager::sendDataToUrl(const String &url, const T &data, const String &excludeField) {
    if (!isConnected()) {
        Serial.println("EDBG: Not connected to Wi-Fi. Cannot send data.");
        return false;
    }

    // Convert data to JSON
    JsonDocument doc;
    data.toJson(doc);

    // Optionally exclude a field
    if (!excludeField.isEmpty()) {
        doc.remove(excludeField);
    }

    String jsonString;
    serializeJson(doc, jsonString);

    WiFiClient client;
    String host = url.substring(0, url.indexOf("/", 8)); // Extract host
    String path = url.substring(url.indexOf("/", 8));    // Extract path

    if (client.connect(host.c_str(), 80)) {
        client.println("POST " + path + " HTTP/1.1");
        client.println("Host: " + host);
        client.println("Content-Type: application/json");
        client.print("Content-Length: ");
        client.println(jsonString.length());
        client.println();
        client.println(jsonString);

        Serial.println("EDBG: Data sent: " + jsonString);

        unsigned long timeout = millis();
        while (client.available() == 0) {
            if (millis() - timeout > 5000) {
                Serial.println("EDBG: Server response timeout.");
                client.stop();
                return false;
            }
        }

        // Handle response
        String responseLine = client.readStringUntil('\n');
        Serial.println("Response: " + responseLine);
        if (responseLine.startsWith("HTTP/1.1 200")) {
            Serial.println("EDBG: Data successfully received by the server.");
            client.stop();
            return true;
        } else {
            Serial.println("EDBG: Server responded with error.");
            client.stop();
            return false;
        }
    } else {
        Serial.println("EDBG: Failed to connect to server.");
        return false;
    }
}

#endif
