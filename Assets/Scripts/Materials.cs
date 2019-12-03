using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Materials : MonoBehaviour
{
    public Tilemap map;
    public Tilemap tilesType;
    public Text matur, steinn, vidur;

    private int mat = 0, stei = 0, vid = 0, buttonCooldown =0;
    private List<Vector3> flis =new List<Vector3>();



    void Update()
    {
        buttonCooldown --;
        if (buttonCooldown <= 0)
        {
            if (Input.GetKeyDown("z") || Input.GetKeyDown("e"))
            {
                bool tomt = false;
                // gá hvort playerinn hefur safnað efni á flísuni áður
                foreach(Vector3 i in flis)
                {
                    if (i == transform.position)
                    {
                        tomt = true;
                    }
                }
                // ef tomt er ekki rétt þá gá hverskonar flísu playerinn er á
                if (tomt == false)
                {
                    TileBase thorp = tilesType.GetTile(new Vector3Int(0,0,0));
                    TileBase grass = tilesType.GetTile(new Vector3Int(1,0,0));
                    TileBase vatn = tilesType.GetTile(new Vector3Int(1,-1,0));
                    TileBase sandur = tilesType.GetTile(new Vector3Int(0,-1,0));
                    TileBase mol = tilesType.GetTile(new Vector3Int(-1,-1,0));
                    TileBase sVant = tilesType.GetTile(new Vector3Int(-1,0,0));
                    TileBase gVant = tilesType.GetTile(new Vector3Int(-1,1,0));
                    TileBase vFjall = tilesType.GetTile(new Vector3Int(0,1,0));
                    TileBase gFjall = tilesType.GetTile(new Vector3Int(1,1,0));
                    TileBase gSkog = tilesType.GetTile(new Vector3Int(2,1,0));

                    TileBase ground = map.GetTile(Vector3Int.RoundToInt(transform.position));
                    if (ground == gFjall || ground == mol || ground == sandur)
                    {
                        stei +=1;
                        steinn.text = "Steinn: " + stei.ToString();
                        flis.Add(transform.position);
                    }
                    if (ground == grass || ground == sVant || ground == gVant)
                    {
                        mat +=1;
                        matur.text = "Matur: " +mat.ToString();
                        flis.Add(transform.position);
                    }
                    if (ground == gSkog)
                    {
                        vid +=1;
                        vidur.text = "Viður: " + vid.ToString();
                        flis.Add(transform.position);
                    }
                    buttonCooldown = 10;
                }
            }
        }
    }
}
