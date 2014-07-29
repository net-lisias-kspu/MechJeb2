### NB this is a stub - please embellish especially if after compiling you solve any issues you have you think others may encounter... 

These are just some quick notes from my experiences compiling MechJeb in Linux using MonoDevelop

Create a MonoDevelop C# Library in the MechJeb source folder, delete the default class provided and add all the .cs source files in that directory, add ALL the files inside the properties and all the sources in the alglib folder. Its important you check that the only AssemblyInfo.cs file in your project is the one in the properties folder.  Ensure the project targets .Net framework 3.5.

next you need to locate some files from KSP itself as references, in Linux these will usually be in:

~/.local/share/Steam/SteamApps/common/Kerbal Space Program/KSP_Data/Managed

where ~ is your home directory (note .local is a "hidden" folder and you will have to select show hidden in the file browser (right click))

add

> Assembly-CSharp.dll
>
> Assembly-CSharp-firstpass.dll
>
> UnityEngine.dll


also add
> System.Data.Linq

from the standard packages

Make sure the files in Properties/ are included and have Build Action set to "EmbeddedResource" and that their Resource ID is set to "MuMech.Properties.<filename>"

Hopefully you should now be able to build MechJeb2.dll ! place this in the KSP plugins directory and the mechjeb part in the appropriate folder, don't forget to add said part to your design!!

If you have trouble with the plugin while it is running, check for clues in the log at KSP_Data/output_log.txt