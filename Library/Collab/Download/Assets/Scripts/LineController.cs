using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineController : MonoBehaviour
{
    private GameControllerScript gameControllerScript;
    private JSONData jSONData;

    [SerializeField]
    private GameObject lineGeneratorPrefab;
    [SerializeField]
    private float animationDuration = 2f;

    private LineRenderer lineRenderer;

    public Vector3 startPos, endPos, firstPos;


    // Start is called before the first frame update
    void Start()
    {
        jSONData = FindObjectOfType<JSONData>();
        gameControllerScript = FindObjectOfType<GameControllerScript>();
    }

    public void SpawnLineGenerator()
    {
        StartCoroutine(AnimateLine());
    }

    private IEnumerator AnimateLine()
    {        
        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        lRend.positionCount = 2;

        startPos = jSONData.buttonPos1;
        endPos = jSONData.buttonPos2;
        firstPos = jSONData.FirstButtonPos;

        Vector3 pos = startPos;

        lRend.SetPosition(0, startPos);

        float startTime = Time.time;
        
        while (pos != endPos)
            {
            float t = (Time.time - startTime) / animationDuration;
            pos = Vector3.Lerp(startPos, endPos, t);
            lRend.SetPosition(1, pos);
            yield return null;
        }
        if (pos == endPos)
        {
            jSONData.allowAnimation = true;
            if (jSONData.lastButton)
            {
                SpawnLineGenerator();
                jSONData.lastButton = false;   
            }
        }
        if (pos == firstPos)
        {
            StartCoroutine(LoadLevelsPanel());
        }
    }

    private IEnumerator LoadLevelsPanel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
