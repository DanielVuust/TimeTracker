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
    bool sendDataToUrl(const String &url, const T &data);
};

// Template definition must be in the header
template <typename T>
bool WiFiManager::sendDataToUrl(const String &url, const T &data) {
    if (!isConnected()) {
        Serial.println("DBG: Not connected to Wi-Fi. Cannot send data to API.");
        return false;
    }

    // Convert data to JSON
    JsonDocument doc;
    data.toJson(doc);
    String jsonString;
    serializeJson(doc, jsonString);

    WiFiClient client;
    if (client.connect(url.c_str(), 80)) {
        client.println("POST / HTTP/1.1");
        client.println("Host: " + url);
        client.println("Content-Type: application/json");
        client.print("Content-Length: ");
        client.println(jsonString.length());
        client.println();
        client.println(jsonString);

        Serial.println("DBG: Data sent: " + jsonString);

        unsigned long timeout = millis();
        while (client.available() == 0) {
            if (millis() - timeout > 5000) { 
                Serial.println("DBG: Server response timeout.");
                client.stop();
                return false;
            }
        }

        // Read the response
        String responseLine = client.readStringUntil('\n');
        Serial.println("Response: " + responseLine);
        if (responseLine.startsWith("HTTP/1.1 200")) {
            Serial.println("DBG: Data successfully received by the server.");
            client.stop();
            return true;
        } else {
            Serial.println("DBG: Server responded with error.");
            client.stop();
            return false;
        }
    } else {
        Serial.println("DBG: Failed to connect to API.");
        return false;
    }
}

#endif
