//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

FingerGestures Playmaker Integration
Version: 0.6
Date: March 3rd 2013
Requires: FingerGestures v3.1.b1+

============================
 How to use
============================
** Scene Setup **
- Make sure you have a FingerGestures component in your scene
- Add to your scene the GestureRecognizers and/or FingerDetector components that you will require
- Don't forget to put a ScreenRaycaster component on the gestures/detectors that need to interact with scene objects
- Add a PlayMaker FSM to an empty or existing object in your scene, depending on your needs

** FSM Setup **
- In your FSM, add one of the FingerGesturesXXXEvent actions. These allow you to detect gestures or finger events happening in your scene.
- Pay attention to the Start & Current Object Filter fields as they control the firing of the event based on what object is being interacted with.
- Use FingerGesturesGetFingerInfo to retrieve various per-finger data for the specified finger (by index)

==============================
SAMPLES
==============================
** PlayerMaker Sample 1 **
Demonstrates some of the FingerGestures actions for tap, swipe, long press, hover, and custom point cloud gesture recognition.

** PlayerMaker Sample 2 **
Demonstrates how to setup a PlayMaker FSM to manage the dragging of objects around the scene.
Makes use of the FingerGesturesDragEvent action to detect drag gesture events and FingerGesturesDragObject action to move the dragged objects.

============================
 CHANGE LOG
============================
Version 0.6 (June 1st 2013)
- Fixed swipe velocity and move vector variables not being set properly
- Fixed long press storeElapsedTime variable value not being set properly
- Added "Sample 4" showing different ways to detect a tap (globally or per-object)

Version 0.5 (March 3rd 2013)
- Rewrote PlayMaker actions to support new FingerGestures v3.0 release.
- The old FingerGesturesDetectInput action has been split into separate actions (one for each gesture recognizer/finger event detector).
  NOTE: it's still missing an action for the FingerMotionDetector (finger move/stationary event). This will be added in the next version.
- Added more filtering options and data store variables

Version 0.4 (August 13th 2012)
- Added FingerGestureDetectInput.swipeEvent for general swipe event notification
- Added FingerGestureDetectInput.storeSwipeMove to get swipe gesture motion
- Added FingerGestureDetectInput.storeSwipeDirectionName to retrieve approximate swipe gesture direction
- Added FingerGesturesGetFingerInfo.getGameObject property to store the current top-most object under the finger
- Changed FingerGesturesDetectInput.storeDragDelta type from FsmVector3 to FsmVector2
- Changed FingerGesturesGetFingerInfo.getPosition type from FsmVector3 to FsmVector2
- Changed FingerGesturesGetFingerInfo.getStartPosition type from FsmVector3 to FsmVector2
- Changed FingerGesturesGetFingerInfo.getPreviousPosition type from FsmVector3 to FsmVector2
- Changed FingerGesturesGetFingerInfo.getDeltaPosition type from FsmVector3 to FsmVector2
- Fixed FingerGestureDetectInput.storeSwipeVelocity not being assigned a value (thanks 
- Fixed FingerGesturesGetFingerInfo not firing on each update when the "everyFrame" flag is set
- Fixed Rotation & Pinch gesture not using the ignoreLayerMask property when picking object
- Removed FingerGestureActionHelpers (replaced raycast cache by FingerGestures.PickObject)

Version 0.3 (25th April 2012)
- Made the FingerGesturesDetectionInput stateless when it comes to handling continuous gestures such as drag, pinch and rotation.
- Added "Must Hit Object" property to the FingerGesturesDetectionInput action to require a valid object to be present under the finger(s) for events to fire
- Added support for dragging objects via the FingerGestureDragObject action. See "Playmaker Drag" sample scene in the Samples folder.
- Added "Playmaker Tap" sample in the Samples folder

Version 0.2
- Added Ignore Layer property to FingerGesturesDetectInput action to ignore colliders in specific layers when raycast for objects under the finger
- Removed left-over raycast in FingerGesturesActionHelpers.PickObject()

Version 0.1
- Initial release

