using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int repeatSpawn;
    private GameObject bookInstance;
    private Markers markersScript;
    private Shelfs shelfsScript;
    public GameObject bookPrefab;
    public int spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        GameObject gm = GameObject.Find("GameManager");
        markersScript = gm.GetComponent<Markers>();
        shelfsScript = gm.GetComponent <Shelfs>();
        for (repeatSpawn = 0; repeatSpawn < 2; repeatSpawn++) //spawn X books at 1 marker
        {
            for (spawnPoint = 0; spawnPoint < markersScript.markers.Length; spawnPoint++ ) //spawn on all markers
            {
                //Collider markerCollider = markersScript.markers[spawnPoint].GetComponent<Collider>();//getting collider
                bookInstance = Instantiate(bookPrefab, shelfsScript.shelfs[spawnPoint].transform.position, Quaternion.identity);
            }
            
            //Vector3 bookMeshBoundsSize = bookInstance.GetComponent<Renderer>().bounds.size;
            //bookInstance.GetComponent<Transform>().Translate(bookMeshBoundsSize.x * 1.55f, 0, 0, Space.Self);
        }
    }

}
