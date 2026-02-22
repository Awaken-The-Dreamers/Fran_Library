using UnityEngine;

public class BookChanger : MonoBehaviour
{
    public Vector3 bookScale;


    public void BookChange()
    {
        //RNG for size change
        float[] xScales = { 0.3f, 0.5f, 0.7f, 1f };
        float[] yScales = { 1.5f, 1.7f, 1.9f, 2.1f, 2.3f };
        float xRandom = Random.Range(0, 4);
        float yRandom = Random.Range(0, 5);
        float xScale = xScales[(int)xRandom];
        float yScale = yScales[(int)yRandom];
        Vector3 sizeChange = new Vector3(xScale, yScale, 1);
        //change scale
        this.gameObject.GetComponent<Transform>().localScale = sizeChange;

    }
}
