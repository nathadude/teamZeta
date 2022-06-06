using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //create an array of levels (or cosmetics)
    public int currentLvlIndex;
    public Button[] lvlButtons;

    // Start is called before the first frame update
    void Start()
    {
        //Code may not work for level selection, but may be used for skins or cosmetics
        //foreach (Button button in lvlButtons)
        //{
        //    currentLvlIndex = PlayerPrefs.GetInt("LvlUnlocked", 1); //only supports one level at a time
        //    button.enabled = false;

        //    lvlButtons[currentLvlIndex].enabled = true;
        //}
    }

    public void TryBuy()
    {

    }
}
