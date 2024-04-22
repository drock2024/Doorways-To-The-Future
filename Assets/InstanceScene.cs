using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstanceScene : MonoBehaviour
{
    public string targetScene;

    public GameObject XROrigin;

    public GameObject LobbyRoom;

    public GameObject Door;

    public void LoadCoralScene(string scenePath) {
        targetScene = scenePath;
        SceneManager.LoadScene(targetScene, LoadSceneMode.Additive);
    }

    private void Update() {
        float checkpoint_pos = Door.GetComponent<Transform>().position.x + 1.5f;
        if (XROrigin.GetComponent<Transform>().position.x > checkpoint_pos) {
            LobbyRoom.SetActive(false);
        } else {
            LobbyRoom.SetActive(true);
        }

    }

}
