using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public delegate void InitDelegate();
    public static event InitDelegate InitEvent;
    public static void DoInit()
    {
        InitEvent();
    }
    public bool mine;
    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        mine = Random.value < 0.15;
        var x = (int)transform.position.x;
        var y = (int)transform.position.y;
        Grid.elements[x, y] = this;
        // LoadTexture(1);
    }
    void Init()
    {

    }
    public void LoadTexture(int adjacentCount)
    {
        if (mine)
        { SpriteRenderer.sprite = mineTexture; }
        else
        {
            SpriteRenderer.sprite = emptyTextures[adjacentCount];
        }
    }
    public bool isCovered()
    {
        return SpriteRenderer.sprite.texture.name == "default";
    }
    void OnMouseUpAsButton()
    {
        if (GameManager.curState == State.Begin)
        {
            if (mine)
            {
                Grid.UncoverMines();
                GameManager.curState = State.GameOver;
                print("game over");
            }
            else
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                LoadTexture(Grid.AdjacentMines(x, y));

                Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);

                if (Grid.isFinished())
                    GameManager.curState = State.Win;
                //    print("you win");
            }
        }
    }
    // void OnMouseOver()
    // {
    //      SpriteRenderer.sprite
    // }

}
