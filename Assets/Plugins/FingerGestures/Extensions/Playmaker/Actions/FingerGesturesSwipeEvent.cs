//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect swipe gesture events" )]
    public class FingerGesturesSwipeEvent : FingerGesturesEvents
    {
        [ActionSection( "Swipe Gesture" )]

        [Tooltip( "Event to send when a swipe gesture is detected" )]
        public FsmEvent swipeEvent;

        public FsmEvent swipeLeftEvent;
        public FsmEvent swipeRightEvent;
        public FsmEvent swipeUpEvent;
        public FsmEvent swipeDownEvent;

        [UIHint( UIHint.Variable )]
        public FsmVector2 storeMoveVector;

        [UIHint( UIHint.Variable )]
        public FsmFloat storeVelocity;

        [UIHint( UIHint.Variable )]
        public FsmString storeDirection;

        public override void Reset()
        {
            base.Reset();

            //currentObjectFilter.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
            //currentObjectFilter.GameObject = null;

            swipeEvent = null;
            swipeLeftEvent = null;
            swipeRightEvent = null;
            swipeUpEvent = null;
            swipeDownEvent = null;

            storeDirection = null;
            storeMoveVector = null;
            storeVelocity = null;
        }

        protected override bool HandleGestureEvent( Gesture gesture )
        {
            SwipeGesture swipe = gesture as SwipeGesture;

            if( swipe == null )
                return false;
            
            storeDirection.Value = swipe.Direction.ToString();
            storeMoveVector.Value = swipe.Move;
            storeVelocity.Value = swipe.Velocity;

            SendEvent( swipeEvent );

            switch( swipe.Direction )
            {
                case FingerGestures.SwipeDirection.Up: SendEvent( swipeUpEvent ); break;
                case FingerGestures.SwipeDirection.Down: SendEvent( swipeDownEvent ); break;
                case FingerGestures.SwipeDirection.Right: SendEvent( swipeRightEvent ); break;
                case FingerGestures.SwipeDirection.Left: SendEvent( swipeLeftEvent ); break;
            }

            return true;
        }
    }
}