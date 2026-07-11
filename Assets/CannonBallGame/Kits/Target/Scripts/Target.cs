using UnityEngine;


public class Target : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if(elOtro.tag.StartsWith("Player"))
        {
            //Destuimos la diana que hayamos tocado...
            Destroy(gameObject);

            //...pero también otro examen:
            Destroy(elOtro.gameObject);

            Score.score += 1;
            Globals.currentNumberOfTargets--;
            Debug.Log("-Número de dianas: " + Globals.currentNumberOfTargets.ToString());
        }

    }

}
