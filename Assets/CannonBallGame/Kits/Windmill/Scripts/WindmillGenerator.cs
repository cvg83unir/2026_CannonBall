using UnityEngine;
using System.Collections;

public class WindmillGenerator : MonoBehaviour
{
    [Header("WindmillConfiguration")]
    [SerializeField] float numberOfWindmillInScreen = 3f;
    [SerializeField] float timeBetweenNewGeneration = 1f;
    [SerializeField] GameObject windmillPrefab;

    [Header("SpawnPointsRange")]
    [SerializeField] float minimumXValue = -3.46f;
    [SerializeField] float maximumXValue = 4.92f;
    [SerializeField] float minimumYValue = -3.55f;
    [SerializeField] float maximumYValue = 3.41f;

    [SerializeField] float radiusCheckTargetNear = 2f;
    [SerializeField] LayerMask windmillsLayerMask = Physics2D.DefaultRaycastLayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GenerateWindmills());
    }

    IEnumerator GenerateWindmills()
    {
        while (true)
        {
            //Si no hay suficientes dianas, creamos más:
            if (Globals.currentNumberOfWindMills < this.numberOfWindmillInScreen)
            {
                float newPositionX = Random.Range(this.minimumXValue, this.maximumXValue);
                float newPositionY = Random.Range(this.minimumYValue, this.maximumYValue);

                Vector2 positionToInstantiate = new Vector2(newPositionX, newPositionY);

                //Comprobamos que no haya otra diana cerca, sólo instanciaremos otra diana si no hay otra cerca:
                if (!CheckAnotherWindmillClose(positionToInstantiate))
                {
                    Instantiate(this.windmillPrefab, positionToInstantiate, Quaternion.identity);
                    Globals.currentNumberOfWindMills++;
                }
            }

            //Esperamos hasta la siguiente vez
            yield return new WaitForSeconds(this.timeBetweenNewGeneration);

        }
    }

    private bool CheckAnotherWindmillClose(Vector2 positionToInstantiate)
    {
        Collider2D windmillClose = Physics2D.OverlapCircle(positionToInstantiate, this.radiusCheckTargetNear, this.windmillsLayerMask);

        return !(windmillClose == null);
    }
}
