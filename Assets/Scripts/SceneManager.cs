using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    static public float gameSpeed = 0.5f;
    static public int numberOfPiggies = 3;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        Application.LoadLevel("Scene1");
    }
}
