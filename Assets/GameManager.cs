using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] UnityEvent StartGame;
    [SerializeField] UnityEvent PauseGame;
    [SerializeField] UnityEvent GameOver;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void gameStart()
    {

    }

    public void gamePause()
    {

    }

    public void gameOver()
    {

    }
    
    public void CloseGame() 
    {
     Application.Quit();
    }
}
