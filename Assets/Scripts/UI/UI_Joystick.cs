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
        // 오브젝트의 자식으로 있는  Background오브젝트의 이미지를 가져온다.
        background = transform.GetChild(0).GetComponent<Image>();
        // 오브젝트의 자식으로 있는 Handler오브젝트의 이미지를 가져온다.
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
        // 터치한 위치와 조이스틱의 위치를 빼서 이동한 거리를 구한다.
        joystickDirection = eventData.position - touchPosition;
        // 조이스틱의 위치를 터치한 위치로 이동시킨다.
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

