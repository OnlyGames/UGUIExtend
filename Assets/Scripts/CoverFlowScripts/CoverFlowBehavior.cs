using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// coverflow父对象行为
/// </summary>
public class CoverFlowBehavior : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Tooltip("CoverFlow的图片列表")]
    public List<Texture2D> t2ds;
    [Tooltip("图片间距的基础值")]
    public float imageGap = 100;
    [Tooltip("图片基础尺寸")]
    public float imageHeight = 300;
    [Tooltip("起始图片的索引")]
    public int initIndex = 0;

    public event SetImageDisPlay setImageDisPlay;  //发布刷新图片显示效果事件
    public event SetImageIndex setImageIndex;  //发布刷新图片索引事件

    [HideInInspector]
    public int childCount;

    float dragBeginPosX;

    private void Awake()
    {
        childCount = transform.childCount;

    }


    void Start()
    {
        InitIndex(initIndex);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        dragBeginPosX = data.position.x;
    }
    public void OnDrag(PointerEventData data)
    {

    }
    public void OnEndDrag(PointerEventData data)
    {
        if (data.position.x - dragBeginPosX > 10)
            RefreshImageIndex(1);
        else if (data.position.x - dragBeginPosX < -10)
            RefreshImageIndex(-1);

        setImageDisPlay();
    }

    void RefreshImageIndex(int direction)
    {
        setImageIndex(direction);
    }

    /// <summary>
    /// 初始化coverflow的显示位置
    /// </summary>
    /// <param name="index">起始索引</param>
    public void InitIndex(int index)
    {
        if (t2ds.Count == 0)
            throw new System.Exception("Coverflow need at least one image, please assign image first!");

        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).GetComponent<CoverFlowImageBehavior>().t2dIndex = index;
            if (++index > t2ds.Count - 1)
                index = 0;
        }
    }


    public delegate void SetImageIndex(int direction);
    public delegate void SetImageDisPlay();
}
