using UnityEngine;
using System.Collections;

public class TargetGenerator : MonoBehaviour
{
    [Header("TargetsConfiguration")]
    [SerializeField] float numberOfTargetsInScreen = 3f;
    [SerializeField] float timeBetweenNewGeneration = 1f;
    [SerializeField] GameObject targetPrefab;

    [Header("SpawnPointsRange")]
    [SerializeField] Transform top;
    [SerializeField] Transform bottom;
    [SerializeField] float radiusCheckTargetNear = 1f;
    [SerializeField] LayerMask targetsLayerMask = Physics2D.DefaultRaycastLayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GenerateTargets());
    }

    IEnumerator GenerateTargets()
    {
        while (true)
        {
            //Si no hay suficientes dianas, creamos más:
            if (Globals.currentNumberOfTargets < this.numberOfTargetsInScreen)
            {
                Vector2 positionToInstantiate = Vector2.Lerp(top.position, bottom.position, Random.Range(0f, 1f));

                //Comprobamos que no haya otra diana cerca, sólo instanciaremos otra diana si no hay otra cerca:
                if (!CheckAnotherTargetClose(positionToInstantiate))
                {
                    Instantiate(targetPrefab, positionToInstantiate, Quaternion.identity);
                    Globals.currentNumberOfTargets++;
                    Debug.Log("+Número de dianas: " + Globals.currentNumberOfTargets.ToString());
                }
                else
                {
                    Debug.Log("Había otra diana cerca, hay que volver a intentarlo");
                }

            }

            //Esperamos hasta la siguiente vez
            yield return new WaitForSeconds(this.timeBetweenNewGeneration);

        }
    }

    private bool CheckAnotherTargetClose(Vector2 positionToInstantiate)
    {
        Collider2D targetClose = Physics2D.OverlapCircle(positionToInstantiate, this.radiusCheckTargetNear, this.targetsLayerMask);

        return !(targetClose == null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(top.position, this.radiusCheckTargetNear);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
