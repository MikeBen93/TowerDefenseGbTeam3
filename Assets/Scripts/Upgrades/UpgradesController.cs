using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private ElectroTowerUpgrade electro;
    [SerializeField] private LaserTowerUpgrade laser;
    [SerializeField] private MagnitTowerUpgrade magnit;
    [SerializeField] private PlasmaTowerUpgrade plasma;

    private void Update()
    {
        electro.CheckButtonInteractiablitiy();
        laser.CheckButtonInteractiablitiy();
        magnit.CheckButtonInteractiablitiy();
        plasma.CheckButtonInteractiablitiy();
    }
}
