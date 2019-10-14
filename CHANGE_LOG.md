# MechJeb2 :: Change Log

* 2015-1203: 2.5.5 (MuMech) for KSP 1.0.5
	+ 1.0.5
	+ Flight Recorder Graph module
	+ Education mode option (rename SmartASS to SmartACS) see MM patch [here ](https://github.com/MuMech/MechJeb2/blob/master/MechJebEdu.cfg)
	+ Improvement to the Attitude control
	+ Dynamic Pressure limiter to replace the now useless terminal velocity
	+ Attitude control speed limiter to save some RCS
	+ Add "periapsis in target SoI" InfoItems
	+ Add "minimum DV required for capture by target" InfoItems
	+ Add "Docking guidance: Angular velocity" infoitem
	+ Add electric throttle limiter to avoid empty batteries on ion powered craft
* 2015-0706: 2.5.3 (MuMech) for KSP 1.0.4
	+ Fix a bug that prevented MJ to control some ship with 2.5.2. Sorry about that
	+ one.
* 2015-0705: 2.5.2 (MuMech) for KSP 1.0.4
	+ Built for 1.0.4
	+ New AR202 model and DDS textures
	+ Smart A.S.S. has more options for some mods (axis control disabling, ...)
	+ Live ascent path drawing on the ascent path editor
	+ Better use of control surface and auto tuning
	+ Persistent Rotation mod support
	+ Button to disable Mechjeb in the partmodule
	+ Lot of fix related to other mods compatibility (RF, Procedural Part,
	+ Procedural Fairing, ...)
		- Rework of the Staging Stats window
		- G force info Item
		- Many bug fix
* 2015-0517: 2.5.1 (MuMech) for KSP 1.0.2
	+ Lot of bug fixed related to KSP 1.0
	+ Better precision for the landing auto pilot when using parachutes
* 2015-0427: 2.5 (MuMech) for KSP 1.0.0
	+ Moved DeltaV simulation to use KER code
	+ Landing Sim code use KSP 1.0 aero model. However parachute are not properly
	+ accounted yet.
		- Display Landing prediction trajectory on the map view
		- Differential throttle control module
		- Warp to suicide burn and to atmospheric entry
		- Ascent AP Auto deploy solar panels (also an InfoItem)
		- Visual display of CoM and various velocity in the attitude menu
		- Improved velocity computation based on CoM instead of active pod
		- SMARTR module for RCS with hold relative velocity for now
		- SmartAss action can be set as vessel Actions
		- Improved node burning over multiple stages
		- Career limit related new infoItems
		- Some performance improvement
		- Reduced memory usage (lower garbage collection)
		- A memory leak fixed
		- the usual various bug fixed
		- and fixes for KSP 1.0 release
* 2014-1221: 2.4.2.0 (MuMech) for KSP 0.90
	+ Bug fix related to the Tracking Station upgrade. Some function will not be
	+ disabled until the station is upgraded.
* 2014-1215: 2.4.1.0 (MuMech) for KSP 0.25
	+ Various bug fix
* 2014-1009: 2.4.0.0 (MuMech) for KSP 0.25
	+ New interplanetary transfer with porkchop plots
	+ Add EQ DN and EQ AN to Apoapsis maneuver
	+ Add "Downrange distance" info item to flight recorder
	+ Ascent autopilot option to limit the angle of attack
	+ Maneuver to change the surface longitude the vessel will be over after one
	+ orbit
		- Added Force Roll option to Ascent AP
		- Add more options to SmartASS Surface Mode
		- Support for external gimbal extension (see Sarbian's signature for
	+ KM_Gimbal support)
		- Handles engines with spool time better
		- UI rescale setting
		- Added a button in KSP stock AppLauncher. See settings to hide the menu
	+ handle
		- Import landing site from KerbTown/Kerbal-Konstructs and RSS.
		- Import Landing sites and Runways from users built files (see
	+ <https://github.com/MuMech/MechJeb2/blob/dev/LandingSites.cfg>)
		- Don't reenable SmartASS after another autopilot finishes (and an option to
	+ disable it in the windows editor Misc category)
		- In flight lock of some control while the mouse is over MJ windows
		- Replace the arrow selector of some menu with a drop down. Those who don't
	+ like it can deactivate it in MJ2 Settings
		- Various improvement to engine and gimbal torque information and use
		- Various fix
	+ Thanks to those who submited patch for this release : BloodyRain2k, Meumeu,
	+ sanedragon, Wetmelon, xytovl
* 2014-0724: 2.3.1.0 (MuMech) for KSP 0.24
	+ Only changed the Compatibility Checker to allow 0.24.1
* 2014-0724: 2.3.1.0 (MuMech) for KSP 0.24
	+ Only changed the Compatibility Checker to allow 0.24.1
* 2014-0724: 2.3.1.0 (MuMech) for KSP 0.24
	+ Only changed the Compatibility Checker to allow 0.24.1
* 2014-0717: 2.3.0.0 (MuMech) for KSP 0.24
	+ Compatible with KSP 0.24;
	+ 0.24 changed some Parachute code. MJ landing with parachutes will be less precise until Squad fix a bug;
	+ Hohmann transfer calculation to always find the first window;
	+ Burn with RCS when not other engine is available;
	+ Ascent AP can now schedule launches at interplanetary transfer windows;
	+ More work on the docking AP;
	+ Make orbital calculations compatible with RSS;
	+ Add "Escape Velocity" info item;
	+ Adds SUN as a Reference for ASAS adv mode;
	+ And many bugs fixed;
* 2014-0506: 2.2.1.0 (MuMech) for KSP 0.23.5
	+ No changelog provided
