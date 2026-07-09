using UnityEngine;

public class CannonBallLifeTime : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnMouseDown()
    {
        //Si clicamos con el ratón en la bola, también desaparcerá:
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D elOtro)
    {
        //Si la bola golpea con la pared drecha se destruye, porque sino sería muy fácil dar a las dianas
        if(elOtro.gameObject.CompareTag("LimitRight"))
        {
            Destroy(gameObject);
        }
    }

}
