# ClusterLaster: "SpiderPiggy 3D: Let´s Cart!" (3D-Game)

![Logo](https://user-images.githubusercontent.com/94470276/215739062-16b5e568-a96b-4c7c-9266-db3915d0f6a4.png)


SpiderPiggy3D ist ein 3D-Spiel für den Game-Engines Kurs des Studiengangs IMI an der HTW-Berlin im Wintersemster 22/23.

## Getting Started

WICHTIG: Das Spiel ist primär für Smartphones und sekundär für Windows (10 und 11) entwickelt und lässt sich ausschließlich auf diesen Plattformen spielen.

Um das Spiel zu starten muss entweder das letzte Release oder der Inhalt des Ornder <code>/builds/windows</code> heruntergeladen und die darinbefindliche <code>SpiderPiggy.exe</code> gestartet werden.

Viel Spaß beim spielen!

## Beschreibung

Bei unserem Cart Racing Game geht es darum...

### Genre & Art Style
3D Racing Game, Fun-Racer

## Features

### Player 

Der Player (Als Prefab implementiert) wird gesteuert mit den WASD Tasten.
Das Seil wir mit der linken Maustaste bedient.
Der Player kann auch Items wie Äpfel, Bananen, etc. einsammeln, welche jeweils unterschiedliche Vorteile bringen.

### Obstacles

Als Hindernis für die Spieler ist Möwenkacke auf der Straße verteilt. Sollte ein Spieler über diese fahren, rutscht er für einen Augenblick aus und mus sich wieder neu orientieren. Da dies Zeit kostet, sollten Kollisionen vermieden werden. Die Möwen-Hinterlassenschaften sind überwiegend in der zweiten Hälfte der Strecke verteilt, damit die erste Hälfte einen leichteren Einstieg ermöglicht.

### Multiplayer

## Das Spiel

### Start Menü

Zu Beginn des Spiels erscheint das Start Menü.

![grafik](https://user-images.githubusercontent.com/68195151/204773267-c47d8269-983f-46c3-b9e8-942706275c5d.png)

* Start - Das Main-Game startet
* Tutorial - Das Tutorial startet
* Quit - Das Spiel wird beendet

### Main Game

Das Herzstück des Spiels: Die sich automatisch / zufällig während dem Gameplay aufbauende Endlessrunner-Szene. Hier geht es darum den höchstmöglichen Score zu erreichen und dabei nicht zu sterben. In das Feuer zu fallen oder die "ESC"-Taste drücken beendet das Spiel.

![grafik](https://user-images.githubusercontent.com/68195151/204862896-e4f63271-089c-4085-9fc9-c3c930ae9225.png)
![grafik](https://user-images.githubusercontent.com/68195151/204863417-930feb32-66b9-4296-91a4-81af09f4d1a1.png)

### Tutorial Level

Hier wird dem Spieler in einem Mini-Level die Steuerung erklärt.

![grafik](https://user-images.githubusercontent.com/68195151/205256954-2ae90333-2e44-4153-8ee7-3145107c485f.png)

* Start - Das Main-Game startet
* Main Menu - Rückkehr zum Startmenu


### GameOver Menü 

Wenn der Player stirbt erscheint das Game-Over-Menü. Hier wird der aktuell erreichte Score sowie der bisherige Highscore angezeigt.

![grafik](https://user-images.githubusercontent.com/68195151/204773755-0328c6cf-c544-4e68-b3a7-a1408735b4ab.png)

* Try Again - Das Main-Game startet erneut
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
* Das SpeiderPiggy ist mit Dall-E (https://openai.com/dall-e-2/) generiert worden und dann händisch in Einzelteile aufgeteilt und geriggt worden.
* Die Möwe wurde ebenfalls mit Dall-E (https://openai.com/dall-e-2/) generiert und in Photoshop in mehrere Tiles umgesetzt.

#### Music
* Game Soundtrack: https://pixabay.com/de/music/frohliche-kinderlieder-fun-happy-50s-pop-113298/

#### Sounds
* Item Sound 1: https://freesound.org/people/BloodPixelHero/sounds/591706/


#### Code
* Für die Mechanik der Checkpoints wurde sich an einem Tutorial orientiert, jedoch für den Multiplayer stark überwarbeitet. (https://www.youtube.com/watch?v=IOYNg6v9sfc&t=522s&ab_channel=CodeMonkey)   Zugrif am 13.01.2022, 19:55 Uhr
