using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonShot : MonoBehaviour
{

    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform shotPoint;
    [SerializeField] float shotSpeed = 15f;
    [SerializeField] InputActionReference fire;

    private Rigidbody2D rd2D;

    private void Awake()
    {
        this.rd2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        this.fire.action.Enable();
        this.fire.action.started += OnFire;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        InstantiateBall();
    }

    private void InstantiateBall()
    {
        GameObject newCannonBall = Instantiate(this.cannonBallPrefab, shotPoint.position, shotPoint.rotation);
        newCannonBall.GetComponent<Rigidbody2D>().linearVelocity = shotPoint.right * shotSpeed;
        //Destroy(newCannonBall, 5f);
    }

    //private void Update()
    //{
    //    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    //    {
    //        GameObject newCannonBall = Instantiate(this.cannonBallPrefab, shotPoint.position, shotPoint.rotation);
    //        newCannonBall.GetComponent<Rigidbody2D>().linearVelocity = shotPoint.right * shotSpeed;
    //        //Destroy(newCannonBall, 5f);
    //    }

    //}

    private void OnDisable()
    {
        this.fire.action.Disable();
        this.fire.action.started += OnFire;
    }
}
