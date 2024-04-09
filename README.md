# Inventory Management System API

## Introduction

This Inventory Management System is a .NET Web API project designed to manage and track the lifecycle of shoe products from factories to shops. It facilitates various stock movements, transaction recordings, and reporting functionalities. The system is web-based, cloud-hosted, and accessible via both computer and mobile devices.

## Main Functional Features

### Product Management
- **Add Shoe Models and Shoes:** Users can create entries for different shoe models and individual shoes in the system.

### Stock Transfers
- **Factory to Store Transfer:** Records the transfer of items from the factory to the store, initiated by a Factory Officer and approved by a Store Officer.
- **Store to Shop Transfer:** Manages the transfer of items from the store to the shop, started by a Store Officer and approved by a Shop Officer.
- **Purchase to Store Transfer:** Tracks items purchased and transferred to the store, initiated by a Purchase Officer and approved by a Store Officer.
- **Shop to Store Transfer:** Handles the return of items from the shop to the store, initiated by a Shop Officer and approved by a Store Officer.

### Reporting and Tracking
- **Sales Report:** Allows the Shop Officer to report on the sales of items.
- **Search Transactions:** Users can search for the aforementioned transactions using various criteria and view detailed information about each transaction.
- **Search Stock:** Enables searching of items in stock, either in the shop or store, based on criteria like machine code, shoe model, etc.
- **Reports:** Provides reporting features that analyze and summarize information about transactions and stocks.

## Non-Functional Features

- **Web-Based Application:** Accessible through any web browser.
- **Cloud-Hosted:** Hosted in the cloud, ensuring availability from anywhere, at any time.
- **Device Compatibility:** Can be accessed from both computers and mobile devices.
- **Performance:** Fast and reliable system performance.
- **Multilingual Support:** Capable of supporting multiple languages upon request.

## Getting Started

To get started with this project, clone the repository to your local machine. Ensure you have the .NET SDK installed and configured properly.

```bash
git clone [repository URL]
cd [project-name]
dotnet restore
dotnet build
dotnet run
