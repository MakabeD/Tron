# TRON – Interactive Cybersecurity Game

#### Video Demo: <YOUTUBE_VIDEO_URL_HERE>

#### Description:

TRON is a semi open-world video game designed to introduce players to core cybersecurity concepts through interactive gameplay. The project was developed as a final project for CS50x and aims to teach real-world security ideas in an engaging, visual, and hands-on way.

The player assumes the role of “Tron,” a digital defender responsible for maintaining the integrity of a computer system represented as a cybersecurity office. Throughout the game, the player must respond to anomalies, suspicious activities, and system threats by interacting with terminals that trigger educational minigames. Each minigame represents a real cybersecurity scenario, encouraging logical reasoning, fast decision-making, and understanding of system vulnerabilities.

This project combines programming, game design, and educational intent. Rather than presenting cybersecurity concepts as abstract theory, TRON embeds them into gameplay mechanics so the player learns by doing. The project was built using Unity and C#, with a database-backed structure intended to simulate realistic system events and logs.

---

## Problem Addressed

Cybersecurity is widely regarded as one of the most complex areas of software development. Many beginners struggle to understand how theoretical threats translate into real-world system behavior.

TRON addresses this problem by transforming cybersecurity scenarios into interactive challenges. By navigating a virtual office and responding to simulated incidents, players can internalize concepts such as system monitoring, anomaly detection, and incident response in a more intuitive and memorable way.

---

## Team Members and Roles

- **Daniel Yepes Molina** – Lead developer, gameplay programmer, systems design, and 3D modeling.
- **Miguel Ángel Zapata Vargas** – Secondary developer, documentation, visual assets research, and texture selection.

---

## Technologies Used

- **Unity Engine 2022.3.62f1**
- **C# (.NET)**
- **Microsoft SQL Server**
- **SQL Server Management Studio (SSMS)**
- **Git & GitHub**

---

## Project Structure

/Assets
/Scenes
/Scripts
/Models
/Textures
/db
database.sql
README.md


- **Assets/** contains all Unity-related resources such as scripts, scenes, models, and textures.
- **Assets/Scripts/** includes the core gameplay logic, event systems, and minigame controllers.
- **Assets/Scenes/** contains the main playable scene used for the project submission.
- **db/** stores the SQL scripts used to define and manage the database.
- **README.md** documents the project and its design decisions.

---

## How to Run the Project

### Prerequisites

Before running the project, ensure you have the following installed:

- Unity Hub (latest version)
- Unity Editor **2022.3.62f1**
- Visual Studio 2022 or JetBrains Rider with C# support
- Microsoft SQL Server (2019 or higher)
- SQL Server Management Studio (SSMS)

---

### Installation Steps

1. Clone the repository:

```bash
git clone https://github.com/MakabeD/Tron/tree/0e6e0d0a8b3a02e559272a63f603a6e87983bebe
```
2. Open Unity Hub.

3. Click Add Project and select the cloned project folder.

4. Ensure Unity uses version 2022.3.62f1.

5. Open the project.


### Running the Game

1. Wait for Unity to finish importing assets.

2. Navigate to:

3. Assets/Scenes/

4. Open the scene named: Main (entrega para la profe)

5. Press the Play ▶ button in Unity.
