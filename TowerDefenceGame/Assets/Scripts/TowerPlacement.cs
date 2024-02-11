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

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            Debug.Log("Tile toiled");
        }
        else
        {
            Debug.Log("Not toiled");
        }

                



        Instantiate(towerPrefabs[0], pos, Quaternion.identity);

        yield return new WaitForSeconds(.5f);

        placing = false;
    }
}
