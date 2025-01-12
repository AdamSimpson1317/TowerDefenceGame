using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeTower : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject upgradePicture;
    public UpgradeUI upgradeUI;
    public TextMeshProUGUI upgradeText1;
    public TextMeshProUGUI levelText1;
    public TextMeshProUGUI costText1;
    public TextMeshProUGUI upgradeText2;
    public TextMeshProUGUI levelText2;
    public TextMeshProUGUI costText2;
    public TowerShooting towerShooting;
    public TowerType towerType;
    
    public bool IsBought1;
    public bool IsBought2;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        upgradeUI = gameManager.GetComponent<UpgradeUI>();
        upgradeText1 = upgradeUI.upgradeNames[0];
        upgradeText2 = upgradeUI.upgradeNames[1];
        levelText1 = upgradeUI.upgradeLevels[0];
        levelText2 = upgradeUI.upgradeLevels[1];
        costText1 = upgradeUI.costs[0];
        costText2 = upgradeUI.costs[1];
        //upgradePicture =  
        towerType = towerShooting.towerType;
        IsBought1 = false;
        IsBought2 = false;
    }

    void Update()
    {
        if(towerType.ToString() == "Archer")
        {
            ArrowUpgrades();
        }
        else if(towerType.ToString() == "Infantry")
        {
            InfantryUpgrades();
        }
        else if(towerType.ToString() == "Wizard")
        {
            WizardUpgrades();
        }
        else if(towerType.ToString() == "CrystalMine")
        {
            CrystalMineUpgrades();
        }
    }

    //Arrow Upgrade 1 is Larger Range
    //Arrow Upgrade 2 is Faster Fire
    void ArrowUpgrades()
    {
        if(IsBought1 == true)
        {
            towerShooting.range *= 2;
        }

        if(IsBought2 == true)
        {
            towerShooting.fireRate *= 2;
        }
    }
    //Infantry Upgrade 1 is Larger Range
    //Infantry Upgrade 2 is Stays Longer
    void InfantryUpgrades()
    {
        if(IsBought1 == true)
        {
            towerShooting.range *= 2;
        }

        if(IsBought2 == true)
        {
            towerShooting.destroyTimer *= 2;
        }
    }
    //Wizard Upgrade 1 is Larger Blast Radius 
    //Wizard Upgrade 2 is Longer Blast
    void WizardUpgrades()
    {
        if(IsBought1 == true)
        {
            towerShooting.projectileSizeMultiplier *= 2;
        }

        if(IsBought2 == true)
        {
            towerShooting.destroyTimer *= 2;
        }
    }
    //CrystalMine Upgrade 1 is more money per round
    //CrystalMine Upgrade 2 is more money per kill
    void CrystalMineUpgrades()
    {
        if(IsBought1 == true)
        {
            
        }

        if(IsBought2 == true)
        {
            
        }
    }

    public void BuyFirstUpgrade()
    {
        IsBought1 = true;
    }
    public void BuySecondUpgrade()
    {
        IsBought2 = true;
    }



}
