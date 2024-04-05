using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    //Variables 
    [SerializeField] TMP_Text itemCountText;
    [SerializeField] GameObject gameOverObject;
    [SerializeField] TMP_Text gameOverTitle;

    int itemCount;
    LevelManager levelManager;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        itemCount = GameObject.FindGameObjectsWithTag("Item").Length;
        itemCountText.text = itemCount.ToString();
        gameOverObject.SetActive(false);
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void ReduceItemCount()
    {
        itemCount--;
        itemCountText.text = itemCount.ToString();

        if(itemCount < 1)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        gameOverTitle.text = "YOU WIN!";
        gameOverObject.SetActive(true);
        Time.timeScale = 0;
        player.isPaused = true;
    }

    public void LoseGame()
    {
        gameOverTitle.text = "YOU LOSE...";
        gameOverObject.SetActive(true);
    }

    public void ResetGame()
    {
        levelManager.PlayGame();
    }
}
