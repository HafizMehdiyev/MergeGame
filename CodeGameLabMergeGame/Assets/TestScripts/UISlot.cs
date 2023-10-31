using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UISlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        var otherItemTransform = eventData.pointerDrag.transform;
        if (transform.childCount == 0)
        {
            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
        }
        else
        {
            GameObject firstGO = transform.GetChild(0).gameObject;
            GameObject secondGO = otherItemTransform.gameObject;
            if (firstGO.tag == secondGO.tag && firstGO != secondGO)
            {
                MergeAnimation(firstGO, secondGO);
            }
            else
                Reverse(firstGO, secondGO);
        }
    }
    private void Reverse(GameObject FirstGO, GameObject SecondGO)
    {
        GameObject firstParent = FirstGO.transform.parent.gameObject;
        GameObject secondParent = SecondGO.transform.parent.gameObject;

        FirstGO.transform.SetParent(secondParent.transform);
        SecondGO.transform.SetParent(firstParent.transform);


        FirstGO.transform.DOLocalMove(Vector3.zero, .1f);
        SecondGO.transform.DOLocalMove(Vector3.zero, .1f);
    }

    private void MergeAnimation(GameObject first, GameObject second)
    {
        second.transform.SetParent(first.transform.parent);
        RectTransform firstRect = first.GetComponent<RectTransform>();
        RectTransform secondRect = second.GetComponent<RectTransform>();


        firstRect.DOAnchorPosX(firstRect.anchoredPosition.x - 40f, .3f).
            SetLoops(2, LoopType.Yoyo);

        secondRect.DOAnchorPosX(secondRect.anchoredPosition.x + 40f, .3f).
            SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            string cubeNumberText = (TagManager.Instance.TagReturn(transform.GetChild(0).gameObject)).ToString();
            Destroy(second);
        });






    }
}
