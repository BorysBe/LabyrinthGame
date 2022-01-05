using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HitboxButton : MonoBehaviour
{

    [SerializeField] int hitDamage = 20;
    GameObject chavs;

    private void Start()
    {
        chavs = GameObject.FindGameObjectWithTag("Chav");
    }

    private void GetLastTouchPoint(PointerEventData eventData)
    {

        var cam = Camera.main;
        Vector2 mousePos = new Vector2();
        mousePos.x = eventData.position.x;
        mousePos.y = cam.pixelHeight - eventData.position.y;

        LastTouchPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        LastTouchPoint = new Vector3(LastTouchPoint.x, -LastTouchPoint.y, LastTouchPoint.z);
        Debug.Log("Last touch value :" + LastTouchPoint.x + ", " + LastTouchPoint.y + ", " + LastTouchPoint.z);
    }

    private string m_ControlPath;
    protected Vector3 LastTouchPoint { get { return _lastTouchPoint; } private set { _lastTouchPoint = value; } }
    private Vector3 _lastTouchPoint;


    public void ActionOnTouch()
    {
        chavs.GetComponent<Chavs>().Damage(hitDamage);
        chavs.GetComponent<Chavs>().DrawInjuries(LastTouchPoint);
    }
}
