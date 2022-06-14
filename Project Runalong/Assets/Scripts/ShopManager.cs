using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //create an array of cosmetics
    public int currentSpriteButton;
    public Button[] spriteButtons;

    // Start is called before the first frame update
    void Start()
    {
        //Code may not work for level selection, but may be used for skins or cosmetics
        foreach (Button button in spriteButtons)
        {
            currentSpriteButton = PlayerPrefs.GetInt("SpriteUnlocked", 0);
            button.enabled = false;

            spriteButtons[currentSpriteButton].enabled = true;
        }
    }

    public void TryBuy()
    {

    }
}
