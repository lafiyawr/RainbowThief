# Stolen Rainbow


# OVERVIEW

Stolen Rainbow is a demo game showcasing several of the Niantic Lightship ARDK features you can utilize in your own project: Semantic Segmentation, Occlusion, and VPS. 

In this mini game, a magical rainbow makes a rare appearance on Earth and is promptly stolen by an evil penguin. Doing so turns the world black and white. You have to find and battle the penguin to retrieve pieces of the rainbow and restore color to the world. Oh, and there's a talking star. 


You can view a [demo here](https://www.youtube.com/watch?v=LrLliFCZD9c&ab_channel=BadChickStudios).


# WALKTHROUGH

This will breakdown how the project is structured and how the main features are set up.

<table><tr><td><strong>NOTE:</strong> In order to ensure certain objects weren't affected by the color saturation post processing effect in my game, I had to use two cameras. Because of that, I found it easier to set this project up in the 3D URP template, but it is not required to use that template for your own project.  </td></tr></table>

## Project Structure

There are two scenes in this project. The main scene is called <strong>Rainbow</strong>, however there is another one called <strong>Start</strong>. That scene holds the start screen which gives simple instructions about where the game takes place. This way folks understand that it is a location based game that only works at a certain location.

In the <strong>Rainbow</strong> scene, most of the game's functionality is handled using a Unity Timeline. The <strong>timeline</strong> is attached to the <strong>TimelineManager</strong> GameObject which can be found nested under <strong>Managers</strong>. 

<img src="https://badchickstudios.com/stolenrainbow/managers.png" />

The timeline handles the dialogue text, intruction text, animation, music cues, location tracking triggers, and visibility of certain GameObjects.

<img src="https://badchickstudios.com/stolenrainbow/timeline.png" />

The rest of the GameObjects under <strong>Managers</strong> have scripts attached to them as well to handle specific functionality.

The <strong>MainCanvas</strong> is where you'll find all of the UI including the instructions, the talking star's text bubble, the star shooter button, and the rainbow counter that keeps track of how many pieces you've collected. 

<strong>Audio</strong> is where, you guessed it, you'll find some of the audio components, mainly the background music and various sound effects.

The other components under <strong>Managers</strong> and the <strong>Semantic Canvas</strong> will be explained in more detail throughout the walkthrough.

## VPS

<table><tr><td><strong>NOTE:</strong> In order to use certain Lightship features, including VPS, you will need an [API Key](https://lightship.dev/docs/ardk/setup/#adding-your-api-key-to-your-unity-project/)
which can be obtained through your [developer account](https://lightship.dev). </td></tr></table>

VPS is the magic behind location-based experiences. [Adding VPS to your project](https://lightship.dev/docs/ardk/how-to/vps/real_world_location_ar/) enables you to add content to a specific location with millimeter precision. 

In this project, after the first run-in with the penguin, you have to track him down at two subseqent locations. The 1st and 2nd time you beat him, he will drop a crudely drawn "map" of his hideout which is a clue for where you should go. 

In order to import locations into the project, you can either scan one using [Niantic's "Wayfarer" app](https://lightship.dev/docs/ardk/how-to/vps/install_and_use_niantic_wayfarer/), or you can use the [Geospatial Browser](https://lightship.dev/docs/ardk/how-to/vps/use_geospatial_browser/) to find an already scanned location near you.  

For this project, I would choose two locations that are relatively close to one another. This way you don't have to walk too far between encounters. 

Once you have your locations downloaded, you  can dragged the zipped file into the project's asset folder. The file will unzip automatically and you'll end up with two files: The location manifest and the location mesh. You will need both files per location for it to work correctly. 


[You can find more details about how to setup VPS here.](https://lightship.dev/docs/ardk/how-to/vps/real_world_location_ar/)

<h3>Testing the project with custom locations</h3>

1. On the <strong>XR Orgin</strong> GameObject, find the <strong>AR Location Manager</strong> component.
2. Click <kbd>Add AR Location</kbd>  and then drag or select one of your location manifests to the <strong>AR Location Manifest</strong> slot.
3. Go back to <strong>AR Location Manager</strong> on <strong>XR Orgin</strong>.  When you click on the <kbd>AR Location</kbd> dropdown, you should see a list of the locations I've already included as well as any you've imported. 

<img src="https://badchickstudios.com/stolenrainbow/arlocationmanager.png" />

<strong>Take notice of the order that the locations are in.</strong> To start tracking a specific location, you will need to refer to the location number. The location number will be its order on the list. For instance, "fitnesscourt" is the first location on the list, so it would be location "0". You will need these numbers shortly. 



<table><tr><td><strong>NOTE: </strong>The locations do not need to be listed in a certain order for you to call them at different times. For example, even though "fitnesscourt" is the first location on my list, I could choose to make it the 2nd location tracked in the game. </td></tr></table>


Once you've imported your locations into the <strong>AR Location Manager</strong>, you will see the locations nested under the <strong>XR Orgin</strong>. This is where you can see the actual mesh of your location. In the project there are two locations: <strong>fitnesscourt</strong> and <strong>mural</strong>. 

<img src="https://badchickstudios.com/stolenrainbow/locations.png" />

You can nest any content you'd like under each mesh and place it accordingly. In this project, I created prefabs with all of the necessary content to make it easier to add the content to new locations. The Prefabs can be found in<strong> Assets>Prefabs</strong>.


4. Add <strong>Location1Prefab</strong> as a child to the location you'd like to track first and position accordingly.
5. Add <strong>Location2Prefab</strong> as a child to the location you'd like to track second and position accordingly.

In order to track the correct location at the right time in the game, I have a script called <strong>VPSManager</strong> that is attached to the <strong>LocationManager</strong> GameObject. The function allows you to start tracking a specified location.  The timeline calls the function <strong> locationChanger</strong> at specific points in the game. 

<table><tr><td><strong>NOTE: </strong>In my project, I didn't want the locations to start tracking immediately, so I have "Auto Track" unchecked and I start the tracking later in the game. </td></tr></table>

On the <strong>LocationManager</strong> there are two timeline signals that will call the function and enable the tracking. You can change the number in each signal to match your desired location. 

6. On the signal called <strong>location1</strong> change the number to the number of your preferred 1st location.
7. On the signal called <strong>location2</strong> changed the number to the number of your preferred 2nd location.

<img src="https://badchickstudios.com/stolenrainbow/locationtracking.png" />

<table><tr><td><strong>NOTE:</strong> In order to track a new location, you first have to stop tracking the current location. </strong>The <strong>resetlocation</strong> signal calls a function called <strong>resetTracking</strong> that does that. It is called on the timeline before the new location is tracked.  </td></tr></table>

At this point you should be able to build and run to test out the project!


## Semantic Segmentation

In the game, you are prompted to tap the ground in a area that will be spacious enough for the rainbow to appear. This is handled through Semantic Segmentation. It enables the ability to detect different parts of your environment, like sky, ground, or water. In this case, when you tap on an area that is recognized as <strong>ground</strong>, the rainbow will appear in that spot a certain distance away from the camera. 

[You can find more details about setup here](https://lightship.dev/docs/ardk/how-to/ar/query_semantics_real_objects/), but here is where you can find the components in the project. 

Essentially, there are a few components needed to get Semantic Segmentation up and running. 

1. An <strong>AR Segmentation Manager</strong>  which needs to be added to the <strong>Main Camera</strong>. 
2. A <strong>UI Raw Image</strong>. In the project, it is named <strong>SemanticImage </strong> and can be found nested under the <strong>SemanticCanvas</strong>.
3. [A shader and material](https://lightship.dev/docs/ardk/how-to/ar/query_semantics_real_objects/). Both items will be  referenced in the script.
4. A script to handle the functionality. I used the [script from the tutorial as a base](https://lightship.dev/docs/ardk/how-to/ar/query_semantics_real_objects/). The modified script is called <strong>SemanticManager</strong> and it is attached to a GameObject with the same name. 

The main modification to the <strong>SemanticManager</strong> script is that it looks out for whatever you set as the <strong>Preferred Segmentation</strong>, which is a string that can be changed. It is currently set to <strong>ground</strong>, so when the ground is recognized, it will position the rainbow in front of the camera. 

<img src="https://badchickstudios.com/stolenrainbow/segmentation.png" />

## Occlusion
Occlusion helps the placement of your objects look more realistic within an environment. Depending on what's around you, your digital objects can look like they are partially or completely obstructed by any object that's in its way. I've also found that the <strong>Occlusion Manager</strong> helps keep objects anchored to a certain position, even if they aren't touching the ground. 

1. The <strong>AR Occlusion Manager</strong> needs to be added to the <strong>Main Camera</strong> In the <strong>AR Occlusion Manager</strong>, the <strong>Occusion Preference Mode</strong> is set to <strong>Prefer Environment Occlusion</strong>, but you can change that to whatever benefits your project best. <strong>Environment Depth Mode</strong> is only set to <strong>Medium</strong> because as of now Unity crashes in play mode when I set it to "Best".
2. You can also add the <strong>Lightship Occlusion Extension</strong> which helps give additional feature. I set <strong>Optimal Occlusion</strong> to <strong>Closest Occluder</strong> which works for whichever object is closest, but you can also have it focus on a specific object. 
<img src="https://badchickstudios.com/stolenrainbow/occlusion.png" />

## Debugging & Testing
I left some components in place that I used when testing the project. They may be helpful to you as you test. 
<table><tr><td><strong>NOTE:</strong> When testing the locations, if you don't see them appear, try looking around the game view. Sometimes the mesh is just out of sight above or below the camera.  </td></tr></table>

* In the project, I have the <strong>SemanticImage</strong> alpha set to 0. Otherwise, you will see a layer of color over the part of the environment that is being detected. For debugging purposes, you can set the alpha back to 255 to see a visual representation of what's being detected. There is also a text field called <strong>Debug Text</strong> that you can make visible in order to show what's currently being detected. 
* Because the project uses both the Semantic Manager as well as VPS, it is difficult to play through the entire game in the editor.

    -  If you want to test out the Semantic Segmentation functionality, you can play the first part of the game using the [Playback Mode](https://lightship.dev/docs/ardk/features/playback/). However, it won't enable you to continue with the game and test the two locations. You can also use Playback Mode to test locations if you've captured a recording of them. However, you can only play 1 video a time. 
    - In order to playthrough the whole game, I created a bool on the <strong>SemanticManager</strong> called <strong>Debug Segmentation</strong>. This simulates the ground tap needed, and moves the game forward automatically. 

        - Turn off Playback Mode by going to the menu, selecting <strong>Lightship>Settings</strong>, and unchecking <strong>enabled</strong> under <strong>Playback</strong>. This will enable the locations to be "tracked" in when playing in the editor.
        - Set the <strong>Debug Segmentation</strong> bool to <strong>true</strong>.
        - Enter play mode in the editor. 
        
    - If you'd like to just test the individual locations without playing through the whole game,you'll find 3 buttons and text nested under <strong>Main Canvas> Debug Location</strong>. 
    
        - Turn off Playback Mode by going to the menu, selecting <strong>Lightship>Settings</strong>, and unchecking <strong>enabled</strong> under <strong>Playback</strong>. This will enable the locations to be "tracked" in when playing in the editor.
        
        - Enable the  <strong>Debug Location</strong> GameObject. You will then see 3 buttons on the screen: <strong>Location 1</strong>, <strong>Location 2</strong>, and <strong>Reset</strong>. Above that you will see some text that says "Anchor Status".
        - Under the <strong>Debug Location</strong> GameObject, select the <strong>Track Location 1</strong> button, and in the <strong>inspector</strong> find the <strong>On Click</strong> section and change the number to the preferred 1st location. Do the same for <strong>Track Location 2</strong>.
        - Disable the <strong>TimelineManager</strong> GameObject. You may also have to disable the star which is located under <strong>MainCanvas>StarHolder</strong>.
        - On play, you can press either button to track a location. If you want to switch to a new location, hit <strong>Reset</strong> first before you select a new location.
