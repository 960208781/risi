//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect two-finger pinch gesture events" )]
    public class FingerGesturesPinchEvent : FingerGesturesEvents
    {
        [ActionSection( "Pinch Gesture" )]

        [Tooltip( "Event to send when a pinch gesture begins." )]
        public FsmEvent pinchStartedEvent;

        [Tooltip( "Event to send when the pinch delta changes." )]
        public FsmEvent pinchUpdatedEvent;

        [Tooltip( "Event to send when the pinch gesture ends." )]
        public FsmEvent pinchEndedEvent;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the last change/delta in the gap between the two pinching fingers" )]
        public FsmFloat storeDeltaDistance;
        
        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the current distance between the two pinching fingers" )]
        public FsmFloat storeTotalDistance;

        public override void Reset()
        {
            base.Reset();

            pinchStartedEvent = null;
            pinchUpdatedEvent = null;
            pinchEndedEvent = null;
            storeDeltaDistance = null;
            storeTotalDistance = null;
        }

        protected override bool HandleGestureEvent( Gesture gesture )
        {
            PinchGesture pinch = gesture as PinchGesture;

            if( pinch == null )
                return false;

            storeDeltaDistance.Value = pinch.Delta;
            storeTotalDistance.Value = pinch.Gap;

            switch( pinch.Phase )
            {
                case ContinuousGesturePhase.Started:
                    SendEvent( pinchStartedEvent );
                    break;

                case ContinuousGesturePhase.Updated:
                    SendEvent( pinchUpdatedEvent );
                    break;

                case ContinuousGesturePhase.Ended:
                    SendEvent( pinchEndedEvent );
                    break;
            }

            return true;
        }
    }
}