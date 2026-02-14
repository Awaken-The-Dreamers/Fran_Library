using UnityEngine;

public class Shelfs : MonoBehaviour
{
    //[SerializeField] public GameObject[] shelfs = new GameObject[9];
    [SerializeField]
    public GameObject[] shelfs = new GameObject[9];
    private Spawner spawnerScript;
    //private Bounds bounds;
    //public Vector3 max;
    //public Vector3 min;



    void Start()
    {
        spawnerScript = GameObject.Find("GameManager").GetComponent<Spawner>();
        /*
        Debug.Log("spawnPoint = " + spawnerScript.spawnPoint);
        bounds = shelfColliders[0].bounds;
        max = bounds.max;
        min = bounds.min;
        Debug.Log("bounds = " + bounds);
        */

    }
}
