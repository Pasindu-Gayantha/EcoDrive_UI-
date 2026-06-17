# EcoDrive - Vehicle Emission Testing & Certification System (Group Project)

This repository contains the core desktop application developed as a collaborative group project for our university module. EcoDrive is designed to automate and streamline operations at vehicle emission testing centers, covering everything from secure authentication and customer onboarding to dynamic queue management, live billing, and administrative oversight.

## Key System Features

- **Main Dashboard:** Live statistics visualizer for counter staff to track daily booking flows and overall system volume at a glance.
- **Customer & Vehicle Onboarding:** A robust data-entry module to capture customer credentials (NIC, contact information) alongside structural vehicle telemetry (Engine CC, Fuel Type).
- **Dynamic Slot Allocation:** Intelligent lane management engine that routes incoming vehicles based on weight class (Light/Heavy) while enforcing duplicate booking prevention.
- **Automated Billing Utilities:** Dynamic fee calculation framework based on engine specifications, paired with an automated text receipt compiler and local log generator.
- **Administrative Panel:** Decoupled administrative command center equipped with raw database synchronization, CSV data exporting, and system backup management.

## Tech Stack & Environment

- **Development Environment:** Microsoft Visual Studio
- **Language & Framework:** C# (.NET Framework)
- **User Interface:** Windows Forms (WinForms)
- **Database Engine:** Microsoft SQL Server (LocalDB)

## Team Contributions

This project was established through a structured team layout where foundational system architectures were designed collectively. The engineering breakdown is as follows:

- **Member 1:** Designed and developed the comprehensive, clean UI design skeleton (Fully UI Template) and initialized the local Microsoft SQL Server database structure. 
- **Member 2:** Engineered the core operational logic and backend systems for the primary User Dashboard, facilitating state changes and live tracking widgets.
- **Member 3:** Developed the logic and backend architecture for the Customer Registration, Dynamic Time-Slot Booking, and Automated Payment compilation systems.
- **Member 4:** Programmed the backend authentication mechanism, validation rules, and permission-handling structures for the primary Login Page.
- **Member 5:** Developed the logic, backend interfaces, chart-binding data layers, and analytical utilities for the Admin Dashboard.

## System Working Flow

The workflow of the EcoDrive system executes through the following structured stages:

1. **System Initialization & Main Interface:** The application launches directly into the primary counter interface (**Form1**), allowing counter staff immediate, unhindered access to daily operational modules.
2. **Dashboard Overview:** Upon initialization, the **Main Dashboard** widget populates, fetching live data pipelines to show concurrent lane traffic and active scheduling metrics.
3. **Registry Pipeline:** Incoming vehicles are processed through the **Registration View**, matching owner identities with structural vehicle properties which are immediately committed to the local database.
4. **Queue Allocation:** The system executes backend database query checks to locate vacant time slots based on vehicle classification, creating a conflict-free appointment index (**Dynamic Slot Booking**).
5. **Billing & Transact Phase:** The core testing engine dynamically maps out the processing fee. Once a successful payment status is recorded, the system compiles a downloadable, print-ready digital text receipt.
6. **Administrative Authentication & Operations:** To secure high-level management functions, accessing the **Admin Panel** requires administrative credential verification. Once authenticated, it grants full access to system telemetry synchronization, raw CSV data exporting, and emergency local database backup execution.
