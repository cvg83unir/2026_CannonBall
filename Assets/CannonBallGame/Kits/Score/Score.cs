using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score = 0;
    TextMeshProUGUI textScore;

    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        this.textScore.text = $"Score: {score}";
    }

}
