using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    public void LoadNextScene() {
        SceneController.LoadScene(SceneController.Scene.MainScene);
    }

    public void Quit() {
        Application.Quit();
    }

    public void LoadMainMenu() {
        SceneController.LoadScene(SceneController.Scene.MainMenu);
    }
}
