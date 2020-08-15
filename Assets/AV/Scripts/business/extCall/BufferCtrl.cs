using UnityEngine;
using System.Collections;
using System.IO;

public class BufferCtrl
{

    public static string imgDir
    {
        get
        {
            return Application.temporaryCachePath + "/Image";
        }
    }
    public static string modDir
    {
        get
        {
            string cacheUrl = Application.temporaryCachePath + "/Model";
            if (!Directory.Exists(cacheUrl))
            {
                Directory.CreateDirectory(cacheUrl);
            }
            return cacheUrl;
        }
    }

}
