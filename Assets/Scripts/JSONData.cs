using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JSONData : MonoBehaviour
{
    public GameObject LevelsPanel;

    public GameObject listButton;
    public GameObject content;

    public TextAsset textJSON;

    public GameObject button;

    public Transform startPoint;
        
    public int buttonNumber, listNumber;

    public int levelsCount, buttonsCount,linesCount, levelNumber, checkNumber=1;

    public bool allowAnimation, lastButton;

    public Vector3 buttonPos1, buttonPos2, FirstButtonPos;

    //JSON Data
    [System.Serializable]
    public class Levels
    {
        public string[] level_data;
    }

    [System.Serializable]
    public class LevelsList
    {
        public Levels[] levels;
    }

    public LevelsList levelsList = new LevelsList();

   
    void Start()
    {

        Vector3 screenPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        button.transform.position = screenPoint;

        levelsList = JsonUtility.FromJson<LevelsList>(textJSON.text);
        levelsCount = levelsList.levels.Length;

        buttonNumber = 0;
        listNumber = 0;

        allowAnimation = true;
        lastButton = false;

        LevelsPanel.SetActive(true);
        CreateLevelsList();
    }

    public void InstantiateButtons()
    {
        buttonsCount = levelsList.levels[levelNumber].level_data.Length/2;
        for (int i = 0; i < buttonsCount; i++)
        {
            buttonNumber++;
            button.GetComponentInChildren<TextMesh>().text = buttonNumber.ToString();

            float x = (float.Parse(levelsList.levels[levelNumber].level_data[i*2])) / 1000;
            float y = -(float.Parse(levelsList.levels[levelNumber].level_data[i * 2 + 1])) / 1000;

            Vector3 startPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height*x, Screen.height*y+Screen.height, 1));

            Instantiate(button, startPos, Quaternion.identity);
        }
    }

    public void CreateLevelsList()
    {
        for (int i = 0; i < levelsCount; i++)
        {
            var newListButton = Instantiate(listButton);
            newListButton.transform.parent = content.transform;
            listNumber++;
            newListButton.GetComponentInChildren<Text>().text = listNumber.ToString();
        }        
    }
      
//levels selection
    public void OnMouseOver()
    {
        GameObject levelteks = EventSystem.current.currentSelectedGameObject;
        Text tekstas = levelteks.GetComponentInChildren<Text>();
        levelNumber = int.Parse(tekstas.text) - 1;
        LevelsPanel.SetActive(false);
        InstantiateButtons();

    }
}
