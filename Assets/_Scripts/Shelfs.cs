using Unity.VisualScripting;
using UnityEngine;

public class Shelfs : MonoBehaviour
{
    public GameObject[] shelfs = new GameObject[9];
    private Spawner spawnerScript;
    private Bounds bounds;
    public Vector3 max;
    public Vector3 min;
    public Collider shelfCollider;

    void Start()
    {
        // Example bounds setup if not assigned
        spawnerScript = GameObject.Find("GameManager").GetComponent<Spawner>();
        shelfCollider = shelfs[spawnerScript.spawnPoint].GetComponent<Collider>();
        bounds = shelfCollider.bounds;
        max = bounds.max;
        min = bounds.min;
        Debug.Log("bounds = " + bounds);


    }
}
