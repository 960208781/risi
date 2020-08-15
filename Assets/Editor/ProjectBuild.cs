using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

public class ProjectBuild
{
    //得到工程中所有场景名称
    static string[] SCENES = FindEnabledEditorScenes();
    static void BulidTarget(string name, string path) { }

    private const string androidPath = @"F:\unity包\java\Quduoduo";
    private const string iosPath = @"F:\unity包\ios\Quduoduo";


    [MenuItem("Build/EnableTest")]
    public static void EnableTest()
    {
        var define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        if (!define.Contains("test"))
        {
            define += ";test";
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, define);
        }

        define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS);
        if (!define.Contains("test"))
        {
            define += ";test";
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, define);
        }
        //if()

        //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "test");
    }

    [MenuItem("Build/BuildApk")]
    public static void BuildApk()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "test");
        GenericBuild(SCENES, Application.dataPath + "/../gj.apk", BuildTarget.Android, BuildOptions.None);
    }

    // [MenuItem("Build/BuildApkRun")]
    // public static void BuildApkRun()
    // {
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "test");
    //     GenericBuild(SCENES, Application.dataPath + "/../gj.apk", BuildTarget.Android, BuildOptions.AutoRunPlayer);
    // }

    // [MenuItem("Build/BuildApkScript")]
    // public static void BuildApkScript()
    // {
    //     PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "test");
    //     GenericBuild(SCENES, Application.dataPath + "/../gj.apk", BuildTarget.Android, BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.AutoRunPlayer);
    // }

    [MenuItem("Build/BuildAndroid")]
    public static void BuildGoogleProject()
    {
        string fileName = DateTime.Now.ToString("yyyyMMddHHmm");
        var target_dir = androidPath + "/" + fileName;

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "");

        if (!Directory.Exists(target_dir))
            Directory.CreateDirectory(target_dir);
        GenericBuild(SCENES, target_dir, BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer);

        Debug.LogError("Package Android Complete");
    }

    [MenuItem("Build/BuildAndroidDebug")]
    public static void BuildGoogleProjectDebug()
    {
        string fileName = DateTime.Now.ToString("yyyyMMddHHmm");
        var target_dir = androidPath + "/" + fileName + "_debug";

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "test");

        if (!Directory.Exists(target_dir))
            Directory.CreateDirectory(target_dir);
        GenericBuild(SCENES, target_dir, BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer | BuildOptions.AllowDebugging | BuildOptions.Development);
    }


    [MenuItem("Build/BuildIOS")]
    public static void BuildIOS()
    {
        string fileName = DateTime.Now.ToString("yyyyMMddHHmm");
        var target_dir = iosPath + "/" + fileName;
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "EVERYPLAY_IPHONE");

        if (!Directory.Exists(target_dir))
            Directory.CreateDirectory(target_dir);
        GenericBuild(SCENES, target_dir, BuildTarget.iOS, BuildOptions.None);
        string args = string.Format(@"a -k -r -s -m1 -ep1 -df  {0}\{1}.zip {0}\{1}", iosPath, fileName);
        System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo.FileName = @"D:\Program Files (x86)\WinRAR\Rar.exe";
        p.StartInfo.Arguments = args;
        p.Start();

        //p.WaitForExit();
        Debug.LogError("Package IOS Complete");
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
        string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);

        if (res.Length > 0)
        {
            throw new Exception("BuildPlayer failure: " + res);
        }
    }

}