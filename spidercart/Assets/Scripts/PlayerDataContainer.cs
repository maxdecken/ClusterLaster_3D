using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataContainer : MonoBehaviour
{
    //Types of Character available as an Enum
    public enum CharacterType {SpiderPiggyDefault, SpiderPiggyEvil};

    //Selcted Character Type
    public CharacterType playerCharacterType { get; set; } = CharacterType.SpiderPiggyDefault;

    //Place to Store and Pass on the time of the player after the race
    public double PlayerTime { get; set; } = 0;

    //Place to Store and Pass on the place (muliplayer) of the player after the race
    public int PlayerPlace { get; set; } = 1; 

    //Set if race was completet or not
    public bool RaceFinished = false;
}
