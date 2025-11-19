# SmoothOrb Visualization 🥤

![Unity](https://img.shields.io/badge/Unity-2023.x-black?logo=unity)
![URP](https://img.shields.io/badge/URP-17.0.3-blue)
![C#](https://img.shields.io/badge/C%23-9.0-purple)

An interactive 3D Unity application for exploring the internal components of a "Smooth Orb" (smoothie device), featuring fruit interactions and nutritional data visualization through interactive diagrams.


https://github.com/user-attachments/assets/7a9a16a6-caa6-41b2-b59b-cb1fc9160701



## ✨ Features

- **Interactive 3D Visualization** with rotation and zoom capabilities
- **Cross-section View** to explore internal layers (cryogenic core, metal casing, blades, window, mix room, foam system)
- **Fruit Spawning System** with physics simulation (strawberry, banana, guava, apple, orange)
- **Interactive Dialogues** with audio guidance
- **Animated UI Panels** with fade in/out effects
- **Data Visualization** using bubble and chord diagrams for nutritional composition
- **Drag and Drop** interaction system

## 🛠 Technologies

- **Unity 2023.x** with Universal Render Pipeline (URP) 17.0.3
- **C# 9.0**
- **Input System 1.11.2** | **Cinemachine 3.1.3** | **TextMesh Pro**

## 🚀 Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/SmoothOrbVisualizacion.git
   ```

2. **Open in Unity Hub** (Unity 2023.1+)
   - Add the project folder
   - Wait for package imports to complete

3. **Run the project**
   - Open `Assets/Scenes/SampleScene.unity`
   - Press Play

## 💻 Usage

- **Rotate**: Left-click + drag
- **Zoom**: Mouse wheel
- **Select Component**: Click on orb parts
- **Spawn Fruits**: Click UI buttons to open orb and generate fruits

## 📁 Project Structure

```
Assets/
├── Animation/          # Orb and UI animations
├── Art/                # 3D models (FBX), fruits, manual images, skybox, UI sprites
├── Audios/            # Audio clips and music
├── MaterialesTexturas/ # Materials and textures
├── Prefabs/           # Reusable prefabs
├── Scenes/            # Unity scenes
└── Scripts/           # C# scripts
    └── OrbeInfoScriptable/ # ScriptableObjects for component data
```

## 📜 Key Scripts

**UI Management**: `UIManager.cs`, `AnimationList.cs`, `DescriptionNavigation.cs`  
**Orb System**: `OrbeCollectionManager.cs`, `OrbeDisplay.cs`, `OrbeInfo.cs`, `ShowHalfSmoothOrb.cs`  
**Interaction**: `DetectMouse.cs`, `RotateWithMouse.cs`, `DragNDrop.cs`, `HoverActivator.cs`  
**Fruits**: `SpawnFruits.cs`  
**Dialogue**: `Dialogue.cs`

## 🎮 Orb Components

8 interactive layers modeled as ScriptableObjects: Outer Layer, Grooved Metal, Cryogenic Core, Blades, Window, Mix Room, Foam System, and Base.


**Note**: Academic project developed for Semester 7 as an educational visualization tool for smoothie device mechanics.

