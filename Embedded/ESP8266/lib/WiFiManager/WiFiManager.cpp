// File: WiFiManager.cpp
#include "WiFiManager.h"
#include <ArduinoJson.h>

WiFiManager::WiFiManager() : connected(false), server(80) {}

void WiFiManager::begin() {
    EEPROM.begin(EEPROM_SIZE);
    loadCredentials();

    if (!tryConnect()) {
        startAccessPoint();
        setupWebServer();
    }
}

void WiFiManager::handle() {
    server.handleClient();
}

bool WiFiManager::isConnected() {
    return connected;
}

void WiFiManager::handleDisconnect() {
    Serial.println("EDBG: Handling Disconnect");
    WiFi.disconnect();
    startAccessPoint();
    Serial.println("EDBG: Disconnected from Wi-Fi and started Access Point");
}

void WiFiManager::saveCredentials(const String &ssid, const String &password) {
    for (size_t i = 0; i < ssid.length(); i++) {
        EEPROM.write(i, ssid[i]);
    }
    EEPROM.write(ssid.length(), '\0');
    for (size_t i = 0; i < password.length(); i++) {
        EEPROM.write(32 + i, password[i]);
    }
    EEPROM.write(32 + password.length(), '\0');
    EEPROM.commit();
}

void WiFiManager::loadCredentials() {
    char ssidArr[32];
    char passwordArr[32];

    for (int i = 0; i < 32; i++) {
        ssidArr[i] = EEPROM.read(i);
    }
    for (int i = 0; i < 32; i++) {
        passwordArr[i] = EEPROM.read(32 + i);
    }

    ssid = String(ssidArr);
    password = String(passwordArr);
}

bool WiFiManager::tryConnect() {
    if (ssid.isEmpty() || password.isEmpty()) {
        return false;
    }

    WiFi.begin(ssid.c_str(), password.c_str());
    if (WiFi.waitForConnectResult() == WL_CONNECTED) {
        Serial.println("EDBG: Connected to Wi-Fi!");
        Serial.print("EDBG: IP Address: ");
        Serial.println(WiFi.localIP());
        connected = true;
        return true;
    }

    connected = false;
    return false;
}

void WiFiManager::startAccessPoint() {
    WiFi.softAP(AP_SSID, AP_PASSWORD);
    IPAddress apIP(192, 168, 4, 1);
    IPAddress netMsk(255, 255, 255, 0);
    WiFi.softAPConfig(apIP, apIP, netMsk);
    Serial.println("EDBG: Started AP Mode. Connect to '" + String(AP_SSID) + "' with password '" + String(AP_PASSWORD) + "'");
    Serial.print("EDBG: IP Address: ");
    Serial.println(WiFi.softAPIP());
    connected = false;
}

void WiFiManager::setupWebServer() {
    server.on("/", HTTP_GET, [this]() { handleRoot(); });
    server.on("/setup", HTTP_POST, [this]() { handleSetup(); });
    server.begin();
}

String WiFiManager::generateSSIDDropdown() {
    int n = WiFi.scanNetworks();
    String dropdown;
    dropdown += "<select name='ssid'>\n";
    for (int i = 0; i < n; i++) {
        dropdown += "  <option value='" + WiFi.SSID(i) + "'>" + WiFi.SSID(i) + "</option>\n";
    }
    dropdown += "</select>\n";
    return dropdown;
}

void WiFiManager::handleRoot() {
    String html;
    html += "<!DOCTYPE html>\n";
    html += "<html>\n";
    html += "<head>\n";
    html += "  <style>\n";
    html += "    body { font-family: Arial, sans-serif; background-color: #f4f4f9; color: #333; margin: 0; padding: 0; }\n";
    html += "    h1 { text-align: center; color: #555; }\n";
    html += "    form { max-width: 400px; margin: 20px auto; padding: 20px; background: #fff; border-radius: 5px; box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1); }\n";
    html += "    input, select, button { width: 100%; padding: 10px; margin: 10px 0; border: 1px solid #ddd; border-radius: 3px; }\n";
    html += "    button { background-color: #007BFF; color: white; border: none; cursor: pointer; }\n";
    html += "    button:hover { background-color: #0056b3; }\n";
    html += "  </style>\n";
    html += "</head>\n";
    html += "<body>\n";
    html += "  <h1>Wi-Fi Setup</h1>\n";
    html += "  <form action='/setup' method='POST'>\n";
    html += "    <button type='button' onclick='window.location.reload()'>Update SSID List</button><br><br>\n";
    html += "    SSID: " + generateSSIDDropdown() + "<br>\n";
    html += "    Password: <input type='password' name='password'><br>\n";
    html += "    <input type='submit' value='Save'>\n";
    html += "  </form>\n";
    html += "</body>\n";
    html += "</html>\n";
    server.send(200, "text/html", html);
}

void WiFiManager::handleSetup() {
    ssid = server.arg("ssid");
    password = server.arg("password");
    saveCredentials(ssid, password);
    server.send(200, "text/html", "<h1>Saved! Restarting...</h1>");
    delay(1000);
    ESP.restart();
}