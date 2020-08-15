//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect long-press gesture events" )]
    public class FingerGesturesLongPressEvent : FingerGesturesEvents
    {
        [ActionSection( "Long-Press Gesture" )]

        [Tooltip( "Event to send when a long-press gesture is detected" )]
        public FsmEvent longPressEvent;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable to store the elapsed press time into" )]
        public FsmFloat storeElapsedTime;

        public override void Reset()
        {
            base.Reset();

            longPressEvent = null;
            storeElapsedTime = null;
        }

        protected override bool HandleGestureEvent( Gesture gesture )
        {
            LongPressGesture longPress = gesture as LongPressGesture;

            if( longPress == null )
                return false;

            storeElapsedTime.Value = gesture.ElapsedTime;
            SendEvent( longPressEvent );
            return true;
        }
    }
}