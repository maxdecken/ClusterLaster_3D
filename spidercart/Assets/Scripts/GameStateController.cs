using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GameStateController : MonoBehaviour
{
    //[SerializeField] private Camera_third_person thirdPersonCam = null;
    [SerializeField] private GameObject play_canvas = null;
    [SerializeField] private GameObject pause_canvas = null;
    [SerializeField] public PlayerController player = null;
    [SerializeField] private PlayerDataContainer playerDataContainer = null;
    [SerializeField] private TMP_Text countdownText = null;
    [SerializeField] private TMP_Text timeText = null;
    [SerializeField] private TMP_Text positionText = null;
    [SerializeField] private SceneLoader gameOverLoader = null;
    [SerializeField] private double startPlayerTime = 0f;
    private bool raceStarted = false;
    private bool raceOver = false;
    public int place = 0;

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

        play_canvas.SetActive(true);
        pause_canvas.SetActive(false);

        // MULTIPLAYER
        // START WORK OF MULTIPLAYER GAME MANAGER
        PunGameManager.Instance.initiatePlayers();

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

    public void SetPlayerController(PlayerController playerController){
        player = playerController;
        //Debug.Log("Setting lookAt");
        //foreach(Transform child in transform){
        //    if(child.gameObject.name == "lookAt"){
        //        Debug.Log("Found lookAt");
        //        thirdPersonCam.lookAt = child;
        //    }
        //}
    }

    // Help for Pause from here: https://gamedevbeginner.com/the-right-way-to-pause-the-game-in-unity/
    public void PauseGame ()
    {
        //Time.timeScale = 0;
        play_canvas.SetActive(false);
        pause_canvas.SetActive(true);
    }

    public void ResumeGame ()
    {
        //Time.timeScale = 1;
        play_canvas.SetActive(true);
        pause_canvas.SetActive(false);
    }

    public void Respawn()
    {
        //Time.timeScale = 1;
        play_canvas.SetActive(true);
        pause_canvas.SetActive(false);
        player.respawn();
    }

    public void OnExitInPause(){
        PhotonNetwork.Disconnect();
        //Time.timeScale = 1;  
    }

    IEnumerator StartRace(){
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Start";

        //player.controllsAllowed = true;
        raceStarted = true;
        startPlayerTime = Time.timeAsDouble;

        yield return new WaitForSeconds(3f);
        countdownText.text = "";

        // Set Name of all Players
        // Todo Find a better way than here
        // Problem: In PunGameManager I dont have an event thats called when a prefab is initiated
        // Here I am sure, that all the player prefabs are initiated
        //Dictionary<int, Photon.Realtime.Player> pList = PhotonNetwork.CurrentRoom.Players;
        //foreach (KeyValuePair<int, Photon.Realtime.Player> p in pList) {
        //    string kartName = "Kart_" + p.Value.NickName;
        //    GameObject.Find(kartName).transform.Find("PlayerTag").GetComponent<TMP_Text>().text = p.Value.NickName;
        //}
    }

    public bool IsRaceStarted(){
        return raceStarted;
    }
    public void RaceFinished(){
        StartCoroutine(RaceOverSequence());
    }

    IEnumerator RaceOverSequence(){
        player.raceFinished = true;
        PunGameData gameData = GameObject.FindObjectOfType<PunGameData>();
        gameData.PlayerFinishedRace();
        raceOver = true;
        timeText.text = "";
        double currentPlayerTime = Time.timeAsDouble;
        double timeDelta = currentPlayerTime - startPlayerTime;
        playerDataContainer.PlayerTime = timeDelta;
        playerDataContainer.PlayerPlace = place;
        playerDataContainer.RaceFinished = true;
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LeaveRoom();
    }

    public void SetPlaceText(){
        positionText.text = "Place: " + place.ToString();
    }
}
