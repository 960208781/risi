//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect drag gesture events" )]
    public class FingerGesturesDragEvent : FingerGesturesEvents
    {
        [ActionSection( "Drag Gesture" )]

        [Tooltip( "Event to send when a Drag gesture begins." )]
        public FsmEvent dragStartedEvent;

        [Tooltip( "Event to send when a finger is moving while performing a Drag gesture." )]
        public FsmEvent dragUpdatedEvent;

        [Tooltip( "Event to send when a Drag gesture ends." )]
        public FsmEvent dragEndedEvent;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the screen-space movement delta since last dragMove" )]
        public FsmVector2 storeDragDelta;
        
        public override void Reset()
        {
            base.Reset();

            dragStartedEvent = null;
            dragUpdatedEvent = null;
            dragEndedEvent = null;
            storeDragDelta = null;
        }
        
        protected override bool HandleGestureEvent(Gesture gesture)
        {
            DragGesture drag = gesture as DragGesture;

            if( drag == null )
                return false;

            storeDragDelta.Value = drag.DeltaMove;

            switch( drag.Phase )
            {
                case ContinuousGesturePhase.Started:
                    SendEvent( dragStartedEvent );
                    break;

                case ContinuousGesturePhase.Updated:
                    SendEvent( dragUpdatedEvent );
                    break;

                case ContinuousGesturePhase.Ended:
                    SendEvent( dragEndedEvent );
                    break;
            }

            return true;
        }
    }
}