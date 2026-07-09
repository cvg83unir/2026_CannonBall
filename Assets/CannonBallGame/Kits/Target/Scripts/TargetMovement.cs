using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using Random = UnityEngine.Random;
public class TargetMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3;
    [SerializeField] float maxSecondsSameDirection = 2f;

    private Vector2 movementDirection = Vector2.zero;
    private Rigidbody2D rb2D;
    private float directionSecondsCounter = 0f;
    private bool changeDirectionDueToCollision = false;


    private void Awake()
    {
        //Cacheamos el rigidbody:
        this.rb2D = GetComponent<Rigidbody2D>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SetDirection());
    }

    IEnumerator SetDirection()
    {
        //Para empezar, establecemos una dirección inicial:
        this.movementDirection = SetNewDestiny();

        while (true)
        {
            this.directionSecondsCounter += Time.deltaTime;

            if (CheckChangeDirection())
            {
                this.movementDirection = SetNewDestiny();
            }

            yield return null;
        }
    }

    private Vector2 SetNewDestiny()
    {
        Vector2 searchDirection = Vector2.zero;

        if (this.movementDirection == Vector2.zero)
        {
            //Si no tenemos una dirección de base (al principio) escogeremos al azar entre arriba y abajo:
            int randomValue  = Random.Range(0, 2);

            //Los posibles valores son 0 ó 1:
            if (randomValue == 0)
            {
                searchDirection = Vector2.up;
            }
            else if (randomValue == 1)
            {
                searchDirection = Vector2.down;
            }
            else
            {
                Debug.Log("No debería ocurrir");
            }

        }
        else if(this.movementDirection == (Vector2.up))
        {
            //Si estábamos yendo hacia arriba, ahora iremos hacia abajo:
            searchDirection = Vector2.down;
        }
        else if (this.movementDirection == (Vector2.down))
        {
            //Si estábamos yendo hacia abajo, ahora iremos hacia arroba:
            searchDirection = Vector2.up;
        }
        else
        {
            Debug.Log("Tampoco debería ocurrir");
        }

        //Reseteamos el contador de segundos de movimeinto en la misma dirección:
        this.directionSecondsCounter = 0;
        this.changeDirectionDueToCollision = false;
        //this.changeDirectionDueToOutOfBounds = false;

        return searchDirection;

    }

    private bool CheckChangeDirection()
    {
        if (this.directionSecondsCounter >= this.maxSecondsSameDirection)
        {
            Debug.Log("Enemigo, cambiar sentido por máximo número de segundos en la misma dirección");
            return true;
        }
        else if (this.changeDirectionDueToCollision == true)
        {
            Debug.Log("Enemigo, cambiar sentido por colisión");
            return true;
        }
        //else if (this.changeDirectionDueToOutOfBounds == true)
        //{
        //    Debug.Log("Enemigo, cambiar sentido por irse fuera de límites establecidos");
        //    return true;
        //}
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        //Si la diana choca contra un límite, hay que cambiar la dirección:
        if (elOtro.CompareTag("Limits"))
        { 
            this.changeDirectionDueToCollision = true;
        }
        else
        {
            Debug.Log("Contra qué colisiona");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.rb2D.linearVelocity = this.movementDirection * movementSpeed;
    }
}
