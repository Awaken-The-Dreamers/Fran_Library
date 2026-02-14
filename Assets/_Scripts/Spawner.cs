using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int repeatSpawn;
    private GameObject bookInstance;
    private Markers markersScript;
    private Shelfs shelfsScript;
    private BookChanger bookScript;
    public GameObject bookPrefab;
    public int spawnPoint;
    [SerializeField] public List<GameObject> booksList = new List<GameObject>();
    private bool firstSpawn = true;
    private int markersCount;
    private int booksCount;
    private GameObject gm;
    private Color[] colors;
    private int[] checker;
    private GameObject[] shelfs;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        markersScript = gm.GetComponent<Markers>();
        colors = markersScript.colors;
        checker = markersScript.colorChecker;
        shelfsScript = gm.GetComponent<Shelfs>();
        shelfs = shelfsScript.shelfs;
        markersCount = markersScript.markers.Length;
        
    }
    public void SpawnFirstBooks()
    {

        for (int i = 0; i < markersCount; i++) //markersCount = 9;
        {
            //counting current books in list
            booksCount = booksList.Count;
            //Instantiate book prefab + make Var of it
            bookInstance = Instantiate(bookPrefab, shelfs[i].transform.position, Quaternion.identity);
            //add this instance to the List to access it later
            booksList.Add(bookInstance);
            //Get bookChangerScript of the current Instance
            bookScript = bookInstance.GetComponent<BookChanger>();
            //intiate scale change
            bookScript.BookChange();
            //Color + Pos change
            ColorChange();
            PositionChange();
        }

    }

    public void ColorChange()
    {
        booksList[booksCount].GetComponent<Renderer>().material.color = colors[checker[spawnPoint]]; //spawnPoint pick color
    }

    public void PositionChange()
    {
        //get scales of shelfs and current book instance
        Vector3 shelfScale = shelfs[spawnPoint].GetComponent<Transform>().localScale;
        Vector3 bookScale = booksList[booksCount].GetComponent<Transform>().localScale;

        //get X and Y pos change of the book accordingly to the shelf 
        float bookPosX = ((-shelfScale.x * 0.5f) + (bookScale.x * 0.5f));
        float bookPosY = ((-shelfScale.y * 0.5f) + (bookScale.y * 0.5f));
        Vector3 bookPosXYZ = new Vector3(bookPosX, bookPosY, 0);
        //pos change
        booksList[booksCount].transform.Translate(bookPosXYZ, shelfs[spawnPoint].transform);
    }

}
