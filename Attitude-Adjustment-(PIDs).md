
The Attitude Adjustment panel controls the settings for the PID controllers that MechJeb uses for controlling the attitude of vessels.

Table of contents
-----------------
- [PID Controller Selection](#pid-controller-selection)
- [BetterController Design](#bettercontroller-design)
- [BetterController Settings](#bettercontroller-settings)
- [BetterController Tuning](#bettercontroller-tuning)
- [BetterController Troubleshooting](#bettercontroller-troubleshooting)
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

-   **Maximum Stopping Time (sec)**    
    This option is primarily meant for conserving RCS fuel and caps the maximum angular velocity by the time it will take to stop.  Setting this to 2 seconds means that vessels will typically use 2
    seconds of RCS fuel to initiate a turn and 2 seconds to stop.  For vessels with large amounts of SAS (i.e. stock) this will make no difference since they will turn much faster (default: 2 seconds).

-   **Minimum Flip Time (sec)**
    For very large vessels/bases with underpowered torque the Maximum Stopping Time can result in incredibly long times to slew around in a circle.  We care about conserving RCS fuel, but then we get
    impatient.  This setting overrides the Maximum Stopping Time
    setting to increase the allowed angular velocity for vessels that will take a very long time to slew around.  This is expressed as the time to turn 180 degrees, not including the additional spin-up
    and spin-down time (default: 20 seconds).

-   **Roll Control Range (deg)**
    When the error in the attitude is greater than this value, the controller will kill the roll velocity and focus on correcting pitch and yaw.  When the error is within this range the vessel will
    adjust the roll error (default: 5 degrees).

-   **LD (rad)**
    This is the only setting for the error in the angular position.  It controls how far away from zero the transition is between the linear-P and sqrt(P) behavior.  Adjusting this value down is 
    effectively more gain, but produces more overshoot.  Adjusting it up will increase the time taken to settle on the correct attitude (default: 0.1).

-   **Kp**
    This is the P gain of the velocity PID.  The higher this value is the less overshoot and the more snappy the control will be.  Adjusting this too high will result in jittering controls (default: 50.0)

-   **Ki**
    This is the I gain of the velocity PID.  It is not recommended to set this different from zero (default: 0.0).

-   **Kd**
    This is the D gain of the velocity PID.  Small amounts of D gain may reduce overshoot and allow the velocity to track better and reduce overshoot, larger amounts produce jitter (default: 0.0).


BetterController Tuning
-----------------------

The first thing to do is to use SMART A.S.S to either KILL ROT or point at some direction (e.g. normal or anti-normal).  Set the LD to something reasonable like 0.10 and Ki and Kd to 0.0.  Bring the Kp up until there is observable jitter in the pitch/yaw/roll controls.  For excessive jitter the "Actuation" column may be flipping between +1.000 and -1.000 so fast that the KSP UI output of the controls isn't jittering, but the vessel is slowly losing tracking control, back down until the "Actuation" column shows numbers near 0.001 or just 0.000.  When you find a value where it starts to jitter (reducing it 5-10% eliminates the jitter) then drop the Kp value by half of that value.  There may still be a little bit of noise less than 0.010 which should be fine.  Typical values will be in the range of 10-100.

Once you have found the highest tolerable Kp value that does not produce jitter, start to adjust LD.  Do this by observing when you change the target (normal to anti-normal and back or whatever) that the motion is smooth and fast and without overshoot.  You want to find the lowest value which does not produce any overshoot.  Lower LD values is effectively higher gain.  Watch the pitch and yaw controls as you approach the target.  They should be saturated opposing the motion and then "snap" down to about 50% and then smoothly settle on zero.  If there is no "snap" then LD probably has room to become more aggressive and to be reduced.  If it "oversnaps" and reverses itself then you have overshoot and should increase LD a bit.

For bonus points you can attempt to add some Kd up until you start to see the controls jitter, which may allow you to push the LD slightly lower and produce more optimally effective controls.

Note that the default tuning values produce good results with larger vessels with insufficient control, keeping them reasonably fast with little to no overshoot, while allowing a little bit more overshoot for smaller rockets with excessive torque.


BetterController Troubleshooting
--------------------------------

In order to troubleshoot it may be better to just go through the PID tuning advice above in order.

How to remove overshoot:


Wobble Rockets
--------------

After initially enabling "KILL ROT" if the PIDs jitter wildly then you have a PID tuning problem (see above), and need more LD or less Kp.  If instead you have a large wobbly rocket, and you observe that the pitch and/or yaw terms start to oscillate slowly between the rails and the rocket begins to wobble in a circle, then you have a wobble rocket problem.  This is not a bug in MechJeb, this is a rocket design problem.  KSP is designed to be a ragdoll rocket simulator which is fun for players, but an active area of research for PhD scientists in Astronautical Engineering.  At one point I considered a sort of noise-cancelling headphones design by using singular spectrum analysis (SSA) to do model-free identification of trend, oscillations and noise and feed the oscillations back to adaptively cancel the signal in the error estimate, but after burning a solid weekend getting to the point where I had eigenvalues and eigenvectors pulled out of the SSA but no idea of the heuristics to use to extract the oscillations that was abandoned forever.  Your vessel, therefore, is not supportable by MechJeb and this problem is not fixable in MechJeb.  You must either install Kerbal Joint Reinforcement or do something like setup autostruts from every part in your rocket to every grandparent part (mods like Editor Extensions Redux make this easy), and then remove any that cause issues with joints snapping, in order to stiffen up the vessel.