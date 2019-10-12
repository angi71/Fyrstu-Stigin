using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text turnText;
    public bool dead = false;
   
   // Start() var einhvern hluta vegna að ekki virka svo að ég notaði Awake() í staðin
    void Awake() 
    {   
        turnText.text = turnLimit.ToString() + "/" + turns.ToString();
        gender = (GENDER)Random.Range(0,1);
         
    }
    void Update()
    {
       buttonCooldown--;
       // ef spilarin er ekki að hreyfast þá setja pos sama og staðsetnigu og hlaupa move()
        if (moving == false) 
        {

            pos = transform.position;
            move();
        }
        // ef spilarin er að hreyfast þá færa staðsetnigu nær pos
        if (moving == true) 
        {
            // ef pos sama er sama og staðsetnigu þá setja hreyfing á ósatt og hlaupa move()
            if (transform.position == pos)
            {
                moving = false;
                move();
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
        }        
    }

    //Gáð hvort að cooldownið sé búið ef svo þá er gáð hvort að það séð búið að ýtta á áttartakka
    private void move() 
    {
        if (buttonCooldown <= 0) 
        {
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                direction(DIRECTION.UP, Vector3.up);
            }
            if (Input.GetKey("down") || Input.GetKey("s"))
            {
                direction(DIRECTION.DOWN, Vector3.down);
            }
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                direction(DIRECTION.RIGHT, Vector3.right);
            }
            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                direction(DIRECTION.LEFT, Vector3.left);
            }
        }
    }
    void direction(DIRECTION dire, Vector3 d) 
    {
        // ef spilarinn efur ekki átt þá setja cooldown og átt
        if (dir != dire)
        {
            buttonCooldown = cooldown;
            dir = dire;
            
        }
        // Annars hreyfa spilaran og telja skref
        else
        {
            moving = true;
            pos += d;
            dir = 0;
            turns+=1;
            turnText.text = turnLimit.ToString() + "/" + turns.ToString();
            // ef skref er sama og skref takmörk þá setja dauður á satt
            if (turns == turnLimit)
            {
                dead = true;
            }
        }
    }
}
  