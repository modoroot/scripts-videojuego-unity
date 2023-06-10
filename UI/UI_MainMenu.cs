using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour {
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject loadGameButton;
    [SerializeField] UI_FadeScreen fadeScreen;

    private void Start() {
        if (SaveManager.instance.HasSaveData() == false) {
            continueButton.SetActive(false);
            loadGameButton.SetActive(false);
        }
    }

    public void ContinueGame() {
        StartCoroutine(LoadSceneFadeEffect(1.5f));
    }

    public void LoadGame() {

    }

    public void NewGame() {
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadSceneFadeEffect(1.5f));
    }

    public void ExitGame() {
        Debug.Log("Sale del juego");
        //Application.Quit();
    }

    IEnumerator LoadSceneFadeEffect(float _delay) {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
