using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject elementPrefab;
    public GameObject gridObj;
    public static State curState = State.Puse;
    public Text w;
    public Text h;
    public Text textcurState;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textcurState.text = curState.ToString();
    }
    public void BeginClick()
    {

        foreach (Transform child in gridObj.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        Grid.w = Convert.ToInt32(w.text);
        Grid.h = Convert.ToInt32(h.text);
        Grid.elements = new Element[Grid.w, Grid.h];
        for (int i = 0; i < Grid.w; i++)
        {
            for (int j = 0; j < Grid.h; j++)
            {
                var elements = Instantiate(elementPrefab);
                elements.transform.position = new Vector3(j, i, 0);
                elements.transform.parent = gridObj.transform;
            }

        }

        // Element.DoInit();
        curState = State.Begin;
    }

}
public enum State
{
    Begin = 0,
    GameOver = 1,
    Win = 2,
    Puse = 3

}
