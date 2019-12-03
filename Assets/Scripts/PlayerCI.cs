using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public enum DIRECTION {  NONE, UP, DOWN, LEFT, RIGHT }


public class PlayerCI : MonoBehaviour
{
    public enum GENDER { MALE, FEMALE }
    
    private bool moving = false;
    private int speed = 5, buttonCooldown = 0;
    private DIRECTION dir = 0;
    private Vector3 pos;
    private GENDER gender;
    
    
    public int cooldown = 5, turns = 0, turnLimit = 200;
    public Text turnText, z;
    public bool dead = false;
    public Tilemap map;
    public Tilemap tilesType;
   // Start() var einhvern hluta vegna að ekki virka svo að ég notaði Awake() í staðin
    void Awake() 
    {   
        turnText.text = "Skref: " + turnLimit.ToString() + "/" + turns.ToString();
        gender = (GENDER)Random.Range(0,1);
        
         
    }
    void Update()
    {
       buttonCooldown--;
       // ef spilarin er ekki að hreyfast þá setja pos sama og staðsetnigu og hlaupa move()
        if (moving == false) 
        {

            pos = transform.position;
            inputD();
        }
        // ef spilarin er að hreyfast þá færa staðsetnigu nær pos
        if (moving == true) 
        {
            // ef pos sama er sama og staðsetnigu þá setja hreyfing á ósatt og hlaupa move()
            if (transform.position == pos)
            {
                moving = false;
                inputD();
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        }        
    }

    //Gáð hvort að cooldownið sé búið ef svo þá er gáð hvort að það séð búið að ýtta á áttartakka
    private void inputD() 
    {
        if (buttonCooldown <= 0) 
        {
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                direction(DIRECTION.UP, Vector3Int.up);
            }
            if (Input.GetKey("down") || Input.GetKey("s"))
            {
                direction(DIRECTION.DOWN, Vector3Int.down);
            }
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                direction(DIRECTION.RIGHT, Vector3Int.right);
            }
            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                direction(DIRECTION.LEFT, Vector3Int.left);
            }
        }
    }
    void direction(DIRECTION dire, Vector3Int d) 
    {
        // ef spilarinn efur ekki átt þá setja cooldown og átt
        if (dir != dire)
        {
            buttonCooldown = cooldown;
            dir = dire;
            
        }
        // Annars gá hvershvonar landslagi spilarinn er á
        else
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

            TileBase ground = map.GetTile(Vector3Int.RoundToInt(pos)+d);
            int modTurn = 1;
            if (ground == gFjall)
            {
                modTurn = 2;

            }
            if (ground == vatn || ground == vFjall)
            {
            }
            else
            {
                move(d,modTurn);
            }       
        }
    }
    //  hreyfa spilaran og telja skref
    void move(Vector3 d, int modTurn)
    {
        moving = true;
        pos += d;
        dir = 0;
        turns+=modTurn;
        turnText.text = "Skref: " + turnLimit.ToString() + "/" + turns.ToString();
        // ef skref er sama og skref takmörk þá setja dauður á satt
        if (turns >= turnLimit)
        {
            dead = true;
        }
    }
}
  