
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// coverflow生成器
/// </summary>
[AddComponentMenu("GameObject/UI/CoverFlow"), RequireComponent(typeof(RectTransform))]
public class CoverFlowBuilder : ScriptableObject
{
    /// <summary>
    /// 创建coverflow
    /// </summary>
    /// <param name="menuCommand"></param>
    [MenuItem("GameObject/UI/CoverFlow")]
    public static void CreateCoverFlow(MenuCommand menuCommand)
    {
        //从asset中获取coverflow参数
        CoverFlowParameters.Instance = AssetDatabase.LoadAssetAtPath<CoverFlowParameters>("Assets/Settings/CoverFlowParameters.asset");
        CoverFlowParameters coverFlowParameters = CoverFlowParameters.Instance;

        //CoverFlow根对象
        GameObject coverflow = new GameObject("CoverFlow");
        RectTransform coverflowRTrs = coverflow.AddComponent<RectTransform>();
        Transform parent = (menuCommand.context as GameObject).transform;
        coverflow.transform.SetParent(parent, false);

        CoverFlowBehavior coverFlowBehavior = coverflow.AddComponent<CoverFlowBehavior>();

        coverFlowBehavior.imageHeight = coverFlowParameters.imageSizeDelta.y;

        //CoverFlow图片子对象
        for (int i = 0; i < coverFlowParameters.imageCount; i++)
        {
            GameObject image = new GameObject("Image" + i);
            RectTransform imageRTrs = image.AddComponent<RectTransform>();
            image.transform.SetParent(coverflowRTrs, false);
            imageRTrs.sizeDelta = coverFlowParameters.imageSizeDelta;

            image.AddComponent<RawImage>();
            AspectRatioFitter aspectRatioFitter = image.AddComponent<AspectRatioFitter>();
            aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;

            CoverFlowImageBehavior coverFlowImageBehavior = image.AddComponent<CoverFlowImageBehavior>();
            coverFlowImageBehavior.coverFlowIndex = i - coverFlowParameters.imageCount / 2;
        }
    }


    /// <summary>
    /// 创建用于参数配置的.asset文件
    /// </summary>
    [MenuItem("ObjectAsset/CreateCoverFlowAsset")]
    static void CreateCoverFlowAsset()
    {
        AssetBuilder.CreateAsset<CoverFlowBuilder>();
    }

}
