# BATDUCK

## Introduction

this game is a very simple and fun implementation of batman in a universe where he is a duck.

*Batduck* has a mission to save *Duckham* whatever the cost.

Sadly, the city of Duckham has been destoryed by the monster Duckside and now there is nothing left but the indestructable *Duckmobile* and *Duck Signal*.

## How to play

As of the moment, there is no release version. you can copy the files in this repo in a unity project (preferably unity 6) and run the game there.

## Controls

Lets start with the BatDuck.
 - Movements: The `A` `D` are used to rotate and `W` `S` are used to move around.
    - `LSHIFT` is used to sprint.
 - States:
    - `Space` is used to get into alert mode.
    - `C` is used to get into the stealth mode.
    - `N` is used to get into the noraml mode.
 - Cameras: You can switch between different cameras with `V` key. 
 - Duck Signal: You can turn on the signal with `B` key.
    
When closed to Duckmobile, you can use the `E` key to get into the duckmobile and get out with `G` key.

the Duckmobile controls are the same as the BatDuck.`LSHIFT` is used as handbrake.

## Summary Of Works Done

### Scene

* The scene is super minimal and only has a two planes as ground and sky.

### BatDuck
    
* The 3D model was downloaded from sketchfab and imported in unity.
* A camera is attached to this character. The camera can be moved only horizontally by mouse.
* Two spot lights are connected to the character for the alert effects.
* A sound player is attached to the character.

### Duckmobile

* The 3D model was downloaded from sketchfab and imported in unity.
* a camera is attached to this car.
* The car has 4 wheels and a handbrake. The wheels and handbrake are configured in a way that makes the Duckmobile highly driftable and fun to use.

### Duck Signal

* A bicon that displayes the Bat Signal on the sky with subtle movements.

## A Quick And Bad Demonstration

![](showcase1.mp4)
<video src="./showcase1.mp4" controls preload></video>

![](showcase2.mp4)
<video src="./showcase2.mp4" controls preload></video>
