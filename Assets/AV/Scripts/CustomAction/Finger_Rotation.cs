
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Finger")]
    public class Finger_Rotation : FsmStateAction
    {
        public FsmOwnerDefault gameObject;

        public FsmBool On = true;

        public FsmFloat sensitivity = 1.0f;
        public FsmGameObject camera;

        public iTweenFsmAction.AxisRestriction axis = iTweenFsmAction.AxisRestriction.none;

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }
            if (On.Value)
            {
                var move = go.GetComponent<TB_Rotation>();
                if (move == null) move = go.AddComponent<TB_Rotation>();

                if (!camera.IsNone)
                {
                    var cam = camera.Value.GetComponent<Camera>();
                    if (axis == iTweenFsmAction.AxisRestriction.y)
                    {
                        move.Axis = TB_Rotation.RotationAxis.ObjectY;
                    }
                    // move.ReferenceCamera = cam;
                }
                move.Sensitivity = sensitivity.Value;
            }
            else
            {
                var move = go.GetComponent<TB_Rotation>();
                MonoBehaviour.Destroy(move);
            }
            Finish();
        }

    }
}
