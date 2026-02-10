using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BookChanger : MonoBehaviour
{
    private Markers markersScript;
    private Color[] mColors;
    private int[] mColorChecker;
    private int mSpawnCount;
    public Collider bookCollider;
    private Shelfs shelfsScript;


    void Start()
    {
        //get GO + Scripts
        GameObject gm = GameObject.Find("GameManager");
        shelfsScript = gm.GetComponent<Shelfs>();
        markersScript = gm.GetComponent<Markers>();
        GameObject[] shelfs = shelfsScript.shelfs;

        //vars for Scale + Sizechange
        mColors = markersScript.colors;
        mColorChecker = markersScript.colorChecker;
        mSpawnCount = markersScript.spawnCount;
        float[] xScales = { 0.3f, 0.5f, 0.7f, 1f };
        float[] yScales = { 1.5f, 1.7f, 1.9f, 2.1f, 2.3f };
        float xRandom = Random.Range(0, 4);
        float yRandom = Random.Range(0, 5);
        float xScale = xScales[(int)xRandom];
        float yScale = yScales[(int)yRandom];
        Vector3 sizeChange = new Vector3(xScale, yScale, 1);
        
        //color & scale change
        this.gameObject.GetComponent<Renderer>().material.color = mColors[mColorChecker[mSpawnCount]];
        this.gameObject.GetComponent<Transform>().localScale = sizeChange;

        //get collider
        bookCollider = this.gameObject.GetComponent<Collider>();
        this.gameObject.transform.Translate(shelfsScript.max.x, 0, 0, shelfsScript.shelfCollider.transform);
    }
}
