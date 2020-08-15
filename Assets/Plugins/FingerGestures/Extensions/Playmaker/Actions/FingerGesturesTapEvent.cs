//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect tap gesture events" )]
    public class FingerGesturesTapEvent : FingerGesturesEvents
    {
        [ActionSection( "Tap Gesture" )]

        [Tooltip( "Event to send when a single-finger tap gesture is detected" )]
        public FsmEvent tapEvent;

        public override void Reset()
        {
            base.Reset();

            tapEvent = null;
        }

        protected override bool HandleGestureEvent( Gesture gesture )
        {
            TapGesture tap = gesture as TapGesture;

            if( tap == null )
                return false;

            SendEvent( tapEvent );
            return true;
        }
    }
}