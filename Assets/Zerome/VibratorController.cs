using System.Runtime.InteropServices;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    //[DllImport("__Internal")]
    //private static extern void TriggerVibration(int milliseconds);

    //public void Vibrate(int duration)
    //{
        //    #if UNITY_WEBGL && !UNITY_EDITOR
        //    try
        //    {
        //        TriggerVibration(duration); // JavaScript 호출
        //    }
        //    catch (System.Exception e)
        //    {
        //        Debug.LogError($"Vibration failed: {e.Message}");
        //    }
        //    #else
        //    Debug.Log($"Vibration triggered for {duration}ms (WebGL only).");
        //    #endif
    //}

    //public void ConditionalVibrate(bool condition, int duration)
    //{
        //    if (condition)
        //    {
        //        Vibrate(duration);
        //    }
        //    else
        //    {
        //        Debug.Log("Condition not met, no vibration triggered.");
        //    }
    //}
}
