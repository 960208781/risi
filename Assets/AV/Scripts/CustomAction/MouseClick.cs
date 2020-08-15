// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Input)]
    [Tooltip("Sends Events based on mouse interactions with a Game Object: MouseOver, MouseDown, MouseUp, MouseOff. Use Ray Distance to set how close the camera must be to pick the object. Button id can be set as well: 0 for left click ( default), 1 for right click, 2 for middle click")]
    public class MouseClick : FsmStateAction
    {
        public FsmOwnerDefault GameObject;

        [Tooltip("Length of the ray to cast from the camera.")]
        public FsmFloat rayDistance;
        public FsmGameObject camera;
        public FsmEvent onClick;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        public override void Reset()
        {
            GameObject = null;
            rayDistance = 100f;
            everyFrame = true;
        }

        public override void OnEnter()
        {
            var cam = camera.Value.GetComponent<Camera>();
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Fsm.Event(onClick);
            }

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {

        }


    }
}
