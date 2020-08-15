// (c) Copyright HutongGames, LLC 2010-2011. All rights reserved.
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("FingerGestures")]
	[Tooltip("Drag object")]
	public class FingerGesturesDragObject : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The finger index to get info on.")]
		public FsmOwnerDefault objectToDrag;

        [UIHint( UIHint.Variable )]
        [Tooltip( "The finger index dragging the object" )]
        public FsmInt fingerIndex;

        public FsmBool centerObjectOnFinger;
        
		public override void Reset()
		{
            centerObjectOnFinger = false;
            objectToDrag = null;
            fingerIndex = null;
		}
        
        public override void OnUpdate()
        {
            if( fingerIndex.IsNone || objectToDrag.GameObject.IsNone || !objectToDrag.GameObject.Value )
                return;

            FingerGestures.Finger finger = FingerGestures.GetFinger( fingerIndex.Value );
            Transform tf = objectToDrag.GameObject.Value.transform; // transform of the gameobject to drag

            if( centerObjectOnFinger.Value )
            {
                tf.position = ProjectOnCameraPlane( finger.Position, tf.position );
            }
            else
            {
                Vector3 currentFingerPos3d = ProjectOnCameraPlane( finger.Position, tf.position );
                Vector3 previousFingerPos3d = ProjectOnCameraPlane( finger.PreviousPosition, tf.position );
                Vector3 move = currentFingerPos3d - previousFingerPos3d;

                // translate dragged object
                tf.position += move;
            }
        }

        Vector3 ProjectOnCameraPlane( Vector2 screenPos, Vector3 refPos )
        {
            Camera cam = Camera.main;
            Transform camtf = cam.transform;

            // create a plane passing through the dragged object and facing toward the camera
            Plane plane = new Plane( -camtf.forward, refPos );

            Ray ray = cam.ScreenPointToRay( screenPos );

            float t = 0;

            if( !plane.Raycast( ray, out t ) )
                return refPos;

            return ray.GetPoint( t );
        }
	}
}

