# NTRRemoteDebugger
Tool to interface with NTR CFW's remote debugging.

# Features

* Nice GUI for searching for memory addresses and modifying them.
* No messing about with files or other external resources.
* Allows searching for byte, short, int, long, float, double, list of bytes, and text strings.
* Subsequent searches will narrow search results by only requesting the memory addresses needed, speeding up things significantly.
* Ability to "lock" memory addresses to a given value. This means that they are set automatically based on a configured value.
* Can convert AR3DS codes to work with the tool.
* Works on mono and via wine. Thanks to u/Melon__Bread for verifying mono support (and MonoDevelop compile support) and u/MattKimura for verifying wine support.

# Untested

* Search for float, double, and raw bytes.
 
# Notes

* Editing items in the bottom-right grid (the 'Values' grid) requires double clicking. This is strange behavior for the checkbox and dropdown cells.
* The first half of the progress bar at the bottom is for receiving memory values from the 3DS. The second half is for searching for the value.
* The progress bar does not work for narrowing search results, just be patient. It's about 1/3rd of a second per result check for me.
* NTR's performance seems to degrade over time if the connection gets interrupted at all. I find that after about 10 interrupted connections, I have to reboot my 3DS to get NTR's Debugger to start responding properly again. Apparently running [NTR 3.3 fork](https://github.com/Shadowtrance/BootNTR) from Shadowtrance with the 3.2 ntr.bin (same repo) is better for stability and also works on 10.6.
* Tested on 11.4 with BootNTR 3.5 loaded via [BootNTR Selector](https://gbatemp.net/threads/release-bootntr-selector.432911/): no stability issues found