//--------------------------------------------------------------
// FingerGestures -- http://fingergestures.fatalfrog.com
// Copyright (c) Fatal Frog Software. All rights reserved
//--------------------------------------------------------------

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory( "FingerGestures" )]
    [Tooltip( "Detect custom point-cloud gesture events" )]
    public class FingerGesturesPointCloudEvent : FingerGesturesEvents
    {
        [ActionSection( "PointCloud Gesture" )]

        [Tooltip( "Event to send when a point-cloud gesture has been recognized" )]
        public FsmEvent matchEvent;

        [Tooltip( "Name of the point-cloud gesture to match" )]
        public FsmString gestureNameFilter;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the match score percent value" )]
        public FsmFloat storeMatchScore;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the match distance value" )]
        public FsmFloat storeMatchDistance;

        [UIHint( UIHint.Variable )]
        [Tooltip( "Variable in which to store the name of the matched PointCloud gesture template" )]
        public FsmString storeMatchedGestureName;

        public override void Reset()
        {
            base.Reset();

            matchEvent = null;
            storeMatchDistance = null;
            storeMatchScore = null;
        }

        protected override bool HandleGestureEvent( Gesture gesture )
        {
            PointCloudGesture pcg = gesture as PointCloudGesture;

            if( pcg == null )
                return false;

            Debug.Log( "Recognized PCG: " + pcg.RecognizedTemplate.name );

            storeMatchedGestureName.Value = pcg.RecognizedTemplate.name;
            
            if( !string.IsNullOrEmpty( gestureNameFilter.Value ) && pcg.RecognizedTemplate.name != gestureNameFilter.Value )
                return false;

            storeMatchDistance.Value = pcg.MatchDistance;
            storeMatchScore.Value = pcg.MatchScore;
            
            SendEvent( matchEvent );
            return true;
        }
    }
}