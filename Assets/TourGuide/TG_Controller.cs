﻿using UnityEngine;
using System.Collections;

public class TG_Controller : MonoBehaviour
{
    /// <summary>
    /// 导览标签父物体
    /// </summary>
    public GameObject TG_Label_F;
    /// <summary>
    /// 导览标签  预制体
    /// </summary>
    public GameObject TG_Label;
    /// <summary>
    /// 设定距离圆心的距离
    /// </summary>
    public float LabelDistance=13f;
    /// <summary>
    /// 交叉检测范围
    /// </summary>
    public float DetectionRange = 3f;
    /// <summary>
    /// 交叉提升高度
    /// </summary>
    public float LiftingHeight = 1.2f;
    /// <summary>
    /// 缓存
    /// </summary>
    ArrayList TG_Labels = new ArrayList();

    #region 测试数据
    string str = @"{
  ""poiList"" : [
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B000A83IZ6"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街17号附近"",
      ""location"" : ""116.416952,39.984631"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;公共停车场|交通设施服务;停车场;路边停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""28"",
      ""name"" : ""停车场(惠新西街)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150904|150906"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_234778"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/538fe0c7a310b13376484c5c""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B000A8377L"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街18号"",
      ""location"" : ""116.417626,39.984524"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;公共停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""地下"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""34"",
      ""name"" : ""北京罗马花园停车场"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150904"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_221373"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13daa310fd36c3eb6d21""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13daa310fd36c3eb6d1f""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFG7TZVR"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街18号"",
      ""location"" : ""116.417508,39.984709"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;停车场相关"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""地面"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""35"",
      ""name"" : ""罗马花园地面停车场"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150900"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : [

      ],
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFGB9MCP"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街18号罗马花园"",
      ""location"" : ""116.417605,39.984276"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;路边停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""路边"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""39"",
      ""name"" : ""罗马花园停车场"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150906"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_234558"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/b94575d715b1ca47866df2c42599b35f""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/f48f26f48701395b63b61e11548afadf""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/d6e64f612ea5bd0a62c7a90e8d813d3f""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFG2O93Y"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街15号院内"",
      ""location"" : ""116.416870,39.984879"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""15321556871;15321556872"",
      ""type"" : ""餐饮服务;餐饮相关场所;餐饮相关"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : ""82.00"",
        ""meal_ordering"" : ""0"",
        ""rating"" : ""3.0""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""53"",
      ""name"" : ""爱物语蛋糕(惠新西街店)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050000"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : [

      ],
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFFY7OSR"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街甲17号底商"",
      ""location"" : ""116.416887,39.983941"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""010-57742988"",
      ""type"" : ""餐饮服务;中餐厅;中餐厅"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : ""肉夹膜,鸭血粉丝汤,传统肉夹馍,油泼扯面,羊肉泡馍,油波面,没有,秦镇米皮,炒面片,米皮,岐山臊子面,传统凉皮,羊肉烩扯面"",
      ""biz_ext"" : {
        ""cost"" : ""25.00"",
        ""meal_ordering"" : ""0"",
        ""rating"" : ""3.5""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""66"",
      ""name"" : ""三秦食客(惠新店)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050100"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_372140"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/b4f393eec866d1b9165b0f2f051a6927""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/ddfe4b40aed79bbdfa56ed6314c86679""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/c3e7605528d3ce0efd58c4f017a95440""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFGKX92D"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街与安苑路交口安苑路17号巧思大厦一层"",
      ""location"" : ""116.416885,39.983853"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""010-84837830;010-84830278"",
      ""type"" : ""餐饮服务;快餐厅;快餐厅"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : ""夏威夷披萨,美式红薯条,黑椒牛肉披萨"",
      ""biz_ext"" : {
        ""cost"" : ""48.00"",
        ""meal_ordering"" : ""0"",
        ""rating"" : ""3.5""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""75"",
      ""name"" : ""美闻比萨(惠新西街店)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050300"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_549634"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/a69ad4732d670462b4d156a82587290b""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/0d258dc8cca761406b39cb37f60e982f""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53b0a1fcffeb58298dc99af6736dcac2""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFFYGLCR"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街17号惠新西街小区"",
      ""location"" : ""116.416288,39.984441"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;公共停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""地面"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""79"",
      ""name"" : ""惠新西街小区停车场"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150904"",
      ""gridcode"" : ""5916738300"",
      ""event"" : [

      ],
      ""navi_poiid"" : [

      ],
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B000AA5E99"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街与安苑路交叉路口西北角"",
      ""location"" : ""116.416884,39.983792"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""010-64972868;15010102816"",
      ""type"" : ""餐饮服务;中餐厅;中餐厅"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : ""56.00"",
        ""meal_ordering"" : ""0"",
        ""rating"" : ""4.0""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""81"",
      ""name"" : ""德州扒鸡(惠新店)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050100"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_548920"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : ""菜"",
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/901a53392f9802ad0084e1ebdc840df7""
        },
        {
          ""title"" : ""其他"",
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/81101576b5d4167d3b28a768ee5127da""
        },
        {
          ""title"" : ""其他"",
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/f14dd8c042e1c2ad08e167947555386e""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFG61ZZU"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街安苑路甲17号巧思大厦2层"",
      ""location"" : ""116.416880,39.983755"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""010-64498862;13911236409"",
      ""type"" : ""餐饮服务;中餐厅;中餐厅|餐饮服务;休闲餐饮场所;休闲餐饮场所"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : ""香芒吉利虾,神仙饭,神仙鸡,黑椒牛仔骨,砂锅牛蛙,啤酒鸭,精品毛血旺,上汤娃娃菜,干炒牛河,菜谱"",
      ""biz_ext"" : {
        ""cost"" : ""68.00"",
        ""meal_ordering"" : ""0"",
        ""rating"" : ""4.6""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""85"",
      ""name"" : ""静花漾咖餐厅(安苑路店)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050100|050400"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_548918"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFGYNL99"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街安苑路17号巧思大厦2层(惠新西街北口地铁往南200米)"",
      ""location"" : ""116.416776,39.983695"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""010-64498862;18701231911"",
      ""type"" : ""餐饮服务;中餐厅;中餐厅"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : ""香芒吉利虾,神仙饭,台湾三杯鸡,黑椒牛仔骨,砂锅牛蛙,神仙鸡,啤酒鸭,金陵素鹅,上汤娃娃菜,精品毛血旺,干炒牛河,黑椒牛柳炒面,剁椒鱼头,烤乳鸽,昨日的忧伤,肉酱面,鲜蔬汁酱大根,浓缩咖啡,小炒湖南香干,小炒黄牛肉,奶酪鳕鱼,鱼恋上蛙,腊八豆烧多宝鱼,菜谱"",
      ""biz_ext"" : {
        ""cost"" : ""81.00"",
        ""meal_ordering"" : ""0"",
        ""rating"" : ""4.5""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""95"",
      ""name"" : ""宸悦·secret garden"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050100"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : [

      ],
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/1e5216401db2c5c1c0c7c86f1e5412a9""
        },
        {
          ""title"" : ""菜"",
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/ad08c941775aec0195b97ce2140ac1a1""
        },
        {
          ""title"" : ""菜"",
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/c45e2543f566eba5a4cf520c0dc4756f""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFGXZSE4"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""安苑路与惠新西街交叉口西50米"",
      ""location"" : ""116.416667,39.983583"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;公共停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""110"",
      ""name"" : ""停车场(巧思大厦西南)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150904"",
      ""gridcode"" : ""5916738300"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_548874"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFG7H110"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新东街与惠新北街交叉口西南150米"",
      ""location"" : ""116.418518,39.984638"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;公共停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""路边"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""111"",
      ""name"" : ""停车场(蓝珏苑北)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150904"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_284130"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13daa310fd36c3eb6d1a""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFGXZRXD"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新北街与惠新西街交叉口东南50米"",
      ""location"" : ""116.417517,39.985461"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;路边停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""地下"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""112"",
      ""name"" : ""停车场(惠新西街)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150906"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_548880"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : ""116.418564,39.983456"",
      ""id"" : ""B0FFFAHEGI"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街18号"",
      ""location"" : ""116.418523,39.984322"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""010-56245560"",
      ""type"" : ""餐饮服务;中餐厅;中餐厅"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""meal_ordering"" : ""0"",
        ""rating"" : ""3.1""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""112"",
      ""name"" : ""罗马阳光酒店餐厅"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050100"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_348763"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/3f9dc92362d328e9dc28ce7e10cbd653""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13daa310fd36c3eb6cfe""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13daa310fd36c3eb6cfa""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFGXZSYJ"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""安苑路与惠新西街交叉口西50米"",
      ""location"" : ""116.416662,39.983559"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;停车场出入口"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""113"",
      ""name"" : ""停车场(出入口)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150909"",
      ""gridcode"" : ""5916738300"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_548912"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : ""diner"",
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B000A87J2W"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街9号院-3-1"",
      ""location"" : ""116.416715,39.985459"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : ""010-64928544;18911210279;010-64949544"",
      ""type"" : ""餐饮服务;中餐厅;海鲜酒楼"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : ""蒜蓉粉丝蒸扇贝,鲅鱼饺子,蛏子,黑椒牛柳,海杂拌,剁椒天鹅蛋,蒜蓉生蚝,韭菜虾仁饺子,三文鱼刺身,小鸡炖蘑菇,螃蟹,酥皮蘑菇汤,龙须菜,三文鱼,带子,龙虾,炭烧鲍鱼,牡蛎汤,炭烧海螺,海鲜疙瘩汤,炸双盒,两吃基围虾,炭烧鱿鱼,香螺"",
      ""biz_ext"" : {
        ""cost"" : ""91.00"",
        ""meal_ordering"" : ""1"",
        ""rating"" : ""3.5""
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""117"",
      ""name"" : ""清香阁平价海鲜(惠新店)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""050119"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_219258"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/9b4ca72579d0890b24e48ecba3b55f12""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/e7caea50bfe1abcf23a0208eb1307200""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/978481d65886036ea42acd55ffc0224c""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : ""116.418219,39.985730"",
      ""id"" : ""B000A7I4QI"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街16号"",
      ""location"" : ""116.417925,39.985496"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;公共停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""地下"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""127"",
      ""name"" : ""蓝珏苑停车场"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150904"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_234554"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13d6a310fd36c3eb5edf""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13d6a310fd36c3eb5eda""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13d6a310fd36c3eb5edc""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFG7U28E"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街16号"",
      ""location"" : ""116.417922,39.985658"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;路边停车场"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""parking_type"" : ""其它"",
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""144"",
      ""name"" : ""蓝珏苑小区停车场"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150906"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : ""J50F001020_348794"",
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/b4af9efce2c3ee26ee37b02505800931""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13d6a310fd36c3eb5ec2""
        },
        {
          ""title"" : [

          ],
          ""url"" : ""http:\/\/store.is.autonavi.com\/showpic\/53da13d6a310fd36c3eb5ec0""
        }
      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    },
    {
      ""biz_type"" : [

      ],
      ""alias"" : [

      ],
      ""entr_location"" : [

      ],
      ""id"" : ""B0FFH14N48"",
      ""groupbuy_num"" : ""0"",
      ""address"" : ""惠新西街16号附近"",
      ""location"" : ""116.418212,39.985602"",
      ""match"" : ""0"",
      ""pname"" : ""北京市"",
      ""adname"" : ""朝阳区"",
      ""tel"" : [

      ],
      ""type"" : ""交通设施服务;停车场;停车场入口"",
      ""cityname"" : ""北京市"",
      ""website"" : [

      ],
      ""tag"" : [

      ],
      ""biz_ext"" : {
        ""cost"" : [

        ],
        ""rating"" : [

        ]
      },
      ""adcode"" : ""110105"",
      ""indoor_data"" : {
        ""floor"" : [

        ],
        ""cpid"" : [

        ],
        ""truefloor"" : [

        ]
      },
      ""email"" : [

      ],
      ""children"" : [

      ],
      ""distance"" : ""150"",
      ""name"" : ""蓝钰苑地下停车场(入口)"",
      ""pcode"" : ""110000"",
      ""recommend"" : ""0"",
      ""postcode"" : [

      ],
      ""exit_location"" : [

      ],
      ""typecode"" : ""150907"",
      ""gridcode"" : ""5916738301"",
      ""event"" : [

      ],
      ""navi_poiid"" : [

      ],
      ""citycode"" : ""010"",
      ""indoor_map"" : ""0"",
      ""discount_num"" : ""0"",
      ""photos"" : [

      ],
      ""shopid"" : [

      ],
      ""business_area"" : ""小关"",
      ""timestamp"" : [

      ]
    }
  ],
  ""location"" : {
    ""longitude"" : ""116.417224"",
    ""latitude"" : ""39.984480""
  }
}";
    #endregion

    // Use this for initialization
    void Start()
    {
        ///初始化数据
        RefreshLabel(str);
    }

    void OnGUI()
    {
        
        //if (GUI.Button(new Rect(0, 0, 100, 20), "ceshi"))
        //{
        //    RefreshLabel(str);
        //}
        //if (GUI.Button(new Rect(0, 20, 100, 20), "ceshi1"))
        //{
        //    RefreshLabel();
        //}

        //GUI.Label(new Rect(0, 220, 300, 30), "attitude ---->" + Input.gyro.attitude);
        //GUI.Label(new Rect(0, 260, 300, 30), "trueHeading ---->" + Input.compass.trueHeading);
        //GUI.Label(new Rect(0, 300, 300, 30), "magneticHeading ---->" + Input.compass.magneticHeading);

        

    }

    /// <summary>
    /// 刷新已有数据
    /// </summary>
    public void RefreshLabel()
    {
        //清除子物体
        for (int i = 0; i < TG_Label_F.transform.childCount; i++)
        {
            Destroy(TG_Label_F.transform.GetChild(i).gameObject);
        }
        //创建缓存
        TG_Labels = new ArrayList();

        //获取数据
        TG_Data TGData = TG_JsonData.TGData;
        //循环生成卡片
        for (int i = 0; i < TGData.poiList.Length; i++)
        {
            //分割获取 经纬度
            string[] locations = TGData.poiList[i].location.Split(new char[] { ',' }, System.StringSplitOptions.None);
            //克隆卡片
            GameObject Label = Instantiate(TG_Label) as GameObject;
            Label.SetActive(true);
            //设置父物体
            Label.transform.SetParent(TG_Label_F.transform);
            //获取直线距离 并 乘以缩放比例
            float longitude = (float.Parse(locations[0]) - float.Parse(TGData.location.longitude));
            float latitude = (float.Parse(locations[1]) - float.Parse(TGData.location.latitude));
            //获取与原点的距离
            float Dis = Vector3.Distance(TG_Label.transform.localPosition, new Vector3(latitude, 0f, longitude));
            //设置位置 （平滑）
            //Label.transform.localPosition = new Vector3(latitude * 10000, 0f, longitude * 10000);
            Label.transform.localPosition = new Vector3(LabelDistance / Dis * latitude, 0f, LabelDistance / Dis * longitude);
            //加入缓存
            TG_Labels.Add(Label);
        }
        OptimalDisplayLabel();
    }
    /// <summary>
    /// 更新json数据并刷新数据
    /// </summary>
    /// <param name="json"></param>
    public void RefreshLabel(string json)
    {
        TG_JsonData.__int__().Initialization(json);
        RefreshLabel();
    }
    /// <summary>
    /// 优化列表
    /// </summary>
    public void OptimalDisplayLabel()
    {
        for (int i = 0; i < TG_Labels.Count; i++)
        {
            
            Vector3 LabelsV = ((TG_Labels[i] as GameObject).transform.localPosition);
            for (int ii = 0; ii < TG_Labels.Count; ii++)
            {
                Vector3 LabelsV_ = ((TG_Labels[ii] as GameObject).transform.localPosition);
                if (LabelsV_.y == LabelsV.y && Vector3.Distance(LabelsV_, LabelsV) <= DetectionRange && i != ii)
                {
                    GameObject TG_Label = (TG_Labels[ii] as GameObject);
                    TG_Label.name = i.ToString();
                    TG_Label.transform.localPosition = new Vector3(TG_Label.transform.localPosition.x, TG_Label.transform.localPosition.y + LiftingHeight, TG_Label.transform.localPosition.z);
                    continue;
                }
               
            }
            
        }
    }
    
}

