using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    private JSONData jSONData;
    private LineController lineController;

    public SpriteRenderer magenta;
    public Sprite blue;  
    public int checkNumbers, clickedNumber, newNumber;

    void Start()
    {
        lineController = FindObjectOfType<LineController>();
        jSONData = FindObjectOfType<JSONData>();
    }

    //click and validate gamebuttons
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            checkNumbers = jSONData.checkNumber;
            TextMesh tekstas = this.gameObject.GetComponentInChildren<TextMesh>();
            clickedNumber = int.Parse(tekstas.text);


            if (checkNumbers == clickedNumber && jSONData.allowAnimation)
            {
                jSONData.allowAnimation = false;

                magenta.sprite = blue;

                jSONData.checkNumber++;
                StartCoroutine(FadeOut());

                if (checkNumbers==1)
                {
                    jSONData.FirstButtonPos = jSONData.buttonPos1 = gameObject.transform.position;
                    jSONData.allowAnimation = true;
                }
                else
                {
                    jSONData.buttonPos2 = gameObject.transform.position;
                    lineController.LineGenerator();
                    jSONData.buttonPos1 = jSONData.buttonPos2;

                    if (clickedNumber == jSONData.buttonsCount)
                    {
                        jSONData.buttonPos2 = jSONData.FirstButtonPos;
                        jSONData.lastButton = true;
                    }
                }
            }
        }
        
    }

    //butttons text fadeout
    IEnumerator FadeOut()
    {
        for (float i = 1f; i >= -0.05f; i -= 0.05f)
        {
            TextMesh tekstas = this.gameObject.GetComponentInChildren<TextMesh>();
            Color originalColor = tekstas.color;
            originalColor.a = i;
            tekstas.color = originalColor;
            yield return new WaitForSeconds(0.05f);
        }        
    }
}
