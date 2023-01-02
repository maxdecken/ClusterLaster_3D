using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName = string.Empty;
    public void LoadScene(){
        SceneManager.LoadScene(sceneName);
    }
}
