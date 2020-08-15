using UnityEngine;
using System.Collections;
using LitJson;
public class N_JsonData 
{

    #region 单例
    static N_JsonData _N_JsonData;
    public static N_JsonData __int__()
    {
        if (_N_JsonData == null)
        {
            _N_JsonData = new N_JsonData();
        }
        return _N_JsonData;
    }
    #endregion
    /// <summary>
    /// 储存的原始json
    /// </summary>
    string _JsonData = "";
    public static N_Data NData;
    /// <summary>
    /// 初始化json
    /// </summary>
    /// <param name="Json"></param>
    public void Initialization(string Json)
    {
        _JsonData = Json;
        //处理json
        JsonData _Json = JsonMapper.ToObject(Json);
        NData = new N_Data();
        NData.destination.latitude = _Json["destination"]["latitude"].ToString();
        NData.destination.longitude = _Json["destination"]["longitude"].ToString();
        NData.origin.latitude = _Json["origin"]["latitude"].ToString();
        NData.origin.longitude = _Json["origin"]["longitude"].ToString();

        NData.steps = new c_step[_Json["steps"].Count];

        for (int i = 0; i < _Json["steps"].Count; i++)
        {
            NData.steps[i] = new c_step();
            NData.steps[i].distance = int.Parse(_Json["steps"][i]["distance"].ToString());
            NData.steps[i].polylines = new c_polyline[_Json["steps"][i]["polyline"].Count];
            for (int ii = 0; ii < _Json["steps"][i]["polyline"].Count; ii++)
			{
                NData.steps[i].polylines[ii] = new c_polyline();
                NData.steps[i].polylines[ii].latitude = _Json["steps"][i]["polyline"][ii]["latitude"].ToString();
                NData.steps[i].polylines[ii].longitude = _Json["steps"][i]["polyline"][ii]["longitude"].ToString();
			}
        }
    }

    /// <summary>
    /// 导航数据
    /// </summary>
    public class N_Data
    {
        public c_destination destination = new c_destination();
        public c_origin origin = new c_origin();
        public c_step[] steps;
    }
    /// <summary>
    /// 终点
    /// </summary>
    public class c_destination
    {
        public string latitude;
        public string longitude;
    }
    /// <summary>
    /// 起点
    /// </summary>
    public class c_origin
    {
        public string latitude;
        public string longitude;
    }
    /// <summary>
    /// 路径
    /// </summary>
    public class c_step
    {
        public int distance;
        public c_polyline[] polylines;
    }
    /// <summary>
    /// 路段
    /// </summary>
    public class c_polyline
    {
        public string latitude;
        public string longitude;
    }
}
