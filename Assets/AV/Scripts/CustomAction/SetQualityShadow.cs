// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"package bundle":["Assets/PlayMaker/Ecosystem/Custom Packages/QualitySettings.unitypackage"]
}
EcoMetaEnd
---*/

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("QualitySettings")]
    [Tooltip("Sets a new graphics quality level.")]
    public class SetQualityShadow : FsmStateAction
    {

        public FsmFloat shadowDistance = 150;
        public FsmBool stableFit = false;
        public override void Reset()
        {
            shadowDistance.Value = 150;
            stableFit = new FsmBool(false);
        }

        public override void OnEnter()
        {
            UnityEngine.QualitySettings.shadowDistance = shadowDistance.Value;
            UnityEngine.QualitySettings.shadowProjection = stableFit.Value ? UnityEngine.ShadowProjection.StableFit : UnityEngine.ShadowProjection.CloseFit;

            Finish();
        }
    }
}
