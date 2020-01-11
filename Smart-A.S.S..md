_Please help expanding this page. The Smart A.S.S. module is in need of more thorough documentation._

The _Smart A.S.S._ module provides aids for vessel pitch control. Please note that the Smart A.S.S autopilot calculates it's pitch, bank and roll according to the orbital flight path and will not follow the flight path marker on the artifical horizon but will instead calculate it's own flight path marker from the orbital speed. NOT the surface or current speed mode selected or shown on the artificial horizon.

# Main buttons

* OFF: Switch off Smart A.S.S.
* KILL ROT: "Kills" the spacecraft's rotation (counters rotation/tumbling).
* NODE: Orients the spacecraft to the current maneuver node

# Mode buttons

* OBT: Orbit mode
* SURF: Surface mode
* TGT: Target mode
* ADV: Advanced mode

## Orbit mode

* PRO GRAD: Orient to orbital prograde.
* RETR GRAD: Orient to orbital retrograde.
* NML +: Orient to orbital normal (change inclination).
* NML -: Orient to orbital anti-normal (change inclination).
* RAD +: Orient to radial outward (away from SOI).
* RAD -: Orient to radial inward (towards SOI).
* Force roll: Rotates to a specific angle. 

## Surface mode

* SVEL +: Orient in the direction of movement relative to the ground. Useful during lift-off for rockets which don't have fins or are otherwise instable.
* SVEL -: Orient in the opposite direction of movement relative to the ground. Useful during reentry or aerobraking with an aerodynamically unstable craft.
* HVEL +: Orient in the direction of horizontal movement relative to the ground.
* HVEL -: Orient in the opposite direction of horizontal movement relative to the ground.
* SURF: Orient the vessel in specific direction relative to surface.
* UP: Orient "up", perpendicular to the surface.

In the SURF mode, there are three tunables:

* HDG: Heading. Also called or azimuth, or the direction where you want to go.
* PIT: Pitch or inclination. 0 is horizontal and 90 is straight up. Can be negative.
* ROL: Roll. 0 is top side up.

[[Aircraft Autopilot]] is more suited to piloting aircrafts.

## Target mode

* TGT +: Orient towards the target.
* TGT -: Orient away from the target.
* RVEL +: Orient toward your relative velocity. Burning this direction will increase your relative velocity.
* RVEL -: Orient away from your relative velocity. Burning this direction will decrease your relative velocity.
* PAR +: Orient parallel to the target's orientation. If the target is a docking node it orients the ship along the docking axis, pointing away from the node.
* PAR -: Orient antiparallel to the target's orientation. If the target is a docking node it orients the ship along the docking axis, pointing toward the node.

## Advanced mode
* Reference:
  * Inertial
  * Orbit
  * Orbit_Horizontal
  * Surface_North
  * Target
  * Relative_Velocity
  * Target_Orientation
  * Maneuver_Node
  * Sun
  * Surface_Horizontal
* Direction:
  * Forward
  * Back
  * Up
  * Down
  * Right
  * Left
* Force Roll: Rolls the spacecraft to a specific angle.
* EXECUTE: Apply the selected orientation

Advanced Mode allows you to orient the spacecraft relative to a specific reference.  As an example, this can be useful for orienting a spacecraft to maximize energy harvested by the solar panels.  On a spacecraft where all of the solar panels are on the bottom, selecting SUN as the reference and UP as the direction will optimally position the spacecraft for energy harvesting.  For spacecraft with the solar arrays on the top of the spacecraft select DOWN as the orientation.  Clicking execute applies the orientation change.

Another use for this is in positioning surveillance and planetary data satellites.  Selecting ORBIT - DOWN - EXECUTE will keep the spacecraft pointed at the planet or moon.