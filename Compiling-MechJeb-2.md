These notes confirmed to work with MonoDevelop on Linux and Xamarin Studio on Windows.

* Create a MonoDevelop C# Library in the MechJeb source folder. 
* Delete the default class provided and the provided AssemblyInfo.cs.
* Add all the .cs source files in the MechJeb directory, and all the sources in the alglib folder.
* Add the files in the Properties directory, and set the non-C# files to have Build Action "EmbeddedResource" and make sure that their Resource ID is set to "**MuMech**.Properties.xxxxxx" where xxxxxx is the name of the resource.
* Ensure the project targets .Net framework 3.5.
* Next, locate some files from KSP itself as references. They can be found at:

_Your KSP Directory_/KSP_Data/Managed

add

> Assembly-CSharp.dll
>
> Assembly-CSharp-firstpass.dll
>
> UnityEngine.dll


also add
> System.Data.Linq

from the standard packages

Hopefully you should now be able to build MechJeb2.dll ! place this in the KSP plugins directory and the mechjeb part in the appropriate folder, don't forget to add said part to your design!!

If you have trouble with the plugin while it is running, check for clues in the log at KSP_Data/output_log.txt