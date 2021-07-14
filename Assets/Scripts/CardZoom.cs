using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;
    private GameObject ZoomCard;
    
    

    private void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        ZoomCard = Instantiate(gameObject, new Vector2(400, 225), Quaternion.identity);
      
        ZoomCard.GetComponent<Image>().raycastTarget = false;
        ZoomCard.name = "ZoomedCard";
        ZoomCard.transform.SetParent(Canvas.transform, false);
        ZoomCard.layer = LayerMask.NameToLayer("Zoom");

        RectTransform rect = ZoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(120, 160);
    }

    public void OnHoverExit()
    {
        Destroy(ZoomCard);
    }

    public void OnDragging()
    {
        Destroy(ZoomCard);
    }
}
