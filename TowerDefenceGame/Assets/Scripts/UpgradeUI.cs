using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeUI : MonoBehaviour
{

    public GameObject upgradePanel;
    public TextMeshProUGUI[] upgradeNames;
    public TextMeshProUGUI[] upgradeLevels;
    public TextMeshProUGUI[] costs;
    private void Start()
    {
        upgradePanel.SetActive(false);

    }
    public void TogglePanel(bool toggle)
    {
        upgradePanel.SetActive(toggle);
    }

    public void UpdateUpgradePanel(string name1, string name2, int level1, int level2, int cost, int cost2)
    {
        upgradeNames[0].text = name1;
        upgradeLevels[0].text = "Level " + level1.ToString();
        costs[0].text = cost.ToString();
        upgradeNames[1].text = name2;
        upgradeLevels[1].text = "Level " + level2.ToString();
        costs[1].text = cost2.ToString();
    }
}
