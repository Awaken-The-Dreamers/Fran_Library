using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BookGen : MonoBehaviour
{
    private Color[] colors = new Color[]
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
    private int[] colorChecker = new int[9] { 9,9,9,9,9,9,9,9,9 };
    public int pickedColor;
    public int currentMarker = 0;

    void Start()
    {
        MarkerColorPick();
    }

    void MarkerColorPick()
    {

        int pickedColor = Random.Range(0, colors.Length);
        MarkerColorCheck();

        void MarkerColorCheck()
        {
            if (!colorChecker.Contains(pickedColor))
            {
                //Debug.Log("!!!CHECKER pickedColor: " + pickedColor + " | colorChecker: " + colorChecker.ToCommaSeparatedString() + " | currentMarker: " + currentMarker);
                MarkerColorSet();
            }
            if (colorChecker.Contains(pickedColor) && currentMarker < markers.Length)
            {
                //Debug.Log("_CHECKER pickedColor: " + pickedColor + " | colorChecker: " + colorChecker.ToCommaSeparatedString() + " | currentMarker: " + currentMarker);
                MarkerColorPick();
            }

        }
        void MarkerColorSet()
        {
            if (currentMarker <= markers.Length)
            {
                markers[currentMarker].GetComponent<Renderer>().material.color = colors[pickedColor];
                colorChecker[currentMarker] = pickedColor;
                ++currentMarker;
                //Debug.Log("SET pickedColor: " + pickedColor + " | colorChecker: " + colorChecker.ToCommaSeparatedString() + " | currentMarker: " + currentMarker + " | colorChecker.Contains(pickedColor): " + colorChecker.Contains(pickedColor) + " | currentMarker < markers.Length: " + (currentMarker < markers.Length));
                MarkerColorPick();
            }

        }
    }

}
