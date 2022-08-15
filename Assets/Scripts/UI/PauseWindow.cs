using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    public void EnablePause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void DisablePause()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
