# RailWaysideMonitor

## Overview && Current Features
This project is a simple wayside monitoring API. It manages and processes a simulation of railway equipment devices and events. Furthermore, the current device state can be maintained, or modified by said events. Finally, the API can generate alerts in response to state changes, and maintain the alert history. There are currently 5 automated tests to pass.

- Simulated Wayside devices.
- In-memory event & alert history 
- Generating alerts when track circuit state switches to occupied.
- GET processing for devices, events, and alerts
- POST processing for events
- Device state updates
- Request validation with appropriate HTTP responses.
- Manual API test requests.
- xUnit automated tests

## Technologies?
.NET 10
ASP.NET Core Minimal APIs
REST
Docker

## API Endpoints
GET /api/devices
GET /api/events
POST /api/events
GET /api/alerts

## Example Event Flow
1. POST /api/events
2. Device validation
3. State validation
4. Update device state
5. Store Event
6. Generate alert if applicable

## How to run?
- Command Prompt
- dotnet run
- .http file has test requests
- dotnet test into automated test folder

## Current Limitations
- Data is stored in memory and reset when application restarts.
- Device states currently use a shared enum for all types.
- Alert logic is simple and covers one case.

## Planned Improvements
- PostgreSQL persistence
- Automated unit && integration tests
- Separate device simulator component
- Device-specific state models
- Kubernetes deployment

## Disclaimer
This project is a personal software project used to learn and practice. It does not cover and implement real railway signaling or safety logic that one would find in professional environments.