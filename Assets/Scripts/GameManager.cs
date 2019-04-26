using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject elementPrefab;
    public GameObject gridObj;
    public Camera mainCamera;
    public Button pauseBtn;
    public static State curState = State.Pause;
    public Text w;
    // public Text h;
    public Text textcurState;
    public Text textTimer;
    private float timer;
    public Sprite tagSprite;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        textcurState.text = curState.ToString();
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit=Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero);
            if (hit.collider)
            {
                if(hit.transform.tag=="Element")
                {
                    if(hit.transform.GetComponent<Element>().SpriteRenderer.sprite.name!="Tag")
                    hit.transform.GetComponent<Element>().SpriteRenderer.sprite=tagSprite;
                    else
                    {
                           hit.transform.GetComponent<Element>().SpriteRenderer.sprite=hit.transform.GetComponent<Element>().emptyTextures[9];
                    }
                }
                
            }
        }
    }
    public void BeginClick()
    {

        foreach (Transform child in gridObj.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (w.text == null)
            return;
        Grid.w = Convert.ToInt32(w.text);
        Grid.h = Convert.ToInt32(w.text);
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
        mainCamera.transform.position = new Vector3(Grid.w / 2, Grid.h / 2, -10);
        mainCamera.orthographicSize = Grid.w / 2 + 2;
        // Element.DoInit();
        pauseBtn.gameObject.SetActive(true);
        StartCoroutine(Timers(1));
        curState = State.Begin;
    }

    public void PauseClick()
    {
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (curState == 0)
        {
            curState = State.Pause;
            btn.GetComponentInChildren<Text>().text = "继续";
        }
        else if (curState == (State)3)
        {
            curState = State.Begin;
            btn.GetComponentInChildren<Text>().text = "暂停";
        }
    }



    IEnumerator Timers(int seconds)//rote为计时的间隔
    {

        while (true)
        {
            textTimer.text = timer.ToString();
            if (curState == State.Begin)
            {
                timer += seconds;

            }
            yield return new WaitForSeconds(seconds);
        }


    }


}
public enum State
{
    Begin = 0,
    GameOver = 1,
    Win = 2,
    Pause = 3

}
