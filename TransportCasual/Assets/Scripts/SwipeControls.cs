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
    [SerializeField] private PlayerMovement player;
    private Vector2 _lastPoint;
    private Vector2 _mousePos;
    private Vector2 _input;
    private Vector3 startPlayerPos;
    private bool isPositive;

    private void Awake()
    {
        _lastPointerId = c_nonPointer;
        //joyPad.gameObject.SetActive(false);
        Invoke(nameof(LateAwake), 0.1f);
    }

    private void LateAwake()
    {

        player = FindObjectOfType<PlayerMovement>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_lastPointerId == c_nonPointer)
        {
            //joyPad.gameObject.SetActive(true);
            _lastPointerId = eventData.pointerId;
            _lastPoint = eventData.position;

            isPointerDown = true;
            startPlayerPos = player.transform.localPosition;
            Debug.Log("bastým");
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
            player.Input = Vector3.zero;

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
        if (Mathf.Abs(diff.x) / Screen.width < horizontalTresholdPercent)
        {
            diff.x = 0;
        }
        if (Mathf.Abs(diff.y) / Screen.height < verticalTresholdPercent)
        {
            diff.y = 0;
        }
        //Debug.Log("mouse " + mousePos.x + "lastx " + _lastPoint.x + "diff " + diff);
        _input = diff / 2;
        //Debug.Log("dif"+ diff);
    }

    private void Update()
    {
        if (player != null)
        {



            if (isPointerDown)
            {
                //Debug.Log(player.transform.localPosition.y - startPlayerPos.y);
                //Debug.Log(_lastPoint.y+ " "+ _mousePos.y);
                _lastPoint.y = Mathf.MoveTowards(_lastPoint.y, _mousePos.y, Mathf.Abs(_lastPoint.y - _mousePos.y) * 10 * Time.deltaTime);
                //_lastPoint = _mousePos;
              

                if (_input.x >= 0)
                {
                    _input.x = Mathf.Clamp(_input.x - (player.transform.localPosition.y - startPlayerPos.y) * 50, 0, 500);
                }
                else
                {
                    _input.x = Mathf.Clamp(_input.x - (player.transform.localPosition.y - startPlayerPos.y) * 50, -500, 0);
                }
                player.Input = _input;

                _lastPoint.x = _mousePos.x;
                startPlayerPos.y = player.transform.localPosition.y;

            }
        }
    }
}
