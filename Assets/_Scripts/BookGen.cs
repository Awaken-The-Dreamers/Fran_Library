using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BookGen : MonoBehaviour
{
    public Color[] colors = new Color[]
    {
        Color.aquamarine,
        Color.cornflowerBlue,
        Color.darkOrange,
        Color.indianRed,
        Color.mediumOrchid,
        Color.navyBlue,
        Color.plum,
        Color.skyBlue,
        Color.yellowNice
    };
    public GameObject[] markers;
    public int[] colorChecker = new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    public int pickedColor;
    public int currentMarker = 0;
    public GameObject bookPrefab;
    public GameObject emptySpacePrefab;
    private Collider markerCollider;
    private bool allMarkersColored = false;
    private float bookShift = -1.7f;

    void Start()
    {
        Debug.Log("Start_Shift: " + bookShift);
        MarkerColorPick();
        SpawnRepeater();

    }

    public void MarkerColorPick()
    {
        pickedColor = Random.Range(0, colors.Length);

        if (currentMarker <= markers.Length && !colorChecker.Contains(pickedColor))
        {
            MarkerColorSet();
        }
        if (currentMarker <= markers.Length && colorChecker.Contains(pickedColor) && !allMarkersColored)
        {
            MarkerColorPick();
        }



    }

    public void MarkerColorSet()
    {
        if (currentMarker <= markers.Length)
        {
            markers[currentMarker].GetComponent<Renderer>().material.color = colors[pickedColor];
            colorChecker[currentMarker] = pickedColor;
            ++currentMarker;
            /*
            Debug.Log("currentMarker: " + currentMarker);
            Debug.Log("pickedColor: " + pickedColor);
            Debug.Log("colorChecker: " + colorChecker.ToCommaSeparatedString());
            */

            if (currentMarker >= markers.Length) allMarkersColored = true;
            MarkerColorPick();
        }


    }

    void SpawnRepeater()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("Rep_Shift: " + bookShift);
            bookShift += 0.65f;
            BookSpawner();
            
            
        }
    }
    public void BookSpawner()
    {
        for (int i = 0; i < markers.Length; i++)
        {
            markerCollider = markers[i].GetComponent<Collider>();//getting collider
            Debug.Log("Spwn_Shift: " + bookShift);
            float[] xScales = { 0.3f, 0.5f, 0.7f, 1f };
            float[] yScales = { 1.5f, 1.7f, 1.9f, 2.1f, 2.3f };
            float xRandom = Random.Range(0, 4);
            float yRandom = Random.Range(0, 5);
            float xScale = xScales[(int)xRandom];
            float yScale = yScales[(int)yRandom];
            //color change
            GameObject instance = Instantiate(bookPrefab, markerCollider.bounds.center, Quaternion.identity);
            instance.GetComponent<Renderer>().material.color = colors[colorChecker[i]];
            //scale change
            instance.GetComponent<Transform>().localScale = new Vector3(xScale, yScale, 1);
            Vector3 bounds = instance.GetComponent<Collider>().bounds.max;//mb get bounds and add new book outside
            //move relative to self
            instance.GetComponent<Transform>().Translate(bookShift, 0, 0, Space.Self); //x = +1.4f MAX right | -1.4f MAX left
        }
    }

    }
