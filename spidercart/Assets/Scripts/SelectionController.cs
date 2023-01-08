using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [SerializeField] private PlayerDataContainer playerDataContainer = null;

    void Start()
    {
        playerDataContainer = (PlayerDataContainer) GameObject.FindObjectOfType(typeof(PlayerDataContainer));
    }

    public void SetCharacter(string characterTypeName){
        Debug.Log("Selected: " + characterTypeName);
        if(characterTypeName == "SpiderPiggyEvil"){
            playerDataContainer.playerCharacterType = PlayerDataContainer.CharacterType.SpiderPiggyEvil;
        }else{
            playerDataContainer.playerCharacterType = PlayerDataContainer.CharacterType.SpiderPiggyDefault;
        }
    }
}
