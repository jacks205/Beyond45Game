ChapmanMetalSlug
================

CPSC 340 2d Side Scroller Shooter

#Design Document

Contents
-------------
1. [Game Overview] (#gameplayoverview)
2. [Gameplay Mechanics] (#gameplaymechanics)
3. [Controls] (#controls)
4. [Interface] (#interface)
5. [Menu and Screen Descriptions] (#menu)
6. [Game World and Levels] (#gameworld)
7. [Game Progression] (#progression)
8. [Characters] (#characters)
9. [Enemies] (#enemies)
10. [Weapons] (#weapons)
11. [Design Notes] (#notes)
12. [Weekly Blog Posts](#posts)
13. [Future Features] (#futurefeatures)


Version History
---------------------

Version | Draft | Description
-----------|--------|-----------------
Draft | 10/2/2014 | Initial Draft
0.1 | 10/22/2014 | Revision and Addition of Story and Level

##<a name="gameplayoverview"></a>Gameplay Overview

This game will be a classic side scroller 2D shooter that would resemble games such as Metal Slug and Contra. The story is not currently set, I have been debating with several ideas that revolve around a Rambo-like story of infiltrating an enemy compound, either saving people and blowing something up (or both), and facing a boss.

##<a name="gameplaymechanics"></a>Gameplay Mechanics

Class arcade style of 2D Shooter Platformer. Main focus is to utilize a Xbox controller to control the movement and aiming with separate joysticks.

Sample gameplay can be found in my [Weekly Blog Posts](#posts) and will constantly be updated on my thought process of the game until release.

##<a name="controls"></a>Controls
Following Unity's [Xbox360 Controller Mapping](http://wiki.unity3d.com/images/a/a7/X360Controller2.png):

    - Left Joystick
      - Movement of player
    - Right Joystick
      - Alignment of weapon
    - A
      - Jump
    - B
      - Grenades
    - Right Trigger
      - Shooting

##<a name="interface"></a>Interface

![Example Interface](Assets/Ideas/UIExample.png)


1. Health Bar
  - Current health of player
2. Grenade Ammo
  - Shows current number of grenades
3. Current Weapon
  - Will show current weapon player is holding
4. Damage
  - Enemies will have their lost hitpoints hover as shot
  - Most likely will be implemented for boss fights
5. Lives
  - Amount of lives the player has left until "Continue?"

##<a name="menu"></a>Menu and Screen Descriptions

Simple menu that consists of "Start", "Continue", and "Quit". The player can continue where they previously left off in their most recent checkpoint.

##<a name="gameworld"></a>Game World and Levels

What if in 1939 the Molotov–Ribbentrop Pact actually held, and Nazi Germany never decided to go with Operation Barbarossa in 1941? That means Hitler wouldn't of overextended his forces into Russia, causing Germany to lose the war. Without the east to worry about, he focuses his attention to the west and south, successfully developing a foothold in the UK and Africa and prolonging the war past 1945. 

The main character will travel across different environments, consisting of deep forests in Europe and Siberia, dry desert of Africa and the Middle East, and infiltrating secret Nazi/Communist compounds.

##<a name="progression"></a>Game Progression

Progression through the game will be tracked by a checkpoint system. Markers on the game levels will save checkpoints of the player, and each death will bring them back to the most recent checkpoint. 

The player will also have a limited number of lives that, when they run out, the play must start the current level all over again.

##<a name="characters"></a>Characters

Main Hero: United States Commando sent behind enemy lines to disrupt and destroy key enemy positions in order to advanced the Allied movement toward bringing down the Nazis and Russians.

##<a name="enemies"></a>Enemies

Infantry: Shoot at slow rate and and reasonably accurate. Light health and are susceptible to rockets and grenades. Can be deadly behind cover.

Tanks: Large Shell that takes away a lot of damage, shoots at a very slow rate. Bullets do nearly no damage to them, must take advantage of rockets and well placed grenades.

Jeeps: Health is between infantry and tanks, but still takes a lot of bullets to kill. Fast rate of fire that is inaccurate and sometimes unpredictable.

##<a name="weapons"></a>Weapons

[Weapon Examples](https://www.youtube.com/watch?v=JPiwHO6HJ2U)

Grenades: Able to pick them up, to a limit of 5. Thrown in upwards angle to reach targets behind cover. Effective against infantry.

Rockets: Able to pick them up for a limited amount of time. Effective against Tanks and Jeeps, and able to blow up cover.

##<a name="notes"></a>Design Notes

Deciding between having the levels fluidly join together or might have story between levels as if a new mission each time.

##<a name="posts"></a>Weekly Blog Posts
1. [Initial Panic](http://jacks205.blogspot.com/2014/09/week-1-initial-panic.html)
2. [Not Much Panic](http://jacks205.blogspot.com/2014/09/week-2-not-much-panic.html)
3. [Who Do I Shoot?](http://jacks205.blogspot.com/2014/10/week-3-who-do-i-shoot.html)
4. [New Style and Progess!](http://jacks205.blogspot.com/2014/10/week-4-new-style-and-progess.html)

##<a name="futurefeatures"></a>Future Features

