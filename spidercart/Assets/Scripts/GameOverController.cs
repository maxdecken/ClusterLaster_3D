using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    private PlayerDataContainer playerDataContainer = null;
    [SerializeField] private GameObject defaultSpiderPiggy = null;
    [SerializeField] private GameObject evilSpiderPiggy = null;
    private GameObject currentSpiderPiggy = null;
    [SerializeField] private TMP_Text currentTimeText = null;
    [SerializeField] private TMP_Text bestTimeText = null;
    [SerializeField] private TMP_Text placeText = null;

    // Start is called before the first frame update
    void Start()
    {
        playerDataContainer = (PlayerDataContainer) GameObject.FindObjectOfType(typeof(PlayerDataContainer));

#if UNITY_EDITOR
        // For Debug (Testing the Scene w/o context) only!
        if(playerDataContainer == null){
            Debug.Log("No PlayerDataContainer found: Creating new one!");
            // New playerDataContainer with default settings
            GameObject playerDataContainerGameObject = new GameObject("PlayerDataContainer");
            playerDataContainerGameObject.gameObject.AddComponent<PlayerDataContainer>();
            playerDataContainerGameObject.AddComponent<DontDestroy>();

            playerDataContainer = playerDataContainerGameObject.GetComponent<PlayerDataContainer>();
            playerDataContainer.playerCharacterType = PlayerDataContainer.CharacterType.SpiderPiggyEvil;
            playerDataContainer.PlayerPlace = 1;
            playerDataContainer.PlayerTime = 143.5f;
        }
#endif
        if(playerDataContainer.playerCharacterType == PlayerDataContainer.CharacterType.SpiderPiggyEvil){
            defaultSpiderPiggy.SetActive(false);
            evilSpiderPiggy.SetActive(true);
            currentSpiderPiggy = evilSpiderPiggy;
        }else{
            defaultSpiderPiggy.SetActive(true);
            evilSpiderPiggy.SetActive(false);
            currentSpiderPiggy = defaultSpiderPiggy;
        }
        SetText();
    }

    private void SetText(){
        //Set Place Text
        if(playerDataContainer.RaceFinished){
            int place = playerDataContainer.PlayerPlace;
            if(place <= 1){
                placeText.text = "YOU HAVE WON!!! Place: " + place.ToString();
                currentSpiderPiggy.GetComponent<Animator>().SetBool("isCheeringStanding", true);
            }else{
                placeText.text = "Place: " + place.ToString();
                currentSpiderPiggy.GetComponent<Animator>().SetBool("isDefeatedStanding", true);
            }
            //Help for formatting from here: https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
            int minutesCurrent = Mathf.FloorToInt((float) playerDataContainer.PlayerTime / 60f);
            int secondsCurrent = Mathf.FloorToInt((float) (playerDataContainer.PlayerTime - minutesCurrent * 60));
            string timeTextCurrent = string.Format("{0:0}:{1:00}", minutesCurrent, secondsCurrent);

            currentTimeText.text = "New Time: " + timeTextCurrent;

            //If no HighScoreFile exists, create a new one
            if(!PlayerPrefs.HasKey("bestTime")){
                PlayerPrefs.SetFloat("bestTime", 0);
            }

            //If a new best time was set, or best time is the same as current time
            float bestTimeEver = PlayerPrefs.GetFloat("bestTime");
            if(bestTimeEver <= playerDataContainer.PlayerTime){
                bestTimeText.text = "New Best-Time!!!";
                PlayerPrefs.SetFloat("bestTime", (float) playerDataContainer.PlayerTime);
            }else{
                int minutesBest = Mathf.FloorToInt((float) bestTimeEver / 60f);
                int secondsBest = Mathf.FloorToInt((float) (bestTimeEver - minutesBest * 60));
                string timeTextBest = string.Format("{0:0}:{1:00}", minutesBest, secondsBest);
                bestTimeText.text = "Best Time Ever: " + timeTextBest;
            }
        }else{
            placeText.text = "YOU LEFT THE GAME";
            currentTimeText.text = "";
            bestTimeText.text = "";
            currentSpiderPiggy.GetComponent<Animator>().SetBool("isDefeatedStanding", true);
        }
    }

    void OnDestroy(){
        Destroy(playerDataContainer.gameObject);
    }
}
