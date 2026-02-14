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

        /*spawns book at the start of the shelf collider
         * Changes it color and pos
         * 
        */

        for (spawnPoint = 0; spawnPoint < markersCount; spawnPoint++) 
        {
            //counting current books in list
            booksCount = booksList.Count;
            //Instantiate book prefab
            bookInstance = Instantiate(bookPrefab, shelfs[spawnPoint].transform.position, Quaternion.identity);
            //add this instance to the List to access it later
            booksList.Add(bookInstance);
            //Get bookChangerScript of the current Instance
            bookScript = bookInstance.GetComponent<BookChanger>();
            //intiate scale change
            bookScript.BookChange();
            
            //get scales of shelfs and current book instance
            Vector3 shelfScale = shelfs[spawnPoint].GetComponent<Transform>().localScale;
            Vector3 bookScale = booksList[booksCount].GetComponent<Transform>().localScale;

            //get X and Y pos change of the book accordingly to the shelf 
            float bookPosX = ((-shelfScale.x * 0.5f) + (bookScale.x * 0.5f));
            float bookPosY = ((-shelfScale.y * 0.5f) + (bookScale.y * 0.5f));
            Vector3 bookPosXYZ = new Vector3(bookPosX, bookPosY, 0);

            //pos + color change
            bookInstance.GetComponent<Renderer>().material.color = colors[checker[spawnPoint]];
            booksList[booksCount].transform.Translate(bookPosXYZ, shelfs[spawnPoint].transform);
            for (int Repeat = 0; Repeat < 2; Repeat++)
            {

            }
        }

    }

    public void RepeatBookSpawn()
    {

        if (firstSpawn && booksCount < markersCount)
        {

        }
        if (firstSpawn && booksCount >= markersCount) firstSpawn = false;
        if (!firstSpawn && booksCount >= markersCount)
        {
            //booksList[booksCount].transform.Translate(bookPosXYZ, shelfs[spawnPoint].transform);
        }
        /*
         * If its possible, might be better to spawn next books relative to 1st book spawn
         * booksList[spawnPoint-(9 * spawnRepeat)].transform
         * we get pos of 1st book, 2nd book and etc
         */
    }

}
