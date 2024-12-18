// File: WiFiManager.h
#ifndef WIFI_MANAGER_H
#define WIFI_MANAGER_H

#include <ESP8266WiFi.h>
#include <ESP8266WebServer.h>
#include <EEPROM.h>

#define EEPROM_SIZE 96
#define AP_SSID "Time_Tracker"
#define AP_PASSWORD "12345678"

class WiFiManager {
private:
    String ssid;
    String password;
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
};

#endif