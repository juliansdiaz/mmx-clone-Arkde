using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Variables
    [SerializeField] AudioClip sfx_play;
    AudioSource menuAudio;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        menuAudio = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        //Removes any extra LevelManager objects in the scene
        int lvlManagers = FindObjectsOfType<LevelManager>().Length;
        if(lvlManagers > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void PlayGame()
    {
        menuAudio.PlayOneShot(sfx_play);
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }
}
