using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace  BrushingLine
{
    

public class UIManager : MonoBehaviour
{
    
    [SerializeField]private Text levelText;
    [SerializeField]private GameObject mainMenu, completeMenu;
    [SerializeField]private Button nextButton,retryButton;
    
    
    
    public Text LevelText
    {
        get => levelText;
    }

    private void Start()
    {
        nextButton.onClick.AddListener(() => OnClick(nextButton ));
        retryButton.onClick.AddListener(() => OnClick(retryButton ));
    }

    void OnClick(Button btn)
    {
        switch (btn.name)
        {
            case "NextButton" :
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "RetryButton":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
        {
            
        }
    }

    public void LevelComplete()
    {
        completeMenu.SetActive(true);
        mainMenu.SetActive(false);
       
        
    }
    
    
}
}