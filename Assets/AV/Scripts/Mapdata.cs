using System.Collections.Generic;

namespace Map
{
    public class Location
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double longitude { get; set; }
    }

    public class Biz_ext
    {
        /// <summary>
        /// 
        /// </summary>
        public string cost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lowest_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rating { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string star { get; set; }
    }

    public class Deep_info
    {
    }

    public class PoiList
    {
        /// <summary>
        /// 110105
        /// </summary>
        public string adcode { get; set; }
        /// <summary>
        /// 安苑路与惠新西街交叉口东北150米
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Biz_ext
        /// </summary>
        public Biz_ext biz_ext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string biz_type { get; set; }
        /// <summary>
        /// 北京市
        /// </summary>
        public string cityname { get; set; }
        /// <summary>
        /// Deep_info
        /// </summary>
        public Deep_info deep_info { get; set; }
        /// <summary>
        /// 17
        /// </summary>
        public string distance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// B0FFIIJIG3
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 116.417802,39.984548
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// C座一0一租售中心
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Photos
        /// </summary>
        public List<string> photos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 生活服务;中介机构;中介机构;
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 071600
        /// </summary>
        public string typecode { get; set; }
    }

    public class MapRoot
    {
        /// <summary>
        /// Location
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// PoiList
        /// </summary>
        public List<PoiList> poiList { get; set; }
    }

}