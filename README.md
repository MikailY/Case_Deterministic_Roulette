# 🎡 Deterministic Roulette - Game Developer Demo

A high-fidelity, 3D single-player Roulette prototype built in Unity 6 (6000.3.0f1). This project focuses on realistic wheel physics simulation, a robust betting system, and a deterministic outcome engine for testing and controlled gameplay.

---

### 🎮 Gameplay Demonstration
<p align="center"> 
  <a href="https://www.youtube.com/watch?v=MFq-C57d07k"> 
    <img src="https://img.youtube.com/vi/MFq-C57d07k/maxresdefault.jpg" alt="Watch Gameplay" width="600px">
  </a>

<em>Click above to watch the gameplay video</em> </p>

---

### 📋 Project Overview
This prototype simulates a complete Roulette experience with a specialized "Deterministic" mode, allowing the next winning number to be pre-defined via the UI.

* Deterministic Engine: Manually select the next result or let the RNG decide.
* Full Roulette Rules: Supports Inside (Straight, Split, etc.) and Outside (Red/Black, Columns, etc.) bets.
* 3D Experience: Immersive wheel animations and ball drop physics.
* Statistics: Tracks spins, win/loss ratios, and total profit.

---

### 🛠️ Technical Implementation
I have strictly followed the technical requirements, focusing on clean code and zero third-party dependencies.

#### Architecture & SOLID

* Observer Pattern & Event Bus
  * Implemented a central Event Bus to manage communication between decoupled systems.
  * When the wheel stops, a Event_OnSpinEnded event is broadcasted. Independent modules like UI, Session, Board and Statistics subscribe to this event, ensuring zero direct dependency (Low Coupling).
* Data-Driven Design (Scriptable Objects): Roulette bet types and payout ratios are decoupled from the logic using Scriptable Objects. This allows for easy balancing and configuration of different roulette rules (European vs. American) without modifying the core codebase.
* Pooling of Chip Objects and Chip Texts to improve performance.
  
#### No Third-Party Plugins (No DOTween)

* All animations (wheel spin, ball movement, UI transitions) are handled via Custom Coroutines and Lerp to ensure high performance and precise control.
* Deterministic Spin Logic: Precisely tuned to simulate the wheel's friction and the ball's final landing and smooth transitions between state changes.

#### UI & Graphics

* TextMeshPro: Used for all dynamic text and historical records.
* World space UI for text on top of placed chips.

---

### 🎮 Gameplay & Controls
* Select chips from the menu on the lower side. There are 5 different chip types. The default one is red, which is valued at 100.
* Interact with the board in the center to place your bets. 
* To select the outcome, use the scrollable menu on the right side. Scroll to the desired number and click.
* After placing your bets, you can click SPIN to continue.
  
---

### Any Known Issues or Future Improvements:
* Logic is working fine but needs general improvement on feel
* Needs a complete UI makeover
* Lacking animations and effects to improve immersion
* 3D environment around table
* Add support for American Roulette (Double-Zero)
* Save & load session data

---

### ⚙️ How to Run
1. Clone this repository.
2. Open the project in Unity 6000.3.0f1.
3. Load the SampleScene located in Assets/Scenes/.
4. Press Play to start the simulation.

---

### 📸 Screenshots

<table style="width: 100%; border-collapse: collapse; border: none;">
  <tr>
    <td align="center" style="border: none;">
      <img width="100%" alt="wheel" src="https://github.com/user-attachments/assets/0c1f7b0d-0f4c-40bd-a42e-5fb4220a5550" /><br />
      <sub><b>Roulette Wheel</b></sub>
    </td>
    <td align="center" style="border: none;">
      <img width="100%" alt="placements" src="https://github.com/user-attachments/assets/f3ee7550-c1ad-4fcf-9b50-02a9015f4190" /><br/>
      <sub><b>Bet Placements</b></sub>
    </td>
    <td align="center" style="border: none;">
      <img width="100%"  alt="chipstacks" src="https://github.com/user-attachments/assets/a342aee7-5124-45ed-9499-a56a7d11467c" /><br/>
      <sub><b>Chip Stacking</b></sub>
    </td>
  </tr>
  <tr>
    <td align="center" style="border: none;">
      <img width="100%" alt="eventbus" src="https://github.com/user-attachments/assets/e9b0445e-2884-4de9-960b-2d41beb630ef" /><br />
      <sub><b>Event Bus</b></sub>
    </td>
    <td align="center" style="border: none;">
      <img width="100%" alt="pool_chips" src="https://github.com/user-attachments/assets/0e7c5f54-ddde-42eb-bdb2-7b7c0528329c" /><br/>
      <sub><b>Pooling Chip Objects</b></sub>
    </td>
    <td align="center" style="border: none;">
      <img width="100%" alt="pool_chiptexts" src="https://github.com/user-attachments/assets/ea88caee-3892-4b5d-b4e7-ec7202020d65" /><br/>
      <sub><b>Pooling Chip Texts</b></sub>
    </td>
  </tr>
</table>

---

### Credits
* "Roulette Table 2 Downloadable" (https://skfb.ly/oRuAK) by Dudzy is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
* "simple low poly casino chips and dices" (https://skfb.ly/oGXrX) by lupas is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
* <a href="https://www.flaticon.com/free-icons/bet" title="bet icons">Bet icons created by Andrejs Kirma - Flaticon</a>
