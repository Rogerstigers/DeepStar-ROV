#include <Wire.h>
#include <Adafruit_MotorShield.h>
#include "utility/Adafruit_MS_PWMServoDriver.h"

Adafruit_MotorShield AFMS = Adafruit_MotorShield();
const byte numChars = 32;
char receivedChars[numChars];

boolean newData = false;


void setup() {
	Serial.begin(9600);
	Serial.println("Deepstar Controller Online");

	AFMS.begin();
}

void loop() {
	recvWithStartEndMarkers();
	processNewData();
}

void processNewData() {
	if (newData) {
		newData = false;
		Serial.println(receivedChars);
		if (strcmp(receivedChars, "ALLSTOP") == 0) {
			StopMotor(1);
			StopMotor(2);
			StopMotor(3);
		}
		if (strcmp(receivedChars, "LEDON") == 0) {
			RunMotor(4, 255, 1);
		}
		if (strcmp(receivedChars, "LEDOFF") == 0) {
			StopMotor(4);
		}
		if (strcmp(receivedChars, "PORT") == 0) {
			RunMotor(1, 255, 1);
			RunMotor(2, 255, 0);
		}
		if (strcmp(receivedChars, "STARBOARD") == 0) {
			RunMotor(1, 255, 0);
			RunMotor(2, 255, 1);
		}
		if (strcmp(receivedChars, "ASCEND") == 0) {
			RunMotor(3, 255, 0);
		}
		if (strcmp(receivedChars, "DIVE") == 0) {
			RunMotor(3, 255, 1);
		}
		if (strcmp(receivedChars, "AHEAD") == 0) {
			RunMotor(1, 255, 1);
			RunMotor(2, 255, 1);
		}
		if (strcmp(receivedChars, "ASTEARN") == 0) {
			RunMotor(1, 255, 0);
			RunMotor(2, 255, 0);
		}
	}
}


void recvWithStartEndMarkers() {
	static boolean recvInProgress = false;
	static byte ndx = 0;
	char startMarker = '<';
	char endMarker = '>';
	char rc;

	while (Serial.available() > 0 && newData == false) {
		rc = Serial.read();

		if (recvInProgress == true) {
			if (rc != endMarker) {
				receivedChars[ndx] = rc;
				ndx++;
				if (ndx >= numChars) {
					ndx = numChars - 1;
				}
			}
			else {
				receivedChars[ndx] = '\0'; // terminate the string
				recvInProgress = false;
				ndx = 0;
				newData = true;
			}
		}

		else if (rc == startMarker) {
			recvInProgress = true;
		}
	}
}

void RunMotor(int motorNumber, int motorSpeed, int motorDirection) {
	Adafruit_DCMotor *motor = AFMS.getMotor(motorNumber);
	motor->setSpeed(motorSpeed);

	if (motorDirection == 1) {
		motor->run(FORWARD);
	}
	else
	{
		motor->run(BACKWARD);
	}
}

void StopMotor(int motorNumber) {
	Adafruit_DCMotor *motor = AFMS.getMotor(motorNumber);
	motor->run(RELEASE);
}

