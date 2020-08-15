// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Finger")]
    public class Finger_Move : FsmStateAction
    {
        public FsmOwnerDefault gameObject;
        public FsmGameObject camera;

        public FsmGameObject selectGame;
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
                var move = go.GetComponent<TBMove>();
                if (move == null) move = go.AddComponent<TBMove>();
                if (!camera.IsNone)
                {
                    var cam = camera.Value.GetComponent<Camera>();
                    move.RaycastCamera = cam;
                }
                move.dragObj = selectGame.Value;
            }
            else
            {
                var move = go.GetComponent<TBMove>();
                MonoBehaviour.Destroy(move);
            }
            Finish();
        }

    }
}