using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

//[AddComponentMenu("Input/On-Screen Button")]
public abstract class Toucher : OnScreenControl, IPointerDownHandler, IPointerUpHandler
{
    [InputControl(layout = "Button")]
    [SerializeField] private string m_ControlPath;
    protected Vector3 LastTouchPoint { get { return _lastTouchPoint; } private set { _lastTouchPoint = value; } }

    protected Vector2 LastMousePosition { get { return _lastMousePosition; } private set { _lastMousePosition = value; } }
    private Vector3 _lastTouchPoint;
    private Vector2 _lastMousePosition;
    void Update()
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetLastTouchPoint(eventData);
        ActionOnTouch(eventData);
    }

    protected abstract void ActionOnTouch(PointerEventData eventData);

    private void GetLastTouchPoint(PointerEventData eventData)
    {

        var cam = Camera.main;
        Vector2 mousePos = new Vector2();
        mousePos.x = eventData.position.x;
        mousePos.y = cam.pixelHeight - eventData.position.y;

        LastTouchPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        LastTouchPoint = new Vector3(LastTouchPoint.x, - LastTouchPoint.y, LastTouchPoint.z);
        LastMousePosition = mousePos;
        Debug.Log("Last touch value :" + LastTouchPoint.x + ", " + LastTouchPoint.y + ", " + LastTouchPoint.z);
        Debug.Log("Last screen value :" + mousePos.x + ", " + mousePos.y );
    }

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

}

