# ClusterLaster: "SpiderPiggy 3D: Let´s Cart!" (3D-Game, Multiplayer)

![Logo](https://user-images.githubusercontent.com/94470276/215739062-16b5e568-a96b-4c7c-9266-db3915d0f6a4.png)


SpiderPiggy3D ist ein 3D-Spiel für den Game-Engines Kurs des Studiengangs IMI an der HTW-Berlin im Wintersemster 22/23.

## Getting Started

WICHTIG: Das Spiel ist primär für Adroid-Smartphones und sekundär für Windows (10 und 11) entwickelt und lässt sich ausschließlich auf diesen Plattformen spielen.

Um das Spiel zu starten muss das letzte Release heruntergeladen und die darinbefindliche <code>spidercart.apk</code> auf das Android-Smartphone kopiert werden und dort installiert werden (Installation aus fremden Quellen muss aktiv sein!). Unter Windows muss die <code>SpiderPiggy.exe</code> gestartet werden.

Viel Spaß beim spielen!

## Beschreibung

Bei unserem Kart Racing Game geht es darum, als erster ins Ziel zu kommen und das so schnell wie möglich. 

https://user-images.githubusercontent.com/68195151/216120740-8c8fed7b-61f9-4ff1-b4eb-d66bc74a87fb.mp4

### Genre & Art Style
3D Racing Game, Fun-Racer

## Features

### Player 

Der Player (Als Prefab implementiert) wird gesteuert mit den WASD Tasten oder mit dem Touchpad.
Es gibt 2 spielbare Charaktere: SpiderPiggy und Evil SpiderPiggy. Beide haben jeweils ein eigenes Kart.

### Obstacles

Als Hindernis für die Spieler ist Möwenkacke auf der Straße verteilt. Sollte ein Spieler über diese fahren, rutscht er für einen Augenblick aus und mus sich wieder neu orientieren. Da dies Zeit kostet, sollten Kollisionen vermieden werden. Die Möwen-Hinterlassenschaften sind überwiegend in der zweiten Hälfte der Strecke verteilt, damit die erste Hälfte einen leichteren Einstieg ermöglicht.

### Multiplayer

## Das Spiel

### Start Menü

Zu Beginn des Spiels erscheint das Start Menü.

![menü](https://user-images.githubusercontent.com/94470276/215739865-04a02076-c945-4b94-9a53-b63cac132fe7.png)


* Race On - Das Main-Game startet
* Quit - Das Spiel wird beendet

### Configuration Menü

Vor Beginn des Spiels kann der Nutzer hier auswählen mit welcher Piggy-Kart-Kombo er spielen will.

![grafik](https://user-images.githubusercontent.com/68195151/216106081-e373e25e-856a-40fe-9e33-922bfd420de6.png)

### Muliplayer-Lobby

In der Muliplayer Lobby sammeln sich die spieler die zusammen Spielen wollen, bis zu vier Spieler können gleichzeitig spielen. Der erste Spieler im Raum ist der Admin und nur dieser kann das spiel starten.

![grafik](https://user-images.githubusercontent.com/68195151/216109766-70e5b81c-c195-4a0e-9def-eba31cbdf05a.png)

### Main Game

Das Herzstück des Spiels: so schnell wie möglich die strecke fahren und als erster ins Ziel kommen. 

![grafik](https://user-images.githubusercontent.com/68195151/216106620-c18c2660-0478-4868-ba66-b70d051ab6ca.png)
![grafik](https://user-images.githubusercontent.com/68195151/216107125-b7ace2d7-010e-4f90-8eae-8c79415af6ae.png)
![grafik](https://user-images.githubusercontent.com/68195151/216107277-419476ae-0e0a-4faf-be87-2147203b9989.png)
![grafik](https://user-images.githubusercontent.com/68195151/216107646-76ff0927-0776-4baf-9f39-4c6bc74e609e.png)

### Race Complete Menü

Wenn der Player ins Ziel fährt, erscheint das Race-Complete-Menü. Hier wird der aktuell erreichte Platz, die Zeit, sowie der bisherige Highscore angezeigt.

![grafik](https://user-images.githubusercontent.com/68195151/216105574-3bc0287f-254a-4ca5-a048-82aa8c1e2304.png)

* Race Again - Das Main-Game startet erneut
* Main Menu - Rückkehr zum Startmenü

### Player Control

Der Player kann sich mit der Steuerung fortbewegen, von Plattform zu Plattform schwingen und Items einsammeln. Zudem kann er sich sprunghaft dem Seil entlang hochziehen. Einige dieser Aktionen werden durch Items beeinflusst und verbessert.

Tasten:
A & D: Nach links bzw. rechts bewegen / schwingen
Q: Am Seil hochziehen
Leertaste & W: Springen -> Mehrere Sprünge sind möglich, Sprung kann auch in der Luft verwendet werden. Nach drei Srpüngen setzt eine Cooldown-Zeit ein.
Linker Mausklick: An der Wand in Richtung des Maus-Icons wird ein Haken platziert, um das Seil zu spannen. In freier Luft kann kein Haken gesetzt werden. Wenn bereits ein Seil gespannt ist, wird mit einem weiteren Mausklick das Seil entfernt. 

### Team
Max Decken, Colin Garms, Alexander Ehrenhöfer, Leah Sommer

### Built With
Software
* Unity Version 2021.3.11f1

### Resources
#### Assets
* Das ursprüngliche Design des SpiderPiggys ist mit Dall-E (https://openai.com/dall-e-2/) generiert worden. Die 3D-Version des SpiderPiggys ist von uns selbst per Hand anhand des ursprünlichen Design modelliert und texturiert worden.

#### Music
* Game Soundtrack: https://pixabay.com/de/music/optimistisch-electro-pop-124340/

#### Sounds
* Countdown: https://elements.envato.com/de/race-countdown-7DDRG4V


#### Code
* Für die Mechanik der Checkpoints wurde sich an einem Tutorial orientiert, jedoch für den Multiplayer stark überwarbeitet. (https://www.youtube.com/watch?v=IOYNg6v9sfc&t=522s&ab_channel=CodeMonkey)   Zugrif am 13.01.2023, 19:55 Uhr
* Die Steuerung wurde zum Teil von der Steuerung des Kartes im Unity-Karting-Game inspriert, aber angepasst und abgewandelt. (https://learn.unity.com/project/karting-template) Zugrif am 01.02.2023, 17:54 Uhr
