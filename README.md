## Introduction

The Godfather script's name comes from an early SA-MP gamemode.
I thought it was a nice idea to bring the nostalgia back in GTA:V and decided to rework the original idea into a new gamemode for GTA:Network.


Features

"Dynamic" means it can be altered within the game by Game Admins (so without touching code.)


### [v1.7]
Added WebRTC voice chat.


### [v1.6]

Added CreateGroup UI.
Removed BCrypt hashing.
Implemented date/time tokens for web-gameserver communication.
Added latest SQL data structure.


### [v1.5]
Added temporary account models for both WebCP and Server until NET.CORE is available.
Updated HTTPController and listening service.
All browsing is done remotely - all web client files are deleted.
User authentication is done on the Web server.
Rewrote HTTPController to receive JSON data.
Game server's listening service runs on port 3001.
Added Materialize to WebCP.
Game server/web server communication is done with session tokens.
Integrated Account Login/Registration.
Integrated Character Login/Registeration.
Updated web/game design.


### [v1.4]
Code refactored.
Server now runs well on compiled version.
Added HTTPController for Node.js implementation and server-controlled browsing.
Added SessionIDs.
Added settings for web- and listening server.
Added documentation to WebCP (soon viewable!).

### [v1.3]
Further implemented Entity Frameworks. Many structures were reworked: referencing is now done primarily through keys/indexes to ensure expandibility.
WebCP has been converted into an ASP.Net Core website.
Added storage for characters, vehicles, properties and groups.
Updated godfather-template.sql

### [v1.2]
Implemented Entity Frameworks (huge thanks to [USER=2008]@Eraknelo[/USER] for providing ideas).
Restructured all systems to Models and Controllers.
Updated godfather-template.sql

### [v1.1]
Added many in-world properties for groups (Police Departments, Hospitals, Prison, Stores, Gas Stations).
Added car trunk/hood functionality with shortcuts (K, I).
Reworked entire group system to support multiple groups per character.
Added StorageManager (storage template for characters, properties and vehicles).
Added Fisherman job.
Added business products.

### [v1.0]
One account system: Multiple characters can be created.
Dynamic Group System
Dynamic Job System
Dynamic Property System
Dynamic Vehicle System
Web Control Panel


## Characters
You can create multiple characters within one account. All character data is stored on a character-level, not on the account.

1. A character can hold administrator privileges.
2. A character can join a group (see below what groups are).
3. Characters can train their skills with jobs or activities.
4. These skills are translated into experience points (XP). This determines the character's level.
5. Characters can own properties.
6. Characters can own vehicles.



## Groups

A character can join or be invited to a group. Groups can be anything ranging from a faction, gang, business, organization, anything! Groups can also have an "Extra Type". For example, a food store business has the "business" type, and the "food store" extra type. Group types and extra types can be dynamically allocated. There are 9 pre-defined group types that have scripted commands associated to them:

Law Enforcement Agency
Military
Medical Department
Correctional Facility
Hitman Agency
Taxi Cab Company
News Network
Criminal Organization
Business

Communication is done through a portable or department radio.
A group has a rank and division system. There are unlimited ranks and divisions.
Vehicles can be assigned to a group.
Properties can be assigned to a group.


## Properties
Properties can be owned by a character or a group. There are three property types:

## House
Door
Building

Houses and buildings can hold items, like weapons, ammo, drugs and other rare goods. A group's assets are calculated by how much is available in their properties. For example: If a player owns one group (e.g. "WallyMart", type business, extra type Food Market), they can own multiple properties (buildings) that all have different levels of stock. The challenge for "Wally" is to take care of his properties' sales and stock, so business' revenue is guaranteed.


## Vehicles

Vehicles can be owned by a character or a group. Each vehicle has a trunk storage, in which items, weapons, ammo, drugs and other rare goods can be stored.

## Web Control Panel
The web control panel is made with the open source AdminLTE theme. Nothing new :=)
The web interface allows you to:
Display your account's statistics
View your characters
Manage accounts / characters.
Manage groups
Manage properties
