using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CombatManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> comboList;
    

    [Header("Player")]
    public Animator player;
    public float playerMaxHP;
    public float playerHP;
    public Slider playerHPSlider;

    [Header("Enemy")]
    public Animator enemy;
    public float enemyMaxHP;
    public float enemyHP;
    public Slider enemyHPSlider;

    [Header("Menus")]
    public GameObject gameOver;
    public GameObject victory;
    public bool gameEnded = false;
    public AudioSource victoryMusic;
    public AudioSource defeatMusic;
    private void Start()
    {
        playerHP = playerMaxHP;
        playerHPSlider.maxValue = playerMaxHP;
        playerHPSlider.value = playerHP;
        

        enemyHP = enemyMaxHP;
        enemyHPSlider.maxValue = enemyMaxHP;
        enemyHPSlider.value = enemyHP;

    }

    private void Update()
    {
        if (comboList[0].GetComponent<Notes>().notesList.Count == 0 && comboList.Count > 1 && playerHP > 0)
        {
            //when the combolist.count checks for 0 unity gives a false error

            comboList.RemoveAt(0);
        }

        if (comboList.Count == 1 && comboList[0].GetComponent<Notes>().notesList.Count == 0)
        {
            //2nd part of the if statement solves a false error from appearing mentioned in the comment above
            enemyHP = 0;
        }

        enemyHPSlider.value = enemyHP;
        playerHPSlider.value = playerHP;

        if(playerHP <= 0)
        {
            gameOver.SetActive(true);
            if (!gameEnded)
            {
                defeatMusic.Play();
              
            }
            gameEnded = true;
            player.GetComponent<Animator>().Play("Player_death");
  
        }

        if (enemyHP <= 0 && playerHP != 0)
        {
            victory.SetActive(true);
            if(!gameEnded)
            {
                victoryMusic.Play();
        
            }
            gameEnded = true;
            enemy.GetComponent<Animator>().Play("Enemy_death");
            
        }
    }

    public void AnimationPlayer(int i)
    {
        if (i == 0)
        {
            float e;
            e = Random.Range(1, 3);
            if (e == 1)
            {
                enemy.GetComponent<Animator>().Play("Enemy_attack1", -1, 0f);
            }
            if (e == 2)
            {
                enemy.GetComponent<Animator>().Play("Enemy_attack2", -1, 0f);
            }
            if (e == 3)
            {
                enemy.GetComponent<Animator>().Play("Enemy_attack3", -1, 0f);
            }

            player.GetComponent<Animator>().Play("Player_block", -1, 0f);
        }

        if (i == 1)
        {
            //enemy random attack selection
            float e;
            e = Random.Range(1, 3);
            if (e == 1)
            {
                enemy.GetComponent<Animator>().Play("Enemy_attack1", -1, 0f);
            }
            if (e == 2)
            {
                enemy.GetComponent<Animator>().Play("Enemy_attack2", -1, 0f);
            }
            if (e == 3)
            {
                enemy.GetComponent<Animator>().Play("Enemy_attack3", -1, 0f);
            }

            //player hurt or death animation
            if (playerHP > 0)
            {
                player.GetComponent<Animator>().Play("Player_DMG", -1, 0f);
            }

        }


        if (i == 2)
        {
            //enemy getting hurt
            if (enemyHP > 0)
            {
                enemy.GetComponent<Animator>().Play("Enemy_DMG", -1, 0f);
            }


            //player random attack selection
            float a;
            a = Random.Range(1, 3);
            if (a == 1)
            {
                player.GetComponent<Animator>().Play("Player_attack1", -1, 0f);
            }
            if (a == 2)
            {
                player.GetComponent<Animator>().Play("Player_attack2", -1, 0f);
            }
            if (a == 3)
            {
                player.GetComponent<Animator>().Play("Player_attack3", -1, 0f);
            }
        }


    }

}
