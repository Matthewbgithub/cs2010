using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadScene : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI loadText;

    //Standard board size
    public static int size = 9;

    //State which safe file to load (1-3)
    public static int LoadFromSaveFile = -1;

    public void LoadSaveFile(int fileNumber)
    {
        LoadFromSaveFile = fileNumber;
        LoadGame(1);
    }

    public void Grid9()
    {
        size = 9;
        LoadGame(1);
    }

    public void Grid13()
    {
        size = 13;
        LoadGame(1);
    }

    public void Grid19()
    {
		size = 19;
        LoadGame(1);
    }

    public void BlitzGame(){
        GoBoard.blitzMode = true;
		size = 13;
        LoadGame(1);
    }

    public void TutorialGame(){
        GoBoard.tutMode = true;
        LoadGame(1);
    }

    public static void ResetSaveFileLoad()
    {
        LoadFromSaveFile = -1;
    }

    private void LoadGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            loadText.text = "Loading progress: " + (progress * 100) + "%";
            if (operation.progress == 0.9f)
            {
                loadText.text = "Smash space to smash";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }


    }

}
