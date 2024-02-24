using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.InputSystem;

public class TowerPlacement : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int location;
    public GameObject[] towerPrefabs;
    public TowerType selectedTower;
    public int selectedTowerCost;
    public WorldMoney worldMoney;
    public Dictionary<Vector3, GameObject> existingTowers = new Dictionary<Vector3, GameObject>();

    public bool placing = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !placing)
        {
            StartCoroutine(TilePlace(selectedTower));
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTowerType(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTowerType(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetTowerType(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetTowerType(0);
        }
    }
    public void SetTowerType(int index)
    {
        if(index == 1)
        {
            selectedTower = TowerType.Archer;
            selectedTowerCost = 100;
        }
        else if (index == 2)
        {
            selectedTower = TowerType.Infantry;
            selectedTowerCost = 200;
        }
        else if (index == 3)
        {
            selectedTower = TowerType.Wizard;
            selectedTowerCost = 300;
        }
        else
        {
            selectedTower = TowerType.None;
            selectedTowerCost = 0;
        }

        
    }

    IEnumerator TilePlace(TowerType tower)
    {
        placing = true;

        //Get location to place tower (needs fixing)
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.x = Mathf.Round(mp.x * 2) / 2;
        mp.y = Mathf.Round(mp.y * 2) / 2;
        mp.z = 0;

        location = tilemap.WorldToCell(mp);
        Vector2 pos = tilemap.CellToWorld(location);

        if (tilemap.GetTile(location))
        {
            if (worldMoney.money >= selectedTowerCost)
            {
                if (existingTowers.ContainsKey(pos))
                {
                    Debug.Log("Tower already in this place!");
                }
                else
                {
                    if (tower == TowerType.Archer)
                    {
                        Debug.Log("placing");
                        GameObject newObj = Instantiate(towerPrefabs[0], pos, Quaternion.identity);
                        worldMoney.UpdateMoney(-selectedTowerCost);
                        existingTowers.Add(pos, newObj);
                    }
                    else if (tower == TowerType.Infantry)
                    {
                        GameObject newObj = Instantiate(towerPrefabs[1], pos, Quaternion.identity);
                        worldMoney.UpdateMoney(-selectedTowerCost);
                        existingTowers.Add(pos, newObj);
                    }
                    else if (tower == TowerType.Wizard)
                    {
                        GameObject newObj = Instantiate(towerPrefabs[2], pos, Quaternion.identity);
                        worldMoney.UpdateMoney(-selectedTowerCost);
                        existingTowers.Add(pos, newObj);
                    }
                    else
                    {
                        Debug.Log("No tower selected");

                    }

                }
            }


            yield return new WaitForSeconds(.5f);

            placing = false;
        }

    }

    public void CheckForTile()
    {
        if (tilemap.GetTile(location))
        {
            Debug.Log("");
        }
        else
        {
            Debug.Log("Not toiled");
        }
    }

    
}

public enum TowerType{
    None,
    Archer,
    Infantry,
    Wizard
}
