# RailWaysideMonitor

## Overview && Current Features
This project is a simple wayside monitoring API. It manages and processes a simulation of railway equipment devices and events. Furthermore, the current device state can be maintained, or modified by said events. Finally, the API can generate alerts in response to state changes, and maintain the alert history.

- Simulated Wayside devices.
- In-memory event & alert history 
- Generating alerts when track circuit state switches to occupied.
- GET processing for devices, events, and alerts
- POST processing for events
- Device state updates
- Request validation with Results.
- Test cases

## What is used?
.NET 10
ASP.NET Core Minimal APIs
REST

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
6. Generate alert is applicable

## How to run?
- Command Prompt
- dotnet run
- .http file has test requests

## Current Limitations
- Data gets wiped on restart since inside memory.
- Device states are global using an enum.
- Alert logic is simple and covers one case.

## Planned Improvements
- PostgreSQL persistence for memory
- Docker containerization
- Automated unit && integration tests
- Separate device simulator component
- Device-specific state models
- Kubernetes deployment

## Disclaimer
This project is a personal software project used to learn and practice. It does cover and implement real railway signaling or safety logic that one would find in professional environments.