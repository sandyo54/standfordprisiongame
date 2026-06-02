# Stanford Prison Experiment Simulation

A psychological choice-based simulation built with **C#**, **WPF**, and **.NET 10.0**. This application explores the power of social roles and authority, inspired by the 1971 Stanford Prison study.

---

## 🎮 Game Overview

In this simulation, you are randomly assigned a role that dictates your objectives, choices, and psychological survival strategy:

* **As a Prisoner:** You must navigate oppressive conditions, decide when to comply or rebel, and manage your mental stability to avoid a total breakdown.
* **As a Guard:** You are tasked with maintaining absolute order. You must balance the enforcement of harsh rules against your own moral conscience and stress levels.

---

## ✨ Key Features

* **Role Randomization:** Every session starts with a "coin-flip" assignment (Prisoner vs. Guard) with unique UI text and objectives.
* **Dynamic Scenarios:** 5 core "Day" events—ranging from 2:00 AM wake-up calls to hunger strikes—that provide different choices based on your role.
* **Stat Management:**
    * **Mental Stability:** Drops when you endure or inflict trauma.
    * **Compliance/Alignment:** Measures how well you fit into the systemic role.
* **Multiple Endings:** 6 distinct outcomes (e.g., Total Rebellion, Systemic Assimilation, or Moral Collapse) triggered by your choices.
* **Escape System:** A mathematical "breakout" mechanic for prisoners influenced by current stress and stability stats.

---

## 📂 Project Structure

* **`MainWindow.xaml/.cs`**: The main interface, terminal logging, and game loop logic.
* **`participant.cs`**: Contains the `Player` class, psychological stat clamping, and escape logic.
* **`Scenario.cs`**: The data structure for narrative events and role-specific choice impacts.
* **`App.xaml`**: Standard WPF application entry point.

---

## 🛠️ Installation & Setup

### Prerequisites
* **Windows OS** (Required for WPF)
* **Visual Studio 2022** (or compatible IDE)
* **.NET 10.0 SDK**

### Running the App
1.  **Clone or download** this repository.
2.  **Open the terminal** in the project folder.
3.  **Run the command:**
    ```bash
    dotnet run --project standfordprisiongame.csproj
    ```
    *Alternatively, open `standfordprisiongame.slnx` in Visual Studio and press F5.*

---

## 📜 License
This project is for educational and simulation purposes.
