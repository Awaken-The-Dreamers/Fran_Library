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


    void Start()
    {
        MarkerColorPick();
        BookSpawner();

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

    public void BookSpawner()
    {
        for (int i = 0; i < markers.Length; i++)
        {

            markerCollider = markers[i].GetComponent<Collider>();
            GameObject instance = Instantiate(bookPrefab, markerCollider.bounds.center, Quaternion.identity);
            instance.GetComponent<Renderer>().material.color = colors[colorChecker[i]];
            float xScale = Random.Range(1, 3);
            float yScale = Random.Range(1, 3);
            float sizeScale = 1.2f;
            instance.GetComponent<Transform>().localScale = new Vector3 (xScale * sizeScale, yScale * sizeScale, 1);

        }
    }
}
