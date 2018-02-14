

The Ascent Guidance of Mechjeb allows to easily get a rocket into a stable,
circular orbit with a variety of options. Launches can also be timed to
rendezvous with a specific target or can simply be launched when ready.

Table of contents
-----------------
- [Launch process](#launch-process)
- [Main Actions](#main-actions)
- [Main Options](#main-options)
- [Ascent Profile](#ascent-profile)
  * [Edit Ascent Path](#edit-ascent-path)
    + [Classic Ascent Profile Options](#classic-ascent-profile-options)
    + [Stock-style GravityTurn](#stock-style-gravityturn)
    + [Powered Explicit Guidance](#powered-explicit-guidance)
- [Target Options](#target-options)

Launch process
--------------

There are four main parts to the launch process, and the exact altitudes
at which they occur are customizable:

1. Initial ascent
2. Gravity turn
3. Coast to the edge of the atmosphere (if applicable)
4. Circularization burn at apoapsis

Main Actions
------------

-   **Show navball ascent path guidance**    
    Creates a targeting reticule on the navball display to point towards while
    launching (NOTE: this creates a virtual target which will unset any target
    you currently have set)

-   **Engage/Disengage autopilot**    
    Engages the autopilot or (if engaged)
    disengages the autopilot

Main Options
------------

-   **Orbit altitude [km]**    
    The desired altitude in kilometres for the
    final circular orbit

-   **Orbit inclination [°]**    
    The desired inclination in degrees for the final
    circular orbit

-   **Prevent overheats**    
    Limits the throttle to prevent parts from
    overheating

-   **Limit Q to [pa]**  
    Limits the [maximal dynamic pressure](https://en.wikipedia.org/wiki/Max_Q).
    This avoids that pieces break off during launch because of atmospheric
    pressure.
    **TODO**: *specify when to use it*

-   **Limit acceleration to [m/s²]**  
    Never exceed the acceleration during ascent

-   **Limit throttle to [%]**  
    Never exceed the percentage of the throttle during ascent
    
-   **Keep limited throttle over [%]**  
    Never go below the percentage of the throttle during ascent
    
-   **Electric limit Lo [%] Hi [%]**  
    **TODO**
    
-   **Force Roll climb [°] turn [°]**  
    **TODO**

-   **Limit AoA to [°] (0.0°)**  
    **TODO**
    -   **Dynamic Pressure Fadeout [pa]**  
        **TODO**

-   **Corrective steering**    
    Will cause the craft to steer based on the more accurate velocity vector
    rather than positional vector (large craft may actually perform better with
    this box unchecked)

-   **Auto-stage**    
    The autopilot will automatically stage when the current stage has run out of
    fuel
    
    - **Delays: pre: [s] post: [s]**    
      The autopilot will pause the actual staging before (pre) and after (post)
      for each stage.
      
    - **Clamp AutoStage Thrust [%]**    
      **TODO**

    - **Stage fairing when:**    
      Stage the fairing independently of the actual staging process, whenever
      all (or any? **TODO**) the following conditions are met:
      - dynamic pressure smaller than [kPa]
      - altitude greater than [km]
      - aerothermal flux smaller than [W/m²]

    - **Stop at stage**    
      Staging will not occur beyond this stage number

-   **Auto-deploy solar panels**    
    Automatically deploy solar panels when safe (verify! **TODO**).
    
-   **Auto-warp**    
    Automatically use warp during ascent

-   **Skip Circularization**    
    Do not circularize when apoapsis has been reached

## Ascent Profile

There are different ascent profiles available:

1.  **Classic Ascent Profile**  
    **TODO** Classic Ascent Profile should be more or less what you're used to.

2.  **Stock-style GravityTurn™**  
    This profile is similar to the gravity turn mod. It is a 3-burn to orbit
    style of launch that can get to orbit with about 2800 dV on stock Kerbin. If
    you want to have fun make a rocket that is basically a nose cone, a jumbo-64
    a mainsail and some fairly big fins. Have the pitch program flip it over
    aggressively (uncheck the AoA limiter, set the values to like 0.5 / 50 / 40 /
    45 / 1) and let it rip.  Note that its not precisely the GT mod algorithm,
    and I didn't intend it to be -- I just didn't know what else to call it. It
    does not do any pitch-up during the intermediate burn right now (that's
    another **TODO**) so it won't handle low TWR upper stages.

3.  **Powered Explicit Guidance (RSS/RO)**  
    PEG is actual gravity turn algorithms from the Surveyor missions that
    properly integrates the trajectory.  It will likely not be that useful on
    Kerbin since it does not know how to do two-burn to orbit (does not
    understand coast phases) so most Kerbin launch vehicles won't ever get
    locked guidance with it. So if you give a 100x100 orbit, then you need a
    rocket that will burn continuously, without throttling, until insertion
    where it will shut down right when it hits the 100x100 orbit.   Example of
    it flying a 3-stage rocket to orbit:  https://vimeo.com/224742550 


### Edit Ascent Path

Depending on the profile selection, the Edit Ascent Path button will open a new
window with options on the respective profile.

#### Classic Ascent Profile Options
The options show the diagram of the rocket's idealized ascent. The following
options can be edited, and the diagram will update as their values change:

-   **Turn Start Altitude**    
    The altitude at which the gravity turn should begin

-   **Turn End Altitude**    
    The altitude at which the gravity turn should end

-   **Final Flight Path Angle**    
    The final angle with respect to the prograde direction

-   **Turn Shape**    
    Varies the parabolic part of the trajectory
    
#### Stock-style GravityTurn

-   **Turn start altitude**
    Altitude in km to pitch over and initiate the Gravity Turn (higher values for lower-TWR rockets).

-   **Turn start velocity**
    Velocity in m/s which triggers pitch over and initiates the Gravity Turn (higher values for lower-TWR rockets).

-   **Turn start pitch**
    Pitch that the pitch program immediately applies.

-   **Intermediate altitude**
    Intermediate apoapsis altitude to coast to and then raise the apoapsis up to the eventual final target.  May be set to equal the final target in
    order to skip the intermediate phase.

-   **Hold AP Time**
    At the intermediate altitude with this much time-to-apoapsis left the engine will start burning prograde to lift the apoapsis.  The engine will throttle
    down in order to burn closer to the apoapsis.  This is very similar to the lead-time of a maneuver node in concept, but with throttling down in the case where
    the player has initiated the burn too early (the corollary is that if you see lots of throttling down at the start, you likely need less HoldAP time).

Initial Pitch Over Issues
-------------------------

The initial pitch over can cause rockets to tumble.  If the rocket gains too much speed and then attempts too large of a pitch maneuver the air resistance will
toss it end over end.  The solutions to this can be to initiate the turn sooner, use less of a pitch angle, put fins on your rocket to stabilize it, or turn on
or tune the AoA limiter.




#### Powered Explicit Guidance

More detailed information on the Surveyor-era PEG Ascent Guidance in MechJeb can be found [here](https://github.com/lamont-granquist/MechJeb2/wiki).


Target Options
--------------

Further options are available when a target is selected and the current ship is
landed

**TODO**
