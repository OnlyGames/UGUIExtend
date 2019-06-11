using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class AssetBuilder : ScriptableObject
{

    /// <summary>
    /// 创建用于参数配置的.asset文件的泛型方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        string path = Application.dataPath + "/Settings";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        AssetDatabase.CreateAsset(CreateInstance(typeof(T)), string.Format("Assets/Settings/{0}.asset", typeof(T).ToString()));
    }


}
