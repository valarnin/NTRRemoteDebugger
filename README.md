# NTRRemoteDebugger
Tool to interface with NTR CFW's remote debugging.

# Features

* Nice GUI for searching for memory addresses and modifying them.
* No messing about with files or other external resources.
* Allows searching for byte, short, int, long, float, double, and list of bytes.
* Subsequent searches will narrow search results by only requesting the memory addresses needed, speeding up things significantly.
* Ability to "lock" memory addresses to a given value. This means that they are set automatically every 100ms or so.
* Can convert AR3DS codes to work with the tool.

# Untested

* Search for value types other than 1/2/4 byte. Who knows, 3DS could represent floats different or something.
* It should work with Mono and therefore work on Linux/Mac. My only Linux VM is command-line only and I'm not going to convert a gentoo hardened VM to be desktop capable.
 
# Notes

* Editing items in the bottom-right grid (the 'Values' grid) requires double clicking. This is strange behavior for the checkbox and dropdown cells.
* The first half of the progress bar at the bottom is for receiving memory values from the 3DS. The second half is for searching for the value.
* The progress bar does not work for narrowing search results, just be patient. It's about 1/3rd of a second per result check for me.
* NTR's performance seems to degrade over time if the connection gets interrupted at all. I find that after about 10 interrupted connections, I have to reboot my 3DS to get NTR's Debugger to start responding properly again.
