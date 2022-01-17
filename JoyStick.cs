using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image back;
    public Image stick;
    float radius = 0f;
    Vector3 dir = Vector3.zero;
    Vector3 dir3D = Vector3.zero;
    Animator animator;
	
    public Vector3 DIR
    {
        get {
            dir3D.Set(dir.normalized.x, 0, dir.normalized.y);
            return dir3D; 
        }
    }

    Vector3 centerPos = Vector3.zero;

    void Start()
    {
        centerPos = back.transform.position;
        radius = back.rectTransform.sizeDelta.x * 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        stick.transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        stick.transform.position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float Distance = Vector2.Distance(eventData.position, centerPos);
        Vector3 tmp = eventData.position;
        dir = tmp - centerPos;

        if (Distance > radius)
        {
            stick.transform.position = centerPos + dir.normalized * radius;
        }
        else
        {
            stick.transform.position = centerPos + dir.normalized * Distance;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stick.transform.position = centerPos;
        dir = Vector3.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.transform.position = centerPos;
        dir = Vector3.zero;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
