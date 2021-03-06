using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField]
    private float swipeMoveSpeed;

    [SerializeField]
    private float carTiltSpeed;

    [SerializeField]
    private bool isPlane;

    private float noTouchTimer;
    private Rigidbody rb;

    public float roadEdgeMargin = .5f;

    private float roadWidth;
    [NonSerialized]public Vector2 Input;
    private float RoadMin => RoadMax * -1;
    private float RoadMax => roadWidth - 0.5f;
    public bool Started { get; set; }

    [NonSerialized] public bool isCenter;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        roadWidth = transform.parent.parent.GetComponent<Player>().RoadMeshCreator.roadWidth;

    }



    private void OnEnable()
    {
        //SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        EventManager.StartListening(Events.StartTap, OnTap);
        EventManager.StartListening(Events.VehicleChange, OnVehicleChange);
        EventManager.StartListening(Events.LevelFinished, OnFinish);
    }

    private void OnDisable()
    {
        //SwipeDetector.OnSwipe -= SwipeDetector_OnSwipe;
        EventManager.StopListening(Events.StartTap, OnTap);
        EventManager.StopListening(Events.VehicleChange, OnVehicleChange);
        EventManager.StopListening(Events.LevelFinished, OnFinish);
    }

    private void OnVehicleChange(EventParam param)
    {
        SusPooler.instance.SpawnFromPool("SmokePoofs", transform.position, Quaternion.identity);
    }

    private void OnTap(EventParam param)
    {
        Started = true;
    }

    private void OnFinish(EventParam param)
    {
        Started = false;
    }

    //private void SwipeDetector_OnSwipe(SwipeData data)
    //{
    //    if (!Started) return;

    //    noTouchTimer = 0.1f;
    //    int direction = data.Direction == SwipeDirection.Left ? -1 : 1;

    //    if (direction > 0 && (RoadMax - transform.localPosition.y) < roadEdgeMargin) return;
    //    if (direction < 0 && (transform.localPosition.y - RoadMin) < roadEdgeMargin) return;

    //    var locVel = transform.InverseTransformDirection(rb.velocity);
    //    locVel.y = direction * data.DistanceX / (Screen.width / 150) * swipeMoveSpeed;

    //    TiltCar(locVel.y);
    //    rb.velocity = transform.TransformDirection(locVel);

    //}

    //private void StopIfNoTouch()
    //{
    //    if (noTouchTimer >= 0f)
    //    {
    //        noTouchTimer -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        var locVel = transform.InverseTransformDirection(rb.velocity);
    //        locVel.y = 0f;
    //        TiltCar(locVel.y);
    //        rb.velocity = transform.TransformDirection(locVel);

    //    }
    //}
    private void Update()
    {
        HandleInput(Input);
    }
    private void TiltCar(float tiltAmount)
    {
        Quaternion target = !isPlane ? Quaternion.Euler(-tiltAmount, 0, 0) : Quaternion.Euler(0, 0, -tiltAmount);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * carTiltSpeed);
    }

    internal void HandleInput(Vector2 notNormalinput)
    {

        var input = notNormalinput;
        var tempPos = transform.localPosition;
   
        if (isCenter)
        {
            Debug.Log("im center boi");
            tempPos.x = Mathf.Lerp(tempPos.x, 0, 2f * Time.deltaTime);
        }
        else
        {
           //Debug.Log(input.x);
            //Debug.Log($"clamp( {tempPos.y + (input.x) * swipeMoveSpeed }, {-5.5f}, {5.5f} )");
            tempPos.y = Mathf.Clamp(tempPos.y+ (input.x) * swipeMoveSpeed * Time.deltaTime, -roadEdgeMargin, roadEdgeMargin);
            //Debug.Log(tempPos.x);
        }
        //tempPos.x += (input.x) * sideSpeed*Time.deltaTime;
        //tempPos.z += 0.1f * speed;
        //Debug.Log(tempPos);

        transform.localPosition = tempPos;

        //rb.MovePosition(tempPos);

        //transform.localPosition = tempPos;

    }

}