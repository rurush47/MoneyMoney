using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    SceneManager sceneManager;

    void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();
    }

	void OnMouseDown()
    {
        sceneManager.StartGame();
    }
}
