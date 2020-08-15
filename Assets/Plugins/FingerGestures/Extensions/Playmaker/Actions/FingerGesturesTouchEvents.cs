//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect finger touch down/up events" )]
    public class FingerGesturesTouchEvents : FingerGesturesEvents
    {
        [ActionSection( "Touch Events" )]

        [Tooltip( "Event to send when a finger starts touching the screen" )]
        public FsmEvent fingerDownEvent;

        [Tooltip( "Event to send when a finger is released from the screen" )]
        public FsmEvent fingerUpEvent;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable to store the amount of time the finger has remaing pressed down before being released" )]
        public FsmFloat storeTimeHeldDown;

        public override void Reset()
        {
            base.Reset();

            fingerDownEvent = null;
            fingerUpEvent = null;
            storeTimeHeldDown = null;
        }

        protected override bool HandleFingerEvent(FingerEvent e)
        {
            if( e is FingerDownEvent )
            {
                SendEvent( fingerDownEvent );
                return true;
            }

            if( e is FingerUpEvent )
            {
                storeTimeHeldDown.Value = ( (FingerUpEvent)e ).TimeHeldDown;
                SendEvent( fingerUpEvent );
                return true;
            }

            return false;
        }
    }
}