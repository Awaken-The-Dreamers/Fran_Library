using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Markers : MonoBehaviour
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
    public GameObject[] markers = new GameObject[9];
    public int[] colorChecker = new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    public int pickedColor;
    public int currentMarker = 0;
    public Collider markerCollider;
    private Spawner sScript;
    private bool allMarkersColored = false;
    public int spawnCount;

    void Start()
    {
        sScript = GameObject.Find("GameManager").GetComponent<Spawner>();
        MarkerColorPick();
        

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
            if (currentMarker >= markers.Length) 
            {
                allMarkersColored = true;
                sScript.SpawnBooks();
            }
            MarkerColorPick();
        }


    }
}
