// ***********************************************************
// Written by Heyworks Unity Studio http://unity.heyworks.com/
// ***********************************************************
using UnityEngine;
/// <summary>
/// Gyroscope controller that works with any device orientation.
/// </summary>
public class GyroUpdate : MonoBehaviour
{
    #region [Private fields]
    private bool gyroEnabled = true;
    private const float lowPassFilterFactor = 0.2f;

    //手机屏幕方向
    private readonly Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);


    private readonly Quaternion landscapeRight = Quaternion.Euler(0, 0, 90);
    private readonly Quaternion landscapeLeft = Quaternion.Euler(0, 0, -90);
    private readonly Quaternion upsideDown = Quaternion.Euler(0, 0, 180);



    //摄像机底座 原点
    private Quaternion cameraBase = Quaternion.identity;
    //校准
    private Quaternion calibration = Quaternion.identity;
    //基准定位
    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion baseOrientationRotationFix = Quaternion.identity;
    private Quaternion referanceRotation = Quaternion.identity;
    private float trueHeading;
    //是否debug
    private bool debug = true;
    public static float jingdu_ = 0f;
    //物体位置
    private Transform _t;
    #endregion
    #region [Unity events]
    //程序入口
    protected void Start()
    {
        //开启陀螺仪
        Input.compensateSensors = false;
        Input.location.Start();
        Input.compass.enabled = true;
        Input.gyro.enabled = true;



        //当前位置 赋值 _T
        _t = transform;

        //TODO 
        // if (SystemInfo.supportsGyroscope == false)
        //     return;

        AttachGyro();
    }
    protected void Update()
    {

#if UNITY_EDITOR
        //if (Input.GetKey(KeyCode.LeftAlt))
        //{


        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var xq = transform.transform.rotation.eulerAngles.x + y * 10f;
        var yq = transform.transform.rotation.eulerAngles.y + x * 10f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xq, yq, transform.rotation.eulerAngles.z), Time.deltaTime * 5);
        //}
        return;
#endif
        //如果陀螺仪没开启 返回
        if (!gyroEnabled)
            return;
        if (Input.compass.magneticHeading != 0f && CallFromJava.jingdu == 0)
        {
            CallFromJava.jingdu = Input.compass.trueHeading;
        }


#if UNITY_ANDROID
        Quaternion qt = ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix();
        _t.rotation = cameraBase * (Quaternion.Slerp(_t.rotation, cameraBase * (qt), lowPassFilterFactor));
#elif UNITY_IPHONE   
        Quaternion Gyro = Input.gyro.attitude;
        Gyro.x *= -1.0f;
        Gyro.y *= -1.0f;
        Vector3 gyro = (Quaternion.Euler(90, 0, 0) * Gyro).eulerAngles;
        //float jingdu = float.Parse(CallIos.cccc(""));
        //_t.localRotation = Quaternion.Euler(new Vector3(gyro.x, gyro.y - CallFromJava.jingdu, gyro.z));
        _t.localRotation = Quaternion.Euler(new Vector3(gyro.x, gyro.y + CallFromJava.jingdu, gyro.z));
#endif

    }

    /*
    protected void OnGUI()
    {
        if (!debug)
            return;
        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("Calibration: " + calibration);
        GUILayout.Label("transform.olj: " + transform.localRotation.eulerAngles.x);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("transform.rotation: " + transform.rotation);
        if (GUILayout.Button("On/off gyro: " + Input.gyro.enabled, GUILayout.Height(100)))
        {
            Input.gyro.enabled = !Input.gyro.enabled;
        }
        if (GUILayout.Button("On/off gyro controller: " + gyroEnabled, GUILayout.Height(100)))
        {
            if (gyroEnabled)
            {
                DetachGyro();
            }
            else
            {
                AttachGyro();
            }
        }
        if (GUILayout.Button("Update gyro calibration (Horizontal only)", GUILayout.Height(80)))
        {
            UpdateCalibration(true);
        }
        if (GUILayout.Button("Update camera base rotation (Horizontal only)", GUILayout.Height(80)))
        {
            UpdateCameraBaseRotation(true);
        }
        if (GUILayout.Button("Reset base orientation", GUILayout.Height(80)))
        {
            ResetBaseOrientation();
        }
        if (GUILayout.Button("Reset camera rotation", GUILayout.Height(80)))
        {
            transform.rotation = Quaternion.identity;
        }
    }
     * */
    #endregion
    #region [Public methods]
    /// <summary>
    /// Attaches gyro controller to the transform.
    /// </summary>
    private void AttachGyro()
    {
        //记录陀螺仪开启
        gyroEnabled = true;
        ResetBaseOrientation();
        UpdateCalibration(true);
        //刷新
        UpdateCameraBaseRotation(true);
        //计算定位
        RecalculateReferenceRotation();
        trueHeading = Input.compass.trueHeading;

    }
    /// <summary>
    /// Detaches gyro controller from the transform
    /// </summary>
    private void DetachGyro()
    {
        gyroEnabled = false;
    }
    #endregion
    #region [Private methods]
    /// <summary>
    /// Update the gyro calibration.
    /// </summary>
    private void UpdateCalibration(bool onlyHorizontal)
    {
        if (onlyHorizontal)
        {
            var fw = (Input.gyro.attitude) * (-Vector3.forward);
            fw.z = 0;
            if (fw == Vector3.zero)
            {
                calibration = Quaternion.identity;
            }
            else
            {
                calibration = (Quaternion.FromToRotation(baseOrientationRotationFix * Vector3.up, fw));
            }
        }
        else
        {
            calibration = Input.gyro.attitude;
        }
    }
    /// <summary>
    /// Update the camera base rotation.
    /// </summary>
    /// <param name='onlyHorizontal'>
    /// Only y rotation.
    /// </param>
    private void UpdateCameraBaseRotation(bool onlyHorizontal)
    {
        if (onlyHorizontal)
        {
            var fw = transform.forward;
            fw.y = 0;
            if (fw == Vector3.zero)
            {
                cameraBase = Quaternion.identity;
            }
            else
            {
                cameraBase = Quaternion.FromToRotation(Vector3.forward, fw);
            }
        }
        else
        {
            cameraBase = transform.rotation;
        }
    }
    /// <summary>
    /// Converts the rotation from right handed to left handed.
    /// </summary>
    /// <returns>
    /// The result rotation.
    /// </returns>
    /// <param name='q'>
    /// The rotation to convert.
    /// </param>
    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    /// <summary>
    /// Gets the rot fix for different orientations.
    /// </summary>
    /// <returns>
    /// 根据屏幕翻转 给出起始角度
    /// </returns>
    private Quaternion GetRotFix()
    {
#if UNITY_3_5
        if (Screen.orientation == ScreenOrientation.Portrait)
            return Quaternion.identity;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.Landscape)
            return landscapeLeft;     
        if (Screen.orientation == ScreenOrientation.LandscapeRight)
            return landscapeRight;
        if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
            return upsideDown;
        return Quaternion.identity;
#else
        return Quaternion.identity;
#endif
    }
    /// <summary>
    /// Recalculates reference system.
    /// </summary>
    private void ResetBaseOrientation()
    {
        /// 根据屏幕翻转 给出起始角度
        baseOrientationRotationFix = GetRotFix();

        baseOrientation = baseOrientationRotationFix * baseIdentity;
    }
    /// <summary>
    /// Recalculates reference rotation.
    /// </summary>
    private void RecalculateReferenceRotation()
    {
        //计算基准定位
        referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
    }
    #endregion
}