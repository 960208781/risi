//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect two-finger twist gesture events" )]
    public class FingerGesturesTwistEvent : FingerGesturesEvents
    {
        [ActionSection( "Twist Gesture" )]

        [Tooltip( "Event to send when a twist gesture begins." )]
        public FsmEvent twistStartedEvent;

        [Tooltip( "Event to send when the twist rotation changes." )]
        public FsmEvent twistUpdatedEvent;

        [Tooltip( "Event to send when the twist gesture ends." )]
        public FsmEvent twistEndedEvent;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the last change/delta in the twist rotation angle" )]
        public FsmFloat storeDeltaRotation;
        
        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the current total twist rotation angle" )]
        public FsmFloat storeTotalRotation;

        public override void Reset()
        {
            base.Reset();

            twistStartedEvent = null;
            twistUpdatedEvent = null;
            twistEndedEvent = null;
            storeDeltaRotation = null;
            storeTotalRotation = null;
        }

        protected override bool HandleGestureEvent( Gesture gesture )
        {
            TwistGesture twist = gesture as TwistGesture;

            if( twist == null )
                return false;

            storeDeltaRotation.Value = twist.DeltaRotation;
            storeTotalRotation.Value = twist.TotalRotation;

            switch( twist.Phase )
            {
                case ContinuousGesturePhase.Started:
                    SendEvent( twistStartedEvent );
                    break;

                case ContinuousGesturePhase.Updated:
                    SendEvent( twistUpdatedEvent );
                    break;

                case ContinuousGesturePhase.Ended:
                    SendEvent( twistEndedEvent );
                    break;
            }

            return true;
        }
    }
}