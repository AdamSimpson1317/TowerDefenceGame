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

    public bool placing = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !placing)
        {
            StartCoroutine(TilePlace());
        }
    }

    IEnumerator TilePlace()
    {
        Debug.Log("placing");
        placing = true;

        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.x = Mathf.Round(mp.x * 2) / 2;
        mp.y = Mathf.Round(mp.y * 2) / 2;
        mp.z = 0;

        location = tilemap.WorldToCell(mp);
        Vector2 pos = tilemap.CellToWorld(location);

        if (tilemap.GetTile(location))
        {
            Debug.Log(location.ToString() + " : " + mp.ToString());
            Instantiate(towerPrefabs[0], pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("not placed");

        }

                



        

        yield return new WaitForSeconds(.5f);

        placing = false;
    }
}
