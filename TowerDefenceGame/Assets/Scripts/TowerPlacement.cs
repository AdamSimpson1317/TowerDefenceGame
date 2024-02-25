using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEngine.InputSystem;

public enum TowerType{
    None,
    Archer,
    Infantry,
    Wizard,
    CrystalMine
}

public class TowerPlacement : MonoBehaviour
{
    
    public Tilemap tilemap;
    public Vector3Int location;
    public GameObject[] towerPrefabs;
    public int[] costs;
    public TowerType selectedTower;
    public int selectedTowerCost;
    public WorldMoney worldMoney;
    public Dictionary<Vector3, GameObject> existingTowers = new Dictionary<Vector3, GameObject>();
    public float placingCooldown;
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
            SetTowerType(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetTowerType(0);
        }
    }
    public void SetTowerType(int index)
    {
        if(index == 1)
        {
            selectedTower = TowerType.Archer;
            selectedTowerCost = costs[0];
        }
        else if (index == 2)
        {
            selectedTower = TowerType.Infantry;
            selectedTowerCost = costs[1];
        }
        else if (index == 3)
        {
            selectedTower = TowerType.Wizard;
            selectedTowerCost = costs[2];
        }
        else if (index == 4)
        {
            selectedTower = TowerType.CrystalMine;
            selectedTowerCost = costs[3];
        }
        else
        {
            selectedTower = TowerType.None;
            selectedTowerCost = 0;
        }

        
    }

    IEnumerator TilePlace(TowerType tower)
    {
        

        //Get location to place tower (needs fixing)
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mp.x = Mathf.Round(mp.x * 2) / 2;
        //mp.y = Mathf.Round(mp.y * 2) / 2;
        mp.z = 0;

        location = tilemap.WorldToCell(mp);
        Vector2 pos = tilemap.CellToWorld(location);
        //pos.y += 0.57735f;
        //pos.y += 0.57735f;
        pos.y += .44f;
        if (pos.x % 1 == 0)
        {
            //pos.y += .15f;
        }
        else if (pos.x % 1 == 0.5)
        {
            //pos.y -= .25f;
        }

        //If .5 (-.15) if whole +.15

        

        

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
                    placing = true;
                    Debug.Log("mp: " + mp);
                    Debug.Log("pos: " + pos);
                    if (tower == TowerType.Archer)
                    {
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
                    else if (tower == TowerType.CrystalMine)
                    {
                        GameObject newObj = Instantiate(towerPrefabs[3], pos, Quaternion.identity);
                        worldMoney.UpdateMoney(-selectedTowerCost);
                        existingTowers.Add(pos, newObj);
                    }
                    else
                    {
                        Debug.Log("No tower selected");
                        
                    }
                    yield return new WaitForSeconds(placingCooldown);

                    placing = false;

                }
            }


            
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


