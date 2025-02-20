using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using YouYouMain;

public class YouYouMenuExt
{
    #region AssetBundleOpenPersistentDataPath 打开persistentDataPath
    [MenuItem("YouYouTools/打开persistentDataPath")]
    public static void AssetBundleOpenPersistentDataPath()
    {
        string output = MainConstDefine.LocalAssetBundlePath;
        if (!Directory.Exists(output))
        {
            Directory.CreateDirectory(output);
        }
        output = output.Replace("/", "\\");
        System.Diagnostics.Process.Start("explorer.exe", output);
    }
    #endregion

    #region SetFBXAnimationMode 设置文件动画循环为true
    [MenuItem("YouYouTools/设置文件动画循环为true")]
    public static void SetFBXAnimationMode()
    {
        Object[] objs = Selection.objects;
        for (int i = 0; i < objs.Length; i++)
        {
            string relatepath = AssetDatabase.GetAssetPath(objs[i]);

            if (relatepath.IsSuffix(".FBX", System.StringComparison.CurrentCultureIgnoreCase))
            {
                string path = Application.dataPath.Replace("Assets", "") + relatepath + ".meta";
                path = path.Replace("\\", "/");
                StreamReader fs = new StreamReader(path);
                List<string> ret = new List<string>();
                string line;
                while ((line = fs.ReadLine()) != null)
                {
                    line = line.Replace("\n", "");
                    if (line.IndexOf("loopTime: 0") != -1)
                    {
                        line = "      loopTime: 1";
                    }
                    ret.Add(line);
                }
                fs.Close();
                File.Delete(path);
                StreamWriter writer = new StreamWriter(path + ".tmp");
                foreach (var each in ret)
                {
                    writer.WriteLine(each);
                }
                writer.Close();
                File.Copy(path + ".tmp", path);
                File.Delete(path + ".tmp");
            }

            if (relatepath.IsSuffix(".Anim", System.StringComparison.CurrentCultureIgnoreCase))
            {
                string path = Application.dataPath.Replace("Assets", "") + relatepath;
                path = path.Replace("\\", "/");
                StreamReader fs = new StreamReader(path);
                List<string> ret = new List<string>();
                string line;
                while ((line = fs.ReadLine()) != null)
                {
                    line = line.Replace("\n", "");
                    if (line.IndexOf("m_LoopTime: 0") != -1)
                    {
                        line = "    m_LoopTime: 1";
                    }
                    ret.Add(line);
                }
                fs.Close();
                File.Delete(path);
                StreamWriter writer = new StreamWriter(path + ".tmp");
                foreach (var each in ret)
                {
                    writer.WriteLine(each);
                }
                writer.Close();
                File.Copy(path + ".tmp", path);
                File.Delete(path + ".tmp");
            }
        }
        AssetDatabase.Refresh();
    }
    #endregion


    #region GetAssetsPath 收集多个文件的路径到剪切板
    [MenuItem("Assets/YouYouMenuExt/收集多个文件的路径到剪切板")]
    public static void GetAssetsPath()
    {
        Object[] objs = Selection.objects;
        string relatepath = string.Empty;
        for (int i = 0; i < objs.Length; i++)
        {
            relatepath += AssetDatabase.GetAssetPath(objs[i]);
            if (i < objs.Length - 1) relatepath += "\n";
        }
        GUIUtility.systemCopyBuffer = relatepath;
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/YouYouMenuExt/收集多个文件的路径到剪切板(Resources)")]
    public static void GetAssetsPathResources()
    {
        Object[] objs = Selection.objects;
        string relatepath = string.Empty;
        for (int i = 0; i < objs.Length; i++)
        {
            string assetPath = AssetDatabase.GetAssetPath(objs[i]);
            assetPath = assetPath.Split("Resources/")[1];
            assetPath = assetPath.Split('.')[0];
            relatepath += assetPath;
            if (i < objs.Length - 1) relatepath += "\n";
        }
        GUIUtility.systemCopyBuffer = relatepath;
        AssetDatabase.Refresh();
    }
    #endregion

    #region GetAssetsPath 收集多个文件的名字到剪切板
    [MenuItem("Assets/YouYouMenuExt/收集多个文件的名字到剪切板")]
    public static void GetAssetsName()
    {
        Object[] objs = Selection.objects;
        string relatepath = string.Empty;
        for (int i = 0; i < objs.Length; i++)
        {
            relatepath += objs[i].name;
            if (i < objs.Length - 1) relatepath += "\n";
        }
        GUIUtility.systemCopyBuffer = relatepath;
        AssetDatabase.Refresh();
    }
    #endregion

    #region CompiltHotifxDll 生成并拷贝热更新程序集到Download文件夹
    [MenuItem("YouYouTools/生成并拷贝热更新程序集到Download文件夹")]
    public static void CompiltHotifxDll()
    {
        HybridCLR.Editor.Commands.CompileDllCommand.CompileDll(EditorUserBuildSettings.activeBuildTarget);

        string CodeDir = "Assets/Game/Download/Hotfix/";

        string ScriptAssembliesDir = Application.dataPath + "/../" + "HybridCLRData/HotUpdateDlls/" + EditorUserBuildSettings.activeBuildTarget.ToString() + "/Assembly-CSharp.dll";
        File.Copy(ScriptAssembliesDir, Path.Combine(CodeDir, "Assembly-CSharp.dll.bytes"), true);

        string aotMetaAssemblyDir = Application.dataPath + "/../" + "HybridCLRData/AssembliesPostIl2CppStrip/" + EditorUserBuildSettings.activeBuildTarget + "/";
        foreach (var aotDllName in HotfixCtrl.aotMetaAssemblyFiles)
        {
            File.Copy(aotMetaAssemblyDir + aotDllName + ".dll", Path.Combine(CodeDir, aotDllName + ".dll.bytes"), true);
        }
    }
    #endregion
}
