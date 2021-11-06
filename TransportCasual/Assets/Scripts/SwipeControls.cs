using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public float horizontalTresholdPercent = 0.01f;
    public float verticalTresholdPercent = 0.01f;
    public string horizontalParamName = "blendX";
    public string verticalParamName = "blendY";
    private bool isPointerDown = false;

    public Vector2 Input => _input;

    private const int c_nonPointer = -2973642;
    private int _lastPointerId;
    public Player player;
    private PlayerMovement playerMov;
    private Vector2 _lastPoint;
    private Vector2 _mousePos;
    private Vector2 _input;
    private Vector3 startPlayerPos;
    private bool isPositive;

    private void Awake()
    {
        _lastPointerId = c_nonPointer;
        //joyPad.gameObject.SetActive(false);
    }
    public void ChangeRb()
    {
        playerMov = player.currentVehicle.GetComponentInChildren<PlayerMovement>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_lastPointerId == c_nonPointer)
        {
            //joyPad.gameObject.SetActive(true);
            _lastPointerId = eventData.pointerId;
            _lastPoint = eventData.position;

            isPointerDown = true;
            startPlayerPos = playerMov.transform.localPosition;
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (_lastPointerId == eventData.pointerId)
        {
            Drag(eventData.position);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == _lastPointerId)
        {
            //joyPad.gameObject.SetActive(false);
            _lastPointerId = c_nonPointer;
            //_lastPoint =
            _input = Vector2.zero;
            isPointerDown = false;
            playerMov.Input = Vector3.zero;

        }
    }


    private void Drag(Vector2 mousePos)
    {
        if (mousePos.x > _mousePos.x)
        {
            if (!isPositive)
            {
                _lastPoint = mousePos;
            }
            isPositive = true;
        }
        else if (mousePos.x < _mousePos.x)
        {
            if (isPositive)
            {
                _lastPoint = mousePos;
            }
            isPositive = false;
        }

        var diff = mousePos - _lastPoint;
        _mousePos = mousePos;
        //if (Mathf.Abs(diff.x) / Screen.width < horizontalTresholdPercent)
        //{
        //    diff.x = 0;
        //}
        //if (Mathf.Abs(diff.y) / Screen.height < verticalTresholdPercent)
        //{
        //    diff.y = 0;
        //}
        //Debug.Log("mouse " + mousePos.x + "lastx " + _lastPoint.x + "diff " + diff);
        _input = diff/2;
        //Debug.Log("dif"+ diff);
    }

    private void Update()
    {
        if (playerMov != null)
        {



            if (isPointerDown)
            {
                //Debug.Log(player.transform.localPosition.y - startPlayerPos.y);
                //Debug.Log(_lastPoint.y+ " "+ _mousePos.y);
                _lastPoint.y = Mathf.MoveTowards(_lastPoint.y, _mousePos.y, Mathf.Abs(_lastPoint.y - _mousePos.y) * 50 * Time.deltaTime);
                //_lastPoint = _mousePos;
              

                if (_input.x > 0)
                {
                    _input.x = Mathf.Clamp(_input.x - (playerMov.transform.localPosition.y - startPlayerPos.y) * 10, 0, 500);
                }
                else if(_input.x < 0)
                {
                    _input.x = Mathf.Clamp(_input.x - (playerMov.transform.localPosition.y - startPlayerPos.y) *10, -500, 0);
                }
                playerMov.Input = _input;

                _lastPoint.x = _mousePos.x;
                startPlayerPos.y = playerMov.transform.localPosition.y;

            }
        }
    }
}
