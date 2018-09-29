The _Translatron_ module controls the vessel's throttle/velocity.

* OFF: Switch off Translatron.
* KEEP OBT: Keep orbital velocity.
* KEEP VERT: Keep vertical velocity (climb/descent speed).
* KEEP SURF: Keep surface velocity.
* EXECUTE: Execute the selected action.
* PANIC!!!: Abort mission by seperating all but the last stage and activating landing autopilot.

Please note that KEEP VERT is only used for controlling vertical speed (that is, rate of climb) If you set a pitch of 80 degrees and vertical speed of 100, your vessel will climb at 100 m/s and acellerate since to it's HDG because it's leaning at a 80 degrees. _However_, if you use KEEP SURF, you total speed will be measure and not your climb/descent (vertical) speed.

Please keep in mind all values are integers and can contain a sign.