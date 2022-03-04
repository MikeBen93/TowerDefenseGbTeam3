using UnityEngine;
using UnityEngine.UI;

public class CrystalUI : MonoBehaviour
{
    public Text crystalText;
    private void Update()
    {
        crystalText.text = PlayerStats.Crystals.ToString();
    }
}

