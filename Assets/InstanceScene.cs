using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstanceScene : MonoBehaviour
{
    public string targetScene;

    public GameObject XROrigin;

    public GameObject LobbyRoom;

    public void LoadCoralScene(string scenePath) {
        targetScene = scenePath;
        SceneManager.LoadScene(targetScene, LoadSceneMode.Additive);
    }

    private void Update() {
        if (XROrigin.GetComponent<Transform>().position.x > 5) {
            LobbyRoom.SetActive(false);
        } else {
            LobbyRoom.SetActive(true);
        }

    }

}
