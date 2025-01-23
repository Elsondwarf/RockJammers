Scripts that need audio
-----------------------
Scene Transion - on game object called GameManager in ScreenTransitionAnimation

Player Moving - on PlayerMovement in Propel() also on PlayerMainMenu in the same function

Collision - on PlayerMovement in
OnCollisionEnter()

Pickup bubbles - on Bubble in OnCollisionEnter to play and in OnCollisionExit to stop

menu sound hover over button - in PauseMenu in OnHover


Things in every scene
---------------------
GameManager

PlayerPrefab
 - Player Detect Border script neds camera controller refrence
 - Player Air Scripts needs slider and image from canvas -> Air Slider (this is the slider) -> Fill Area -> Fill (this image)

CameraController on the main camera
 - needs player transform

Event system

Exit Way
 - Connecting room is the scene ID
 - Door number needs to be the same number as the door in the next room, so you in room 1 that has door 1, it will link to a door on the other room, that door they end up at needs to be in that position in GameManager Exitway array for that room, so for the door in the next room its door number needs to be 1 as that is the position of door in the previous  GameManager Exitway array
If still having questions, ask me or look between test scene 1 and 2

Pause menu has got DoNotDestroyOnLoad as its not linked to anything else so there is only one but it needs to be in the first room after new game as its different from the main menu
 - However in PauseMenu script it needs the event system

Boarder
 - See below on details

How to make a boarder
---------------------
Step 1
 - Tag and layer needs to be boarder

Step 2
 - Number of points = the number of corners in the room
 - same with line renderer if you still want to use that

Step 3
 - put the cordinats for each point
 - I like to do it in order so I know where the wall is currently at

Step 4
 - DONE

Step 5
 - if the player can still go through the wall then copy the object from test scene 1 or 2 and then change the points
 - repeat steps from 1 to 4
 - I don't know why it didn't work, it happened a couple of times to me


Minor Updates
-------------
I have added it so that the player air reduces over time and when the player colliders with the bubble it will reduce in size and air gained - in playerAir
If you are not happy with how this plays, I have left the code for when it was a trigger you can switch it out to but I don't know how that will affect the particle



If there are any question then feel free to ask, I don't charge
If I don't respond though I have gone to sleep