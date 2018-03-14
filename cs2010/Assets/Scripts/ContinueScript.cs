using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    public Button continueButton;
    public Button save1;
    public Button save2;
    public Button save3;

    void Start()
    {
        continueButton.enabled = false;
        save1.enabled = false;
        save2.enabled = false;
        save3.enabled = false;

        int NumOfSaves = SaveLoad.CountSavedGames();

        if (NumOfSaves > 0)
        {
            continueButton.enabled = true;
        }

        for (int i = 0; i < NumOfSaves; i++)
        {
            if (SaveLoad.LoadSlot(i) != null)
            {
                int temp = i + 1;
                switch (temp)
                {
                    case 1:
                        save1.enabled = true;
                        break;
                    case 2:
                        save2.enabled = true;
                        break;
                    case 3:
                        save3.enabled = true;
                        break;
                    default:
                        Debug.Log("No save data");
                        break;
                }
            }
        }
    }
}
