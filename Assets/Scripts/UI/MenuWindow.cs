using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindow : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneLoader.LoadScene(SceneLoader.Menu);
    }


}
