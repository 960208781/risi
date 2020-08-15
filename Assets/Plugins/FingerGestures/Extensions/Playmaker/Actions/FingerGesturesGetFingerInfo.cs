// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("FingerGestures")]
	[Tooltip("Gets info on a finger specified by the finger index.")]
	public class FingerGesturesGetFingerInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The finger index to get info on.")]
		public FsmInt fingerIndex;

		[UIHint(UIHint.Variable)]
		public FsmVector2 getPosition;

		[UIHint(UIHint.Variable)]
		public FsmFloat getStartTime;

		[UIHint(UIHint.Variable)]
		public FsmVector2 getStartPosition;

		[UIHint(UIHint.Variable)]
		public FsmFloat getDistanceFromStart;

		[UIHint(UIHint.Variable)]
		public FsmVector2 getPreviousPosition;

		[UIHint(UIHint.Variable)]
        [Tooltip( "Variable in which to store the difference between previous and current finger position" )]
		public FsmVector2 getDeltaPosition;

        public bool everyFrame;

		public override void Reset()
		{
			fingerIndex = null;
			getPosition = null;
			getStartTime = null;
			getStartPosition = null;
			getDistanceFromStart = null;
			getPreviousPosition = null;
			getDeltaPosition = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetFingerInfo();

			if (!everyFrame)
				Finish();
		}

        public override void OnUpdate()
        {
            DoGetFingerInfo();
        }

		void DoGetFingerInfo()
		{
            FingerGestures.Finger finger = FingerGestures.GetFinger( fingerIndex.Value );

			getPosition.Value = finger.Position;
			getStartTime.Value = finger.StarTime;
			getStartPosition.Value = finger.StartPosition;
			getDistanceFromStart.Value = finger.DistanceFromStart;
			getPreviousPosition.Value = finger.PreviousPosition;
			getDeltaPosition.Value = finger.DeltaPosition;
            
		}
	}
}

