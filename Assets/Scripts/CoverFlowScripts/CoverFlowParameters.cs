using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// coverflow参数单例类
/// </summary>
public class CoverFlowParameters : ScriptableObject
{
    [SerializeField]
    public int imageCount = 3;
    [SerializeField]
    public Vector2 imageSizeDelta = new Vector2();

    static CoverFlowParameters instance;
    public static CoverFlowParameters Instance
    {
        get
        {
            if (instance == null)
                instance = CreateInstance<CoverFlowParameters>();
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private CoverFlowParameters() { }
}
