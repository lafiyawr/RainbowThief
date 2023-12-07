# StolenRainbowV2

This will be updated, but this is a quick overview of the project.

The project utilizes Sky segmentation, Occlusion (if I can get it  to look better), post processing,  and VPS. 

The project's animation and functionality is mainly triggered using the timeline. 

In this mini game, you touch the sky to add a rainbow. Shortly after, you see an evil penguin :) steal the rainbow from the sky which turns the world black and  white. 

You fight him 3 times, each time getting a piece of the rainbow back. After the 3rd time, the color is restored and you see the rainbow again. 

He hides out in two additional locations aside from where you first see him, which is where the VPS come into play. 

You can view a [demo here](https://youtu.be/89Uikzmx_j0)

# ISSUES/QUESTIONS
1- Still having trouble with Occlusion and Segmentation. It makes the rainbow flicker. I've seen a few folks in Discord have similar issues. Jesus is helping me troubleshoot via the discord at the moment. Hopefully we can find a solution.

2- Since this is mainly for developers, can I use private scans? They will be for a public park, but as of now they aren't public.

3- Should I leave some of my debugging tools in place and just explain them in the readme? I mention some below. I was going to remove them, but that might help folks better test in the editor. 

# PROJECT BASICS

The main scene is called "Rainbow 1" (Though I will probably change that soon).

Again, most of the functionality is controlled by the timeline which is on the "TimelineManager" gameobject. 
The semantic code to setup the sky segmentation and spawn the rainbow when you tap the sky is on "SemanticManager". The script is called "SkySpawn4". (It will be renamed).

The fade in/out post processing code is on "Global Volume". The script is called "BWFade".

If you'd like to test outdoors, you can add your own locations. You don't have to delete the current ones unless you want to. You will need two locations, and ideally ones that aren't too far from each other. 

Add the prefab "Location1Prefab" to your first location and position accordingly.

Add the prefab "Location2Prefab" to your second location and position accordingly.

In the timeline manager, you can change the array numbers of your preferred locations on the "LocationManager" track. 

Click on the first and last signal on that track, and in the inspector, change the "locationchanger" number to the number that corresponds to your locations. 

I've also been able to test the full experience in the editor by turning off the playback video and "triggering" sky tap with a bool. This enables me to test that the two locations are working correctly. 

You'll find the bool called "Testing" in the Semantic Manager script. When you get to the part of the game that says "Tap the sky", set the bool to true and the game will continue. 

If you just want to test each location seperately in the editor, I also have two buttons under "MainCanvas" called "Location1" and "Location2". To test, I've disabled the timeline, and just triggered the location meshes this way as well. This also shows that you can switch back and forth between locations. 


# TO-DO LIST

1. Add sound
2. Hide Debug text
3. Add additional instructions
4. Make the penguin move faster in subsequent rounds
5. Organize hierarchy and timeline
6. Clean up unused files
7. Add more comments to the scripts
8. Fix the particles not dissapearing with the rainbow shard
9. Spruce up the start and ending screens
10. Have the shooting stars destroy after a few seconds
11. Create a more comprehensive readme (and the tutorial)



