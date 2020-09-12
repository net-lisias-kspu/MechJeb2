
The Attitude Adjustment panel controls the settings for the PID controllers that MechJeb uses for controlling the attitude of vessels.

Table of contents
-----------------
- [PID Controller Selection](#pid-controller-selection)
- [BetterController Design](#bettercontroller-design)
- [BetterController Settings](#bettercontroller-settings)
- [BetterController Tuning](#bettercontroller-tuning)
- [Wobble Rockets](#wobble-rockets)

PID Controller Selection
------------------------

There are several different PID controllers to choose from:

- MJAttitudeController
- KosAttitudeController
- HybridController
- BetterController

The MJController is the original historical PID controller, this was supplemented with the PID controller from Kos and and "Hybrid" version that merged some of the code from the old MJ controller.  The
current recommended controller is the "BetterController" which is what this document will be concerned with.  There is no other documentation for the other PID controllers other than the source code.

BetterController Design
-----------------------

The Design of the BetterController is based on the [`ALT_HOLD` mode of ArduCopter 2.9](https://archive.is/NqoUm).  Since MechJeb is concerned with holding an attitude in pitch, yaw and roll and not in fly-by-wire enhancement of user-supplied pitch, yaw and roll the altitude hold mode is more appropriate than the pitch, yaw and roll PIDs that ArduCopter uses.  The user input is treated as an external disturbance to be corrected to get back to the reference attitude.

The BetterController does not use the third cascade stage for Acceleration control since this information was not reliable enough and didn't seem to produce any benefits in stability.  The first two stages are implemented with the same kind of P/sqrt(P) control of angular position to PID control of angular velocity.  The result is a PID controller which is fast, with minimal overshoot, with the same smooth decrease in Acceleration.

BetterController Settings
-------------------------



BetterController Tuning
-----------------------

Wobble Rockets
--------------

After initially enabling "KILL ROT" if the PIDs jitter wildly then you have a PID tuning problem (see above), and need more LD or less Kp.  If instead you have a large wobbly rocket, and you observe that the pitch and/or yaw terms start to oscillate slowly between the rails and the rocket begins to wobble in a circle, then you have a wobble rocket problem.  This is not a bug in MechJeb, this is a rocket design problem.  KSP is designed to be a ragdoll rocket simulator which is fun for players, but an active area of research for PhD scientists in Astronautical Engineering.  At one point I considered a sort of noise-cancelling headphones design by using singular spectrum analysis (SSA) to do model-free identification of trend, oscillations and noise and feed the oscillations back to adaptively cancel the signal in the error estimate, but after burning a solid weekend getting to the point where I had eigenvalues and eigenvectors pulled out of the SSA but no idea of the heuristics to use to extract the oscillations that was abandoned forever.  Your vessel, therefore, is not supportable by MechJeb and this problem is not fixable in MechJeb.  You must either install Kerbal Joint Reinforcement or do something like setup autostruts from every part in your rocket to every grandparent part (mods like Editor Extensions Redux make this easy), and then remove any that cause issues with joints snapping, in order to stiffen up the vessel.