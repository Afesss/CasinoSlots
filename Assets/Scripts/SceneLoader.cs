using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public const string Game = "Game";
    public const string Menu = "Menu";
    public static void LoadScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
