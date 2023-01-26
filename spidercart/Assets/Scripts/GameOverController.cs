using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private PlayerDataContainer playerDataContainer = null;
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
        }
#endif
        SetText();
    }

    private void SetText(){
        //Set Place Text
        int place = playerDataContainer.PlayerPlace;
        if(place <= 1){
            placeText.text = "YOU HAVE WON!!! Place: " + place.ToString();
        }else{
            placeText.text = "Place: " + place.ToString();
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
    }

    void OnDestroy(){
        Destroy(playerDataContainer.gameObject);
    }
}
