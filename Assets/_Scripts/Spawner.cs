using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int repeatSpawn;
    private GameObject bookInstance;
    private Markers markersScript;
    private Shelfs sScript;
    private BookChanger bookScript;
    public GameObject bookPrefab;
    private int spawnPoint;
    [SerializeField] public List<GameObject> booksList = new List<GameObject>();
    private int booksCount;
    private int shelfsCount;
    private GameObject gm;
    private Color[] colors;
    private int[] checker;
    private GameObject[] shelfs;
    private Vector3 shelfScale;
    private float shelfSpawnStart_X;
    private float shelfSpawnStart_Y;
    private float firstBookPosX;
    private float firstBookPosY;
    public int spawnRepeat = 5; //books to spawn on each shelf
    private int spawnCycle = 1;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        markersScript = gm.GetComponent<Markers>();
        colors = markersScript.colors;
        checker = markersScript.colorChecker;
        sScript = gm.GetComponent<Shelfs>();
        shelfs = sScript.shelfs;
        shelfsCount = shelfs.Length;
        shelfScale = shelfs[spawnPoint].GetComponent<Transform>().localScale;
        shelfSpawnStart_X = -shelfScale.x * 0.5f;
        shelfSpawnStart_Y = -shelfScale.y * 0.5f;
    }
    public void SpawnBooks()
    {
        
        for (int i = 0; i < shelfsCount; i++) //shelfs = 9;
        {
            if (spawnCycle == 1)
            {
                FirstBooksSpawn();
            }
            if (spawnCycle > 1 && spawnCycle <= spawnRepeat)
            {
                NextBooksSpawn();
            }

        }

    }
    public void ColorChange()
    {
        booksList[booksCount].GetComponent<Renderer>().material.color = colors[checker[spawnPoint]];
    }
    public void FirstBooksSpawn()
    {
        
        //Instantiate book prefab + make Var of it
        bookInstance = Instantiate(bookPrefab, shelfs[spawnPoint].transform.position, Quaternion.identity);
        //add this instance to the List to access it later
        booksList.Add(bookInstance);
        //Get bookChangerScript of the current Instance
        bookScript = bookInstance.GetComponent<BookChanger>();
        //intiate scale change
        ColorChange();
        bookScript.BookChange();
        //Color + Pos change
        
        
        Vector3 firstBookScale = booksList[booksCount].GetComponent<Transform>().localScale;
        firstBookPosX = shelfSpawnStart_X + (firstBookScale.x * 0.5f);
        firstBookPosY = shelfSpawnStart_Y + (firstBookScale.y * 0.5f);
        //get X and Y pos change of the book accordingly to the shelf 
        Vector3 firstBookPosXYZ = new Vector3(firstBookPosX, firstBookPosY, 0);
        //pos change
        booksList[booksCount].transform.Translate(firstBookPosXYZ, shelfs[spawnPoint].transform);
        if (spawnPoint < shelfsCount) spawnPoint++;
        booksCount = booksList.Count;
        SpawnCycleComplete();

    }

    public void NextBooksSpawn()
    {
        Vector3 GetCurrentBookScale()
        {
            Vector3 currentBookScale = booksList[booksCount].GetComponent<Transform>().localScale;
            return currentBookScale;
        }

        Vector3 GetPriorBookScale()
        {
            Vector3 priorBookScale = booksList[booksCount - shelfsCount].GetComponent<Transform>().localScale;
            return priorBookScale;
        }
        //Instantiate book prefab
        bookInstance = Instantiate(bookPrefab, booksList[booksCount - shelfsCount].transform.position, Quaternion.identity);
        //add this instance to the List to access it later
        booksList.Add(bookInstance);
        //Get bookChangerScript of the current Instance
        bookScript = bookInstance.GetComponent<BookChanger>();
        //intiate scale change
        ColorChange();
        bookScript.BookChange();
        //Color + Pos change
        
        float newBookPosY = (-GetPriorBookScale().y*0.5f) + (GetCurrentBookScale().y *0.5f);
        float newBookPosX = (GetPriorBookScale().x *0.5f) + (GetCurrentBookScale().x * 0.5f) + 0.05f;

        Vector3 newBookPosXYZ = new Vector3(newBookPosX, newBookPosY, 0);
        //pos change
        booksList[booksCount].transform.Translate(newBookPosXYZ, booksList[booksCount - shelfsCount].transform);
        if (spawnPoint < shelfsCount-1) spawnPoint++;
        booksCount = booksList.Count;
        SpawnCycleComplete();
    }
    public void SpawnCycleComplete()
    {
        //complete when 1 book have appeared on each of 9 shelfs
        if (booksCount / shelfsCount / spawnCycle == 1)
        {
            spawnPoint = 0;
            SpawnCycleEnd();
        }
    }
    public void SpawnCycleEnd()
    {
        //when all books appeared - count as complete spawnCycle
        spawnCycle++;
        if (spawnCycle <= spawnRepeat) Invoke("SpawnBooks", 0);
        //start of the Book Size counter
        if (spawnCycle > spawnRepeat)
        {
            Debug.Log($"{spawnCycle} > {spawnRepeat}");
            sScript.BookCounter(spawnRepeat, 0); //0 = start int for loop

        }

        
    }





}
