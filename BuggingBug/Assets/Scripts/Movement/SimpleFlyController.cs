using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleFlyController : MonoBehaviour
{
    [SerializeField]
    float deadZone;
    [SerializeField]
    float force;
    [SerializeField]
    FlyAnimationController animationController;

    Rigidbody rb;
    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        animationController.SetFlyBlend(1, 0.1f);
    }
    private void Update()
    {
        AddForceTowardsMouse();
        LookAtMouseDir();
    }

    private void LookAtMouseDir()
    {
        Vector3 dir = GetDirFromPlayerToMouse_XY();
        if (dir.x > 0 && transform.rotation.y != -180f)
        {
            transform.DORotate(Vector3.up * -180f, 0.25f).SetAutoKill();
        }
        else if (dir.x < 0 && transform.rotation.y != 0)
        {
            transform.DORotate(Vector3.zero, 0.25f).SetAutoKill();
        }
    }

    public Vector3 GetMousePosOnXYPlane()
    {
        Vector3 mousePos = new Vector2(Mathf.Clamp(Input.mousePosition.x, Screen.width / 20, Screen.width - Screen.width / 20), Mathf.Clamp(Input.mousePosition.y, Screen.height / 20, Screen.height - Screen.height / 20));
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
        if (dir.magnitude >= deadZone)
        {
            rb.AddForce(dir.normalized * force * Time.deltaTime);
        }
    }
}
