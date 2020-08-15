//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect finger hover events" )]
    public class FingerGesturesHoverEvent : FingerGesturesEvents
    {
        [ActionSection( "Hover Events" )]

        [Tooltip( "Event to send when the finger enters the object" )]
        public FsmEvent fingerEnterEvent;

        [Tooltip( "Event to send when the finger exits the object" )]
        public FsmEvent fingerExitEvent;

        public override void Reset()
        {
            base.Reset();

            fingerEnterEvent = null;
            fingerExitEvent = null;
        }

        protected override bool HandleFingerEvent(FingerEvent e)
        {
            FingerHoverEvent hover = (FingerHoverEvent)e;

            if( hover == null )
                return false;

            if( hover.Phase == FingerHoverPhase.Enter )
            {
                SendEvent( fingerEnterEvent );
                return true;
            }
            
            if( hover.Phase == FingerHoverPhase.Exit )
            {
                SendEvent( fingerExitEvent );
                return true;
            }

            Debug.LogWarning( "Unhandled hover phase: " + hover.Phase );
            return false;
        }
    }
}