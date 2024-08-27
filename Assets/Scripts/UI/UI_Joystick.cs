using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Joystick : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    [SerializeField]
    Image background;
    [SerializeField]
    Image handler;
    Image icon;

    
    float joystickRadius;

    public Vector2 init;
    public Vector2 joystickDirection;
    Vector2 touchPosition;

    private void Start()
    {
        // ������Ʈ�� �ڽ����� �ִ�  Background������Ʈ�� �̹����� �����´�.
        background = transform.GetChild(0).GetComponent<Image>();
        // ������Ʈ�� �ڽ����� �ִ� Handler������Ʈ�� �̹����� �����´�.
        handler = transform.GetChild(1).GetComponent<Image>();        

        init = background.rectTransform.position;
        joystickRadius = background.rectTransform.sizeDelta.y * 0.5f; 
    }


    
    public void OnPointerDown(PointerEventData eventData)
    {               


        background.transform.position = eventData.position;
        handler.transform.position = eventData.position;

        touchPosition = eventData.position;
        
    }    

    public void OnDrag(PointerEventData eventData)
    {
        // ��ġ�� ��ġ�� ���̽�ƽ�� ��ġ�� ���� �̵��� �Ÿ��� ���Ѵ�.
        joystickDirection = eventData.position - touchPosition;
        // ���̽�ƽ�� ��ġ�� ��ġ�� ��ġ�� �̵���Ų��.
        handler.transform.position = touchPosition + Vector2.ClampMagnitude(joystickDirection, joystickRadius);

        GameManager.Instance.MoveDir = joystickDirection.normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        joystickDirection = Vector2.zero;     
        background.transform.position = init;
        handler.transform.position = init;

        GameManager.Instance.MoveDir = Vector2.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
     
    }

}

