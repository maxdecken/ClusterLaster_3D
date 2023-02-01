# ClusterLaster: "SpiderPiggy 3D: Let´s Cart!" (3D-Game)

![Logo](https://user-images.githubusercontent.com/94470276/215739062-16b5e568-a96b-4c7c-9266-db3915d0f6a4.png)


SpiderPiggy3D ist ein 3D-Spiel für den Game-Engines Kurs des Studiengangs IMI an der HTW-Berlin im Wintersemster 22/23.

## Getting Started

WICHTIG: Das Spiel ist primär für Smartphones und sekundär für Windows (10 und 11) entwickelt und lässt sich ausschließlich auf diesen Plattformen spielen.

Um das Spiel zu starten muss entweder das letzte Release oder der Inhalt des Ornder <code>/builds/windows</code> heruntergeladen und die darinbefindliche <code>SpiderPiggy.exe</code> gestartet werden.

Viel Spaß beim spielen!

## Beschreibung

Bei unserem Kart Racing Game geht es darum, als erster ins Ziel zu kommen und das so schell wie möglich. 

### Genre & Art Style
3D Racing Game, Fun-Racer

## Features

### Player 

Der Player (Als Prefab implementiert) wird gesteuert mit den WASD Tasten oder mit dem Touchpad.
Es gibt 2 Characters: SpiderPiggy und Evil SpiderPiggy. Beide haben jeweils ein eigenes Kart.

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

### Main Game

Das Herzstück des Spiels: so schnell wie möglich die strecke fahren und als erster ins Ziel kommen. 


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
* Das SpeiderPiggy ist mit Dall-E (https://openai.com/dall-e-2/) generiert worden und dann händisch in Einzelteile aufgeteilt und geriggt worden.
* Die Möwe wurde ebenfalls mit Dall-E (https://openai.com/dall-e-2/) generiert und in Photoshop in mehrere Tiles umgesetzt.

#### Music
* Game Soundtrack: https://pixabay.com/de/music/optimistisch-electro-pop-124340/

#### Sounds
* Countdown: https://elements.envato.com/de/race-countdown-7DDRG4V


#### Code
* Für die Mechanik der Checkpoints wurde sich an einem Tutorial orientiert, jedoch für den Multiplayer stark überwarbeitet. (https://www.youtube.com/watch?v=IOYNg6v9sfc&t=522s&ab_channel=CodeMonkey)   Zugrif am 13.01.2022, 19:55 Uhr
