using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    

    public static void ChangeLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
