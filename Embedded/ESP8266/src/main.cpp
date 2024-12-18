#include <Arduino.h>
#include "WiFiManager.h"

WiFiManager wifiManager;

void setup() {
    Serial.begin(115200);
    wifiManager.begin();
}

void loop() {
    wifiManager.handle();
}