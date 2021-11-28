# SonosController

This project is intended to be a replacement for the official sonos S1 controller which has certain undesirable features and some expected features not present.

Projects in the solution.

- Comms - this project is intended to handle all file and network IO including the SOAP webservices which Sonos uses and the UPNP discovery of the Sonos components.
- MusicData - this project contains all classes related to the music library, i.e. albums, artists, tracks etc.
- SonosCon - this projects is the UI and uses both Comms and MusicData. Ideally the code should only be the form related events.
