using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragAndDrop : NetworkBehaviour
{
    public GameObject Canvas;
    public GameObject Dropzone;
    public PlayerManager PlayerManager;

    private bool isDragging = false;
    private bool isDraggable = true;

    private Vector2 startPosition;
    private GameObject startParent;
    private bool isOverDropZone = false;
    private GameObject DropZone;
    Vector2 mousePosition;

    private void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        DropZone = GameObject.Find("DropZone");
        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition)
        if (isDragging)
        {
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "DropZone")
        {
            isOverDropZone = true;
            DropZone = collision.gameObject;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
    }

    public void StartDrag()
    {
        if (!isDraggable) return;
        startPosition = transform.position;
        isDragging = true;
        startParent = transform.parent.gameObject;
    }

    public void EndDrag()
    {
        if (!isDraggable) return;
        isDragging = false;
        

        if (isOverDropZone)
        {
            transform.SetParent(DropZone.transform, false);
            isDraggable = false;
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
        }
        else {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }

    
}
