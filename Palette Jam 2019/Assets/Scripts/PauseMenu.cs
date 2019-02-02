using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            //if (Input.GetKeyDown(KeyCode.Escape))
            if (Input.GetButtonDown("Pause"))
                GameManager.Instance.UnPause();
        }
    }
}
