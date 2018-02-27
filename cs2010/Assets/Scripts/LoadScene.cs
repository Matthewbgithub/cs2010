using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;


public class LoadScene : MonoBehaviour
{

    public static int size = 19;

    public void Grid9()
    {
        size = 9;
        SceneLoader();

    }

    public void Grid13()
    {
        size = 13;
        SceneLoader();
    }

    public void Grid19()
    {
        SceneLoader();
    }

    private void SceneLoader()
    {
        LoadGame(1);
    }

    private void LoadGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;


        while (!operation.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            if (operation.progress == 0.9f)
            {
                Debug.Log("Smash space to smash");
                if (Input.GetKeyDown(KeyCode.Space))
                    operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
