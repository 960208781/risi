using UnityEngine;
public class MarkerlessTracking : VoidAREventBehaviour, ITricking
{
    private int lastState = -1;
    private bool isActive = false;
    /// <summary>
    /// 跟踪状态变化反馈
    /// </summary>
    /// <param name="stateCode"></param>
    public void UpdateTracking(int stateCode)
    {
        if (stateCode == 1099)
        {
            Debug.LogError("server error");
        }
        else if (stateCode == 501)
        {
            Debug.LogError("key error");
        }
        else if (stateCode == 101)
        {
            Debug.LogError("use time limit error");
        }
        lastState = stateCode;
    }

    public int GetTrackingState() {
        return lastState;
    }

    /// <summary>
    /// 设置跟踪活动状态
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value) {
        isActive = value;
        DispatchEvent(value ? VoidAREvent.FIND : VoidAREvent.LOST);
    }

    public bool GetActive() {
        return isActive;
    }
}