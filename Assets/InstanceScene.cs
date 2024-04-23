using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstanceScene : MonoBehaviour
{
    public string targetScene;

    public GameObject XROrigin;

    public GameObject LobbyRoom;

    public GameObject SelectorPanel;

    public GameObject Door;

    bool haveLeft = false;

    public void LoadCoralScene(string scenePath) {
        if (SceneManager.sceneCount > 1) {
            Scene oldScene = SceneManager.GetSceneAt(1);
            SceneManager.UnloadSceneAsync(oldScene);
        }
        targetScene = scenePath;
        SceneManager.LoadScene(targetScene, LoadSceneMode.Additive);
    }

    private void Update() {
        float checkpoint_pos = Door.GetComponent<Transform>().position.x + 1.0f;
        if (XROrigin.GetComponent<Transform>().position.x > checkpoint_pos) {
            LobbyRoom.SetActive(false);
            SelectorPanel.SetActive(false);
            haveLeft = true;
        } else {
            LobbyRoom.SetActive(true);
            if (haveLeft) SelectorPanel.SetActive(true);
        }

    }

}
