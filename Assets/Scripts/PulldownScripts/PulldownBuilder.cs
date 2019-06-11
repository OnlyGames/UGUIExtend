using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[AddComponentMenu("GameObject/UI/Pulldown"), RequireComponent(typeof(RectTransform))]
public class PulldownBuilder : ScriptableObject
{
    [MenuItem("GameObject/UI/Pulldown")]
    public static void CreatePulldown(MenuCommand menuCommand)
    {
        GameObject pulldown = new GameObject("Pulldown");
        RectTransform rTrs = pulldown.AddComponent<RectTransform>();
        rTrs.SetParent((menuCommand.context as GameObject).transform, false);
    }
}
