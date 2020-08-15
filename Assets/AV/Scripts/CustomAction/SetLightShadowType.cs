// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Lights)]
    [Tooltip("Sets the strength of the shadows cast by a Light.")]
    public class SetLightShadowType : ComponentAction<Light>
    {
        [RequiredField]
        [CheckForComponent(typeof(Light))]
        public FsmOwnerDefault gameObject;
        public FsmInt shadowType;
        public FsmFloat strength=0.35f;
        public FsmFloat bias = 0.3f;

        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            shadowType = 0;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoSetShadowStrength();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            DoSetShadowStrength();
        }

        void DoSetShadowStrength()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                light.shadows = shadowType.Value == 0 ? LightShadows.None : shadowType.Value == 1 ? LightShadows.Hard : LightShadows.Soft;
                if (shadowType.Value > 1)
                {
                    light.shadowBias = bias.Value;
                }
            }
        }
    }
}