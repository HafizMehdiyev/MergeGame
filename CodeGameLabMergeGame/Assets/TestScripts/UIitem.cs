using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIitem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerUpHandler
{
    private Canvas _mainCanvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private string oldName;
    private string dragName = "Dragging";

    private void Start()
    {
        oldName = gameObject.name;

        _rectTransform = GetComponent<RectTransform>();
        _mainCanvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.name = dragName;
        var slotTransform = _rectTransform.parent;
        slotTransform.SetAsLastSibling(); 
        _canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.name = oldName;
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }

   

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 uiPosition = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(uiPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.tag == transform.gameObject.tag)
            {
                hitObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
