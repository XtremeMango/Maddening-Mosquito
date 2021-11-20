
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class FlyController : MonoBehaviour
{
    [SerializeField]
    float deadZone;
    [SerializeField]
    Transform attachPoint;
    [SerializeField]
    float attachSpeed = 1f;
    [SerializeField]
    float dettachSpeed = 1f;
    [SerializeField]
    Ease attachEase;
    [SerializeField]
    Ease detachEase;
    [SerializeField]
    float force;
    [SerializeField]
    FlyAnimationController animationController;

    Rigidbody rb;
    bool controlEnabled;
    bool flightControlActive;
    bool mounted;
    bool firstClick;

    private void Awake()
    {
        flightControlActive = false;
        controlEnabled = true;
        rb = GetComponent<Rigidbody>();
        animationController.SetFlyBlend(1,0.1f);
        
    }
    private void Start()
    {
        Events.events.OnLeftMBClicked += ToggleMount;
        Events.events.OnGameStateChangedToActive += EnableMove;
        Events.events.OnGameStateChangedToGameOver += Disable;
        Events.events.OnPlayerOutOfBlood += Wimper;
    }

    private void Wimper()
    {
        animationController.SetFlyBlend(0, 0.1f);
        rb.constraints = RigidbodyConstraints.None;
        rb.AddTorque(Random.insideUnitSphere.normalized * 1000f);
        StartCoroutine("WimperOscillate");
        rb.useGravity = true;
    }

    public IEnumerator WimperOscillate()
    {
        float t = 0;
        while(true)
        {
            float force = Mathf.Lerp(0, 1f, t);
            rb.AddForce(Vector3.right * force * Mathf.Sin(Time.realtimeSinceStartup*Mathf.PI));
            t += Time.deltaTime;
            yield return null;
        }
    }

    private void Disable()
    {
        controlEnabled = false;
    }

    private void EnableMove()
    {
        flightControlActive = true;
        firstClick = true;
    }

    private void ToggleMount()
    {
        if (controlEnabled)
        {
            if (firstClick)
            {
                firstClick = false;
            }
            else
            {
                if (flightControlActive && !mounted)
                {
                    CheckNearestSurface();
                }
                else
                {
                    EnableFlightControl();
                }
            }
        }
    }

    private void Update()
    {
        if (flightControlActive && controlEnabled)
        {
            AddForceTowardsMouse();
            LookAtMouseDir();
        }
    }

    private void LookAtMouseDir()
    {
        Vector3 dir = GetDirFromPlayerToMouse_XY();
        if (dir.x > 0 && transform.rotation.y != -180f)
        {
            transform.DORotate(Vector3.up * -180f, 0.25f);
        }
        else if (dir.x < 0 && transform.rotation.y != 0)
        {
            transform.DORotate(Vector3.zero, 0.25f);
        }
    }

    public Vector3 GetMousePosOnXYPlane()
    {
        Vector3 mousePos = new Vector2(Mathf.Clamp(Input.mousePosition.x,Screen.width/20,Screen.width-Screen.width/20), Mathf.Clamp(Input.mousePosition.y, Screen.height / 20, Screen.height - Screen.height / 20));
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -1 * Camera.main.transform.position.z));
    }

    public Vector3 GetDirFromPlayerToMouse_XY()
    {
        Vector3 worldPosition = GetMousePosOnXYPlane();
        Vector3 dir = (worldPosition - transform.position);
        dir.z = 0;
        return dir;
    }

    private void AddForceTowardsMouse()
    {
        Vector3 dir = GetDirFromPlayerToMouse_XY();
        if (dir.magnitude >= deadZone && flightControlActive)
        {
            rb.AddForce(dir.normalized * force * Time.deltaTime);
        }
    }

    private void CheckNearestSurface()
    {
        Ray ray = new Ray(transform.position, Vector3.forward * 15f);
        RaycastHit hit;
        Physics.Raycast(ray,out hit,15f,1<<7);
        if (hit.transform != null)
        {
            flightControlActive = false;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            Vector3 point = hit.point;
            point = (point.magnitude - attachPoint.localPosition.magnitude) * point.normalized;
            MoveToPoint(point,attachSpeed, attachEase, true);
            mounted = true;
            ManSection section = hit.collider.gameObject.GetComponent<ManSection>();
            section.SetActive();
            Events.events.Mounted(section);
        }
    }
    private void MoveToPoint(Vector3 point, float speed, Ease ease,bool setRotation)
    {
        Quaternion rotate;
        if (setRotation)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, (point - transform.position).normalized, out hit);
            rotate = Quaternion.FromToRotation(transform.up, hit.normal);
            animationController.SetFlyBlend(0, speed);
        }
        else
        {
            rotate = Quaternion.identity;
            animationController.SetFlyBlend(1, speed);
        }
        transform.DORotate(rotate.eulerAngles, speed);
        DOTween.To(()=>attachPoint.position,x => transform.position = x,point, attachSpeed).SetEase(ease);
    }

    private void EnableFlightControl()
    {
        if(!flightControlActive && mounted)
        {
            flightControlActive = true;
            mounted = false;
            rb.isKinematic = false;
            MoveToPoint(GetMousePosOnXYPlane(),dettachSpeed, detachEase,false);
            Events.events.Dismount();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deadZone);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.forward*10f);
    }
}
