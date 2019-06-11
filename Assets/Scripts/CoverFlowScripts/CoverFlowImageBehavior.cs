using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

/// <summary>
/// coverflow图片子对象行为
/// </summary>
public class CoverFlowImageBehavior : MonoBehaviour, IPointerClickHandler
{
    public int coverFlowIndex;
    public int t2dIndex;

    RectTransform rTrs;
    RawImage rawImage;
    AspectRatioFitter aspectRatioFitter;

    CoverFlowBehavior coverFlowBehavior;

    public UnityEvent unityEvent;

    private void Awake()
    {
        rTrs = GetComponent<RectTransform>();
        rawImage = GetComponent<RawImage>();
        aspectRatioFitter = GetComponent<AspectRatioFitter>();

        coverFlowBehavior = transform.parent.GetComponent<CoverFlowBehavior>();
        coverFlowBehavior.setImageIndex += SetIndex;   //订阅刷新图片索引事件
        coverFlowBehavior.setImageDisPlay += SetImageDisplay;  //订阅刷新图片显示效果事件
    }


    void Start()
    {
        SetImageDisplay();
    }


    public void OnPointerClick(PointerEventData data)
    {
        if (coverFlowIndex == 0)
        {
            unityEvent.Invoke();
        }
    }

    /// <summary>
    /// 设置CoverFlow中的位置索引和图片的索引
    /// </summary>
    /// <param name="direction"></param>
    public void SetIndex(int direction)
    {
        coverFlowIndex += direction;
        if (coverFlowIndex > coverFlowBehavior.childCount / 2)
        {
            coverFlowIndex = -coverFlowBehavior.childCount / 2;
            t2dIndex -= coverFlowBehavior.childCount;
            while(t2dIndex < 0)
                t2dIndex += coverFlowBehavior.t2ds.Count;
        }
        if (coverFlowIndex < -coverFlowBehavior.childCount / 2)
        {
            coverFlowIndex = coverFlowBehavior.childCount / 2;
            t2dIndex += coverFlowBehavior.childCount;
            while (t2dIndex > coverFlowBehavior.t2ds.Count - 1)
                t2dIndex -= coverFlowBehavior.t2ds.Count;
        }
    }

    /// <summary>
    /// 根据索引确定图片显示效果
    /// </summary>
    public void SetImageDisplay()
    {
        float targetPosX = coverFlowBehavior.imageGap * coverFlowIndex;  //目标位置
        float sizeRatio = (coverFlowBehavior.childCount - Mathf.Abs(coverFlowIndex)) / (float)coverFlowBehavior.childCount;  //目标尺寸
        int targetSiblingIndex = coverFlowBehavior.childCount - 1 - (coverFlowIndex >= 0 ? Mathf.Abs(coverFlowIndex) * 2 : Mathf.Abs(coverFlowIndex) * 2 - 1);  //目标层级
        float alpha = 1 - (1 - sizeRatio) * 0.8f;

        rTrs.SetSiblingIndex(targetSiblingIndex);

        rTrs.DOSizeDelta(coverFlowBehavior.imageHeight * Vector2.one * sizeRatio, 0.3f);
        rTrs.DOAnchorPosX(targetPosX, 0.3f).onComplete = () =>
        {
            rawImage.texture = coverFlowBehavior.t2ds[t2dIndex];
            aspectRatioFitter.aspectRatio = rawImage.texture.width / (float)rawImage.texture.height;
            rawImage.color = new Color(alpha, alpha, alpha, 1);
        };
    }

}
