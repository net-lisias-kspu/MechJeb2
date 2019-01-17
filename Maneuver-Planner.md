This module can create various manuever nodes for you.

![plannerdd](https://user-images.githubusercontent.com/11995799/51346834-df98d180-1a9e-11e9-82ed-56220def479a.png)

## Advanced transfer to another planet

There are two modes for transfer to another planet. In _limited time_ mode, MechJeb picks the best time and burn in the time constraint you specify. In _Porkchop_ mode, a [Porkchop plot](https://en.wikipedia.org/wiki/Porkchop_plot) is drawn. You can either click anywhere on the diagram or click a _Lowest dV_ button. Departure time increases from left to right and transfer time increases from bottom to top. The cooler the colour (most blue) the lower dV is required.

![porkchop](https://user-images.githubusercontent.com/11995799/51347212-c3496480-1a9f-11e9-8978-421509b86e35.png)

## Bi-Impulsive transfer to target

This option is used to plan transfer to target in single sphere of influence. It is suitable for rendezvous with other vessels or moons. Contrary to the name, the transfer is often uni-impulsive. You can select when you want the manevuer to happen or select optimum time.

For the trip back, you can use "Return from a moon" mode.

## Change apsis

There are series of modes to change your: periapsis, apoapsis, inclination, longitude of ascending node, combination of those and few other.

## Curcularize

This mode creates a manevuer to match your apoapsis to periapsis. To match apoapsis to periapsis, select "at periapsis", to match periapsis to , select "at apoapsis". Theese are the most efficient, but it can also create node at specific height or after specific time.

## Rendezvous releated

The usual sequence of manevuers to rendezvous ship in the same sphere of influence is as folows. If your orbital planes are too far off, use "match planes with target" first. There are options to schedule the burn at the cheapest, nearest or specific ascending/descending node. Then you can use Hohmann transfer to get an encounter. When this burn is completed, you can use "match velocities with target" to slow down relative to target at the closest approach.

## Resonant orbit

Resonant orbit is useful for placing satellites to a constellation. This mode should be used starting from a orbit in the desired orbital plane. Important parameter to this mode is the desired orbital ratio, which is the ratio between period of your current orbit and the new orbit.

To deploy satellites, set the denominator to number of satellites you want to have in the constellation. Setting the nominator to one less than denominator is the most efficient, but not necessary the fastest. To successfully deploy all satellites, make sure the numbers are incommensurable.