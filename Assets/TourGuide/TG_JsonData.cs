using UnityEngine;
using System.Collections;
using LitJson;
/// <summary>
/// 导览json数据
/// </summary>
public class TG_JsonData 
{
    #region 单例
    static TG_JsonData _TG_JsonData;
    public static TG_JsonData __int__()
    {
        if (_TG_JsonData==null)
        {
            _TG_JsonData = new TG_JsonData();
        }
        return _TG_JsonData;
    }

    #endregion
    /// <summary>
    /// 储存的原始json
    /// </summary>
    string _JsonData = "";
    public static TG_Data TGData;

    public void Initialization(string Json)
    {
        _JsonData = Json;
        ///处理json
        JsonData _Json = JsonMapper.ToObject(Json);
        TGData = new TG_Data();
        TGData.location.longitude = _Json["location"]["longitude"].ToString();
        TGData.location.latitude = _Json["location"]["latitude"].ToString();

        TGData.poiList = new c_poiList[_Json["poiList"].Count];
        for (int i = 0; i < _Json["poiList"].Count; i++)
        {
            c_poiList s_poiList = new c_poiList();
            s_poiList.name = _Json["poiList"][i]["name"].ToString();
            s_poiList.location = _Json["poiList"][i]["location"].ToString();
            s_poiList.type = _Json["poiList"][i]["type"].ToString();
            s_poiList.typecode = _Json["poiList"][i]["typecode"].ToString();
            TGData.poiList[i] = s_poiList;
        }
        
    }
	
}
/// <summary>
/// 导览数据
/// </summary>
public class TG_Data
{
    public c_poiList[] poiList;
    public c_location location = new c_location();
}
/// <summary>
/// 卡片列表
/// </summary>
public class c_poiList
{
    public string name = "";
    public string type = "";
    public string typecode = "";
    /// <summary>
    /// 卡牌gps
    /// </summary>
    public string location = "";


}
/// <summary>
/// 当前gps
/// </summary>
public class c_location
{
    /// <summary>
    /// 经度
    /// </summary>
    public string longitude = "";
    /// <summary>
    /// 纬度
    /// </summary>
    public string latitude = "";
}