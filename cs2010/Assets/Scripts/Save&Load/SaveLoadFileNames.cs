using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadFileNames : MonoBehaviour {

    public TextMeshProUGUI slot1;
    public TextMeshProUGUI slot2;
    public TextMeshProUGUI slot3;

    void Update () {
        GameState[] savedGames = SaveLoad.savedGames;

        for (int i = 0; i < savedGames.Length; i++)
        {
            if (SaveLoad.LoadSlot(i) != null)
            {
                int tmp = i + 1;

                if (SaveLoad.LoadSlot(i).fileName == null)
                {
                    SaveLoad.LoadSlot(i).fileName = "save slot " + tmp;
                }

                switch(tmp){
                    case 1:
                        slot1.text = SaveLoad.LoadSlot(i).fileName;
                        break;
                    case 2:
                        slot2.text = SaveLoad.LoadSlot(i).fileName;
                        break;
                    case 3:
                        slot3.text = SaveLoad.LoadSlot(i).fileName;
                        break;
                    default:
                        Debug.Log("Outta range nug nug");
                        break;
                }
            }
        }
	}
}
