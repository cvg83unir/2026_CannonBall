using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public class Globals : MonoBehaviour
{
    public static int currentNumberOfTargets = 0;
    public static int currentNumberOfWindMills = 3;
    public static int currentPlayerNumber = 1;

    public static void ResetValuesForNextPlayer()
    {
        currentNumberOfTargets = 0;
        currentNumberOfWindMills = 3;
        currentPlayerNumber++;
    }

    public static void ResetValuesForFirstPlayer()
    {
        currentNumberOfTargets = 0;
        currentNumberOfWindMills = 3;
        currentPlayerNumber=1;
    }


}
