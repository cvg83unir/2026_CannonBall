using UnityEngine;

public class TeleportWindmill : MonoBehaviour
{
    [SerializeField] float minimumXValue = -3.46f;
    [SerializeField] float maximumXValue = 4.92f;
    [SerializeField] float minimumYValue = -3.55f;
    [SerializeField] float maximumYValue = 3.41f;

    [SerializeField] float timeToTeletransport = 60f;

    [SerializeField] float radiusCheckWindmillNear = 1f;
    [SerializeField] LayerMask windmillsLayerMask = Physics2D.DefaultRaycastLayers;
    private float currentTimeToTeletransport = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.currentTimeToTeletransport += Time.deltaTime;
        //Debug.Log("Tiempo a teletransportar: " + this.currentTimeToTeletransport);

        if(this.currentTimeToTeletransport >= this.timeToTeletransport)
        {
            SetNewPosition();
            //Reseteamos la variable del tiempo actual que llevamos para volver a teletransportar
            this.currentTimeToTeletransport = 0f;
        }
    }

    private void SetNewPosition()
    {
        float newPositionX = Random.Range(this.minimumXValue, this.maximumXValue);
        float newPositionY = Random.Range(this.minimumYValue, this.maximumYValue);

        Vector2 newPosition = new Vector2(newPositionX, newPositionY);

        //miramos si el molino se va a teletransportar a una posición donde ya hubiera otro molino
        if (CheckAnotherWindmillClose(newPosition))
        {
            //Sí, hay otro cerca, no podemos mover el molino ahí, así que llamamos recursivamente a esta misma función:
            //Debug.Log("Hay otro molino cerca. X = " + newPositionX + " Y = " + newPositionY);
            SetNewPosition();
        }
        else
        {
            //Debug.Log("TELETRANSPORTE. NUEVA POSICIÓN. X = " + newPositionX + " Y = " + newPositionY);
            this.transform.position = newPosition;
        }


            
    }

    private bool CheckAnotherWindmillClose(Vector2 positionToTeleport)
    {
        Collider2D windmillClose = Physics2D.OverlapCircle(positionToTeleport, this.radiusCheckWindmillNear, this.windmillsLayerMask);

        return !(windmillClose == null);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transformTest.position, this.radiusCheckWindmillNear);
    //}
}
