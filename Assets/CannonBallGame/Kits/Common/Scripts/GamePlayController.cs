using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    [SerializeField] int numberOfPlayers = 2;

    [SerializeField] float secondsToPlay = 60f;
    [SerializeField] float secondsToReloadScene = 5f;
    private float currentPlayedSeconds = 0f;
    private bool playing = true;
    [Header("Text Canvas references")]
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textPlayerNumber;
    [SerializeField] private TextMeshProUGUI textEnding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Reseteamos el contador de puntos:
        Score.score = 0;
        //Reseteamos el resto de variables globales:
        Globals.ResetValuesForNextPlayer();
        this.textPlayerNumber.text = "Player No.: " + Globals.currentPlayerNumber; 
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            this.currentPlayedSeconds += Time.deltaTime;
            if (this.currentPlayedSeconds > this.secondsToPlay)
            {
                this.currentPlayedSeconds = this.secondsToPlay;
            }

            int remianingTimeToShow = (int)(this.secondsToPlay - this.currentPlayedSeconds);

            this.textTime.text = "Remaining Time: " + remianingTimeToShow;

            if (this.currentPlayedSeconds >= this.secondsToPlay)
            {
                this.currentPlayedSeconds = 0;
                this.playing = false;

                //El juego ha terminado para este player:

                ////Destruimos este game object que es el de los propios cañones
                //this.gameObject.SetActive(false);

                //Miramos todos los cañones que tenía el player y los desactivamos para que no se vean en pantalla:
                CannonShot[] cannonsChildren = GetComponentsInChildren<CannonShot>();

                foreach (CannonShot cs in cannonsChildren)
                {
                    cs.gameObject.SetActive(false);
                }

                if (Globals.currentPlayerNumber < this.numberOfPlayers)
                {
                    //Todavía quedan jugadores por jugar:
                    this.textEnding.gameObject.SetActive(true);
                    this.textEnding.color = Color.black;
                    this.textEnding.text = "Time Over. Prepare player number: " + Globals.currentPlayerNumber.ToString();
                    StartCoroutine(ReloadScenceAfterSomeSeconds());

                }
                else
                {
                    //Ya no quedan más jugadores por jugar:
                    this.textEnding.gameObject.SetActive(true);
                    this.textEnding.color = Color.red;
                    this.textEnding.text = "Game Over";
                }



            }
        }
    }

    IEnumerator ReloadScenceAfterSomeSeconds()
    {
        //Queremos que esta corutina se ejecute una única vez, pero después de X segundos para que el jugador pueda
        //leer el mensaje
        yield return new WaitForSeconds(this.secondsToReloadScene);

        //Ahora recargamos la escena:
        SceneManager.LoadScene("SampleScene");
    }
}
