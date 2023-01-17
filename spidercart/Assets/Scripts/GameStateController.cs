using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private PlayerDataContainer playerDataContainer = null;
    [SerializeField] private GameObject kart_default = null;
    [SerializeField] private GameObject kart_evil = null;
    [SerializeField] private TMP_Text countdownText = null;
    [SerializeField] private TMP_Text timeText = null;
    [SerializeField] private SceneLoader gameOverLoader = null;
    [SerializeField] private double startPlayerTime = 0f;
    //SpiderPiggy Object to Change material on
    [SerializeField] private GameObject spiderPiggyCharacter = null;
    //Materials for differnt Piggys
    [SerializeField] private Material m_defaultPiggyMaterial = null;
    [SerializeField] private Material m_evilPiggyMaterial = null;
    private bool raceOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerDataContainer = (PlayerDataContainer) GameObject.FindObjectOfType(typeof(PlayerDataContainer));
        if(playerDataContainer == null){
            Debug.Log("No PlayerDataContainer found: Creating new one!");
            // New playerDataContainer with default settings
            GameObject playerDataContainerGameObject = new GameObject("PlayerDataContainer");
            playerDataContainerGameObject.gameObject.AddComponent<PlayerDataContainer>();
            playerDataContainerGameObject.AddComponent<DontDestroy>();

            playerDataContainer = playerDataContainerGameObject.GetComponent<PlayerDataContainer>();
        }

        //Set right Material for selected piggy
        if(playerDataContainer.playerCharacterType == PlayerDataContainer.CharacterType.SpiderPiggyDefault){
            spiderPiggyCharacter.GetComponent<Renderer>().material = m_defaultPiggyMaterial;
            kart_default.SetActive(true);
            kart_evil.SetActive(false);
        }else if(playerDataContainer.playerCharacterType == PlayerDataContainer.CharacterType.SpiderPiggyEvil){
            spiderPiggyCharacter.GetComponent<Renderer>().material = m_evilPiggyMaterial;
            kart_default.SetActive(false);
            kart_evil.SetActive(true);
        }

        StartCoroutine(StartRace());
    }

    // Update is called once per frame
    void Update()
    {
        double currentPlayerTime = Time.timeAsDouble;
        double timeDelta = currentPlayerTime - startPlayerTime;
        if(timeDelta > 0 && player.controllsAllowed && !raceOver){
            //Help for formatting from here: https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html
            int minutes = Mathf.FloorToInt((float) timeDelta / 60f);
            int seconds = Mathf.FloorToInt((float) (timeDelta - minutes * 60));
            string timeTextContent = string.Format("{0:0}:{1:00}", minutes, seconds);
            timeText.text = timeTextContent;
        }
    }

    IEnumerator StartRace(){
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Start";

        player.controllsAllowed = true;
        startPlayerTime = Time.timeAsDouble;

        yield return new WaitForSeconds(3f);
        countdownText.text = "";
    }
    public void RaceFinished(){
        StartCoroutine(RaceOverSequence());
    }

    IEnumerator RaceOverSequence(){
        raceOver = true;
        timeText.text = "";
        double currentPlayerTime = Time.timeAsDouble;
        double timeDelta = currentPlayerTime - startPlayerTime;
        playerDataContainer.PlayerTime = timeDelta;
        yield return new WaitForSeconds(3f);
        gameOverLoader.LoadScene();
    }
}
