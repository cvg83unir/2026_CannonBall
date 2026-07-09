using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonShot : MonoBehaviour
{

    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform shotPoint;
    [SerializeField] float shotSpeed = 15f;
    [SerializeField] float requiredShotDelay = 1f;
    float currentShotWaitingTime = 0;
    [SerializeField] InputActionReference fire;

    private Rigidbody2D rd2D;

    private void Awake()
    {
        this.rd2D = GetComponent<Rigidbody2D>();
        this.currentShotWaitingTime = 0f;
    }

    private void OnEnable()
    {
        this.fire.action.Enable();
        this.fire.action.started += OnFire;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        //Sólo instanciaremos el disparo si ha pasado el tiempo mínimo necesario entre disparo:
        if (this.currentShotWaitingTime >= this.requiredShotDelay)
        {
             InstantiateBall();
        }
    }

    private void InstantiateBall()
    {
        GameObject newCannonBall = Instantiate(this.cannonBallPrefab, shotPoint.position, shotPoint.rotation);
        newCannonBall.GetComponent<Rigidbody2D>().linearVelocity = shotPoint.right * shotSpeed;

        //reiniciamos el tiempo de espera para poder volver a disparar: 
        this.currentShotWaitingTime = 0;
        //Destroy(newCannonBall, 5f);
    }

    private void Update()
    {
        this.currentShotWaitingTime += Time.deltaTime;

        //if (Keyboard.current.spaceKey.wasPressedThisFrame)
        //{
        //    InstantiateBall();
        //}

    }

    private void OnDisable()
    {
        this.fire.action.Disable();
        this.fire.action.started += OnFire;
    }
}
