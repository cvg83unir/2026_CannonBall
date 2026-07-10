using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class WindmillController : MonoBehaviour
{
    private int goodBladesCounter;
    private BladeController[] bladesArray;

    private void Awake()
    {
        //miramos todas las aspas que tiene el molino, que normalmente van a ser 2, pero por si acaso:
        this.bladesArray = GetComponentsInChildren<BladeController>();

        if(this.bladesArray != null)
        {
            this.goodBladesCounter = bladesArray.Length;
        }

    }

    private void OnEnable()
    {
        if (this.bladesArray != null)
        {

            foreach (BladeController bc in bladesArray)
            {
                //nos suscribimos al evento para saber cuando se ha destruido un aspa():
                bc.onBladeDestroyed.AddListener(OnBladeDestroyed);
            }

        }
    }

    private void OnBladeDestroyed()
    {
        this.goodBladesCounter--;
        Debug.Log("Aspa destruida. Quedan: " + this.goodBladesCounter);

        //Si ya no quedan aspas, destruimos el propio molino:
        if(this.goodBladesCounter<=0)
        {
            //No quedan aspas, destruimos al propio molino:
            Globals.currentNumberOfWindMills--;
            Debug.Log("No quedan aspas, destruimos al propio molino. Molinos Restantes: " + Globals.currentNumberOfWindMills);
            Destroy(this.gameObject);
        }
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        if (this.bladesArray != null)
        {

            foreach (BladeController bc in bladesArray)
            {
                //nos suscribimos al evento para saber cuando se ha destruido un aspa():
                bc.onBladeDestroyed.RemoveListener(OnBladeDestroyed);
            }

        }
    }
}
