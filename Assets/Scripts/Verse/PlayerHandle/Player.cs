using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Player(string name)
    {
        this.NamePlayer = name;
    }
    string NamePlayer;

    GameObject playerObj;
    Rigidbody2D rg;
    SpriteRenderer spRender;

    const float movement = 5f;

    Vector3 moveDir;
    Vector3 lastMoveDir;
    float moveX,moveY;

    float spaceTime = 0.4f;
    bool isDash = false;
    bool startCount = false;

    Mouse mouse;
    
    public void RegisterPlayerToWorld()
    {
        playerObj = new GameObject(NamePlayer);
        spRender = playerObj.AddComponent<SpriteRenderer>();

        Texture2D tex = FindContent<Texture2D>.ListAllContent["Circle.png"].t;
        spRender.sprite = Sprite.Create(tex,new Rect(0,0,tex.width,tex.height),new Vector2(0.5f,0.5f),256);

        
        rg = playerObj.AddComponent<Rigidbody2D>(); 
        rg.gravityScale = 0;


        
        Current.CameraFollow.target = playerObj.transform;
        new InputData(KeyCode.B,EventType.KeyDown,delegate {Mouse.GetMouse.ChangeModeTo(Mouse.MouseMode.Build);});
        new InputData(KeyCode.V,EventType.KeyDown,delegate {Mouse.GetMouse.ChangeModeTo(Mouse.MouseMode.ViewStragety);});
        mouse = new Mouse();
    }
     
    public void UpdatePlayer()
    {
        mouse.MouseUpdate();
        if(Input.GetKey(KeyCode.W))
        {
        moveY +=1;
        }
        if(Input.GetKey(KeyCode.S))
        {
        moveY -=1;
        }
        if(Input.GetKey(KeyCode.A))
        {
        moveX -=1;
        }
        if(Input.GetKey(KeyCode.D))
        {
        moveX +=1;
        }
        moveDir = new Vector3(moveX,moveY).normalized;
        if(moveX!=0||moveY!=0)
        {
            lastMoveDir = moveDir;
        }
         
        if (Input.GetKeyDown(KeyCode.Space)&&!isDash)
        {
            if (startCount)
            {
                isDash = true;
                startCount = false;
            }
            else
            {
                startCount = true;
                spaceTime = 0.4f;
                
            }
        }
        if (startCount)
        {
            spaceTime = spaceTime - Time.deltaTime;
            if (spaceTime <= 0)
            {
                startCount = false; 
            }

        }
        moveX =0;moveY =0;
    }
    public void FixedUpdatePlayer()
    {
         
        rg.velocity = moveDir * movement;
         if(isDash)
         { 
             isDash = false;
             float dashAmount = 1.1f;
             rg.MovePosition(playerObj.transform.position + lastMoveDir*dashAmount);
         }
        //rg.transform.Translate(moveDir*movement);
    }
    public void UpdatePlayerOnGUI()
    {
         
    }
}
