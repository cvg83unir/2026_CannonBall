using UnityEngine;
using UnityEngine.InputSystem;

public class CannonShot : MonoBehaviour
{

    [SerializeField] GameObject cannonBallPrefab;
    [SerializeField] Transform shotPoint;
    [SerializeField] float shotSpeed = 15f;

    private Rigidbody2D rd2D;

    private void Awake()
    {
        this.rd2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject newCannonBall = Instantiate(this.cannonBallPrefab, shotPoint.position, shotPoint.rotation);
            newCannonBall.GetComponent<Rigidbody2D>().linearVelocity = shotPoint.right * shotSpeed;
            //Destroy(newCannonBall, 5f);
        }

    }
}
