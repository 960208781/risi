// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
// Thanks to James Murchison for the original version of this script.

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Custom")]
    public class GetMainVar : FsmStateAction
    {

        public FsmString showType = "";
        public FsmBool isTrack = false;
        public override void Reset()
        {

        }

        public override void OnEnter()
        {
            if (MainController.Ins)
            {
                showType.Value = MainController.Ins.showT.ToString();
                isTrack.Value = MainController.Ins.isTrack;
            }
            else
            {
                showType.Value = "type_free";
            }

            Finish();
        }
    }
}