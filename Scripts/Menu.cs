using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    [SerializeField]
    GameObject[] listOfLevels; 
    public int currentLevel = 0;
    public int destination = 0;
    public bool isMoving;
    public List<GameObject> levelMenuList;
    public Animator player;

    private void Start()
    {
        transform.position = listOfLevels[currentLevel].transform.position;
    }
    void Update()
    {
       

        if (isMoving) 
        {

            transform.position = Vector2.MoveTowards(transform.localPosition, listOfLevels[currentLevel].transform.position, Time.deltaTime * 5);

            if (Vector2.Distance(transform.position, listOfLevels[currentLevel].transform.position) < 0.01f) 
            {
                if (currentLevel == destination)
                {
                    isMoving = false;
                    player.Play("Player_idle");
                    Debug.Log("Idle started playing");
                }
                else if(currentLevel >= destination)
                {
                    currentLevel--;
                }
                else if (currentLevel <= destination)
                {
                    currentLevel++;
                }
                
            }
        }
    }
  


    public void ChangeDestination(int value)
    {
        if (value == destination && !isMoving)
        {
            Debug.Log("open level menu");
            levelMenuList[destination].SetActive(true);

        }
        else if (value != destination)
        {
            destination = value;
            isMoving = true;
            player.Play("Player_run");
            Debug.Log("run started playing");
        }

    }

}