// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Finger")]
    public class Finger_Scale : FsmStateAction
    {
        public FsmOwnerDefault gameObject;

        public FsmFloat minScaleAmount = 0.5f;
        public FsmFloat maxScaleAmount = 2.0f;

        public FsmBool On = true;
        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            if (On.Value)
            {
                var move = go.GetComponent<TBPinchToScale>();
                if (move == null) move = go.AddComponent<TBPinchToScale>();
                move.minScaleAmount = minScaleAmount.Value;
                move.maxScaleAmount = maxScaleAmount.Value;
            }
            else
            {
                var move = go.GetComponent<TBPinchToScale>();
                MonoBehaviour.Destroy(move);
            }

            Finish();
        }

    }
}