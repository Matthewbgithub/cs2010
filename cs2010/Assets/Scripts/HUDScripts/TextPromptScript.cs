using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextPromptScript : MonoBehaviour {

    public GoBoard board;
    public Canvas textCanvas;
    public TextMeshProUGUI GOText;
    public TextMeshProUGUI BlitzText;
    public TextMeshProUGUI passText;

    void Start()
    {
        board = board.GetComponent<GoBoard>();
        textCanvas.gameObject.SetActive(true);
        //if (GoBoard.blitzMode)
        //{
        //    BlitzText.gameObject.SetActive(true);
        //}
        //else
        //{
        //    GOText.gameObject.SetActive(true);
        //}
        ////textCanvas.gameObject.SetActive(false);
        //DisplayStart();
    }
       
    IEnumerator DisplayStart()
    {
        if (GoBoard.blitzMode)
        {
            BlitzText.gameObject.SetActive(true);
        }
        else
        {
            GOText.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        GOText.gameObject.SetActive(false);
        BlitzText.gameObject.SetActive(false);
    }

    //IEnumerator DisplayPassText () {
    //    if(board.IsWhiteTurn()){
    //        passText.text = "black passed";
    //    }
    //    else{
    //        passText.text = "white passed";
    //    }
    //    passText.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(2);
    //    passText.gameObject.SetActive(false);
    //}


} 
