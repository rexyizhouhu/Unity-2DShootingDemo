using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (!LevelManager.instance)
        {
            LevelManager.instance = this;
        }
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        UIManager ui = GetComponent<UIManager>();
        if (ui) ui.ToggleDeathPanel();
    }


}
