using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Text livesText;

    private void Update()
    {
        livesText.text = PlayerStats.GetRemainPercatnage().ToString() + "%";
    }
}
