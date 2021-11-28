# SonosController

This project is intended to be a replacement for the official sonos S1 controller which has certain undesirable features and some expected features not present.

Projects in the solution.

- Devices - this project contains all the base objects for plaers, zones etc.
- Services - This project is in a separate repository, https://github.com/JohnnyOldBoy/Services. This project handles all file and network IO including the SOAP webservices which Sonos uses and the UPNP discovery of the Sonos components, plus utility functions used by various vew models. It is kept seperate as the functionality contained within could also be used for different Sonos controller project, e.g. a web application.
- MusicData - this project contains all classes related to the music library, i.e. albums, artists, tracks etc.
- SonosControllerWPF - this projects is the UI and uses Devices, Services and MusicData and all the view models.
