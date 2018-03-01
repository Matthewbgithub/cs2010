using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameLoader : MonoBehaviour
{

    public void LoadGame(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {

        //AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //operation.allowSceneActivation = false;


        //while (!operation.isDone)
        //{
        //    // [0, 0.9] > [0, 1]
        //    float progress = Mathf.Clamp01(operation.progress / 0.9f);
        //    Debug.Log("Loading progress: " + (progress * 100) + "%");

        //    Debug.Log(operation.isDone);
        //    operation.allowSceneActivation = true;
        //    Debug.Log(operation.isDone);

        //    //if (operation.progress == 0.9f)
        //    //{
        //    //    Debug.Log("Press a key to start");
        //    //    if (Input.anyKeyDown)
        //    //        operation.allowSceneActivation = true;
        //    //}

        //    yield return null;
        //}


        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("Passed");

        //AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        //while (!async.isDone)
        //{
        //    yield return null;
        //}

    }
}
            
