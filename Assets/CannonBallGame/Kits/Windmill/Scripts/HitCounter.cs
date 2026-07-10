using UnityEngine;

public class HitCounter : MonoBehaviour
{
    [SerializeField] int maximumHitNumber = 5;

    private int currentHitNumber = 0;
    private SpriteRenderer sp;

    private void Awake()
    {
        //Cacheamos el sprite rendered:
        this.sp = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D elOtro)
    { 
        if (elOtro.gameObject.CompareTag("PlayerBullet"))
        {
            //Cada vez que una bala toque a uno de las aspas del molino, aumentamos en uno su contador de toques
            this.currentHitNumber++;

            if (this.currentHitNumber >= maximumHitNumber)
            {
                //Si ya se ha llegado al máximo número de hits que soporta el aspa, la destruimos:
                Destroy(this.gameObject);
            } 
            else
            {
                //Si no hemos llegado al toque, de todas maneras, vamos a ir oscureciendo el aspa
                //para que se sepa que está siendo dañado:
                if (this.currentHitNumber == 1)
                {                  
                    this.sp.color = new Color(211f/255f, 211f / 255f, 211f / 255f);
                }
                else if (this.currentHitNumber == 2)
                {
                    this.sp.color = new Color(192f / 255f, 192f / 255f, 192f / 255f);
                }
                else if (this.currentHitNumber == 3)
                {
                    this.sp.color = new Color(169f / 255f, 169f / 255f, 169f / 255f);
                }
                else if (this.currentHitNumber == 4)
                {
                    this.sp.color = new Color(105f / 255f, 105f / 255f, 105f / 255f);
                }
            }
        }
    }

}
