# ViveGame
A Unity project for testing spatial navigation in virtual reality. 

## About the project
This was created at Iowa State University's Virtual Reality Application Center as part of the summer Research Experience for Undergraduates, Summer 2017. 

Contributors: Devi Acharya, Emanuel Bustamante, Alfredo Velasco

Mentors: Lucia Cherep, Jonathan Kelly, Ph.D.

## Overview
This project was created in Unity 5.6.1 using the SteamVR plugin and was built for the HTC Vive. 

Measurements are taken based around the Navigation Lab at Iowa State University. 


## Contributing to project
Download this project and open in Unity. 

### Project structure
The project is organized in folders given different types of files. To begin, open the Scenes folder and open one of the scenes within. 

#### Scenes
The Training scene is used for training participants. It contains the three markers and the camera for the player. Markers spawn at two different start positions and triangles have two turn angles. 

The Game scene is for the main experiment. Landmarks can be toggled on / off in the hierarchy. 

MarkerScene2 is simply a sandbox scene used for testing scripts. It is not used in the experiment. 

#### Scripts
Scripts can be accessed from the Scripts folder. 

Scripts marked "Training"* are similar to those of the same name, but used for the training scene and have slightly different funcitonality. 

Restart handles resetting the scene on keypress (space bar) and the main array of triangle angles 

Collide and MarkerDestroy handle the collider that lets the player make contact with markers. 

StartMarkerSpawn / FirstMarkerSpawn / SecondMarkerSpawn deals with spawning the markers at appropriate distance / angle on contact. 

LaserPointer deals with the different navigation interfaces -- letting players move around map using different ways to teleport, walk, etc. and respond. It also handles much of the writing to file.

Writeposition deals with writing the player's current position to file. 

#### Hierarchy
[SteamVR] is part of the SteamVR imported package. No need to change anything here. 

GameController handles the overall state of the game and scene restarts. 

Plane and Lights are part of the base environment.

CameraRig controls the navigation space and tracks the player's position within it. 


## Testing the Experiment
The Unity project can be tested at any time by pressing "Play." If the Vive and SteamVR are running, this scene should be playable in the Vive. 

## Running the experiment 
### Before running experiment 
For testing participants, it is recommended to first build EXEs for each environment and interface. 

You can change the environment by toggling landmarks on / off in the hierarchy. You can change the nav interface in LaserPointer.cs

Then simply build the project. 

### Running experiment 
Once you have the appropriate set of .EXEs, just open SteamVR and the appropriate EXE and it should run the experiment
For more info on how to run the experiment, check out this link: http://tinyurl.com/runexperiment



