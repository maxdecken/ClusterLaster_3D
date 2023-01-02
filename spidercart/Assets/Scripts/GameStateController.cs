using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    [SerializeField] private TMP_Text countdownText = null;
    [SerializeField] private SceneLoader gameOverLoader = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartRace());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartRace(){
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);
        countdownText.text = "Start";
        player.controllsAllowed = true;
        yield return new WaitForSeconds(3f);
        countdownText.text = "";
    }
    public void RaceFinished(){
        StartCoroutine(RaceOverSequence());
    }

    IEnumerator RaceOverSequence(){
        yield return new WaitForSeconds(3f);
        gameOverLoader.LoadScene();
    }
}
