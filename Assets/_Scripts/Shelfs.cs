using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shelfs : MonoBehaviour
{
    [SerializeField]
    public GameObject[] shelfs = new GameObject[9];
    private int shelfsCount;
    private GameObject gm;
    private Spawner sScript;
    private BookDropper bdScript;
    private int totalBooksOnSingleShelf;
    private int currentBook = 0;
    public List<float> totalBookScaleOnShelf = new List<float>();
    private int currentShelf; //which shelf is curretly being processed
    private int bpStart; //BookPickerStart - start of the size picker
    private float totalBookScale; //total books count on shef
    private int loopCounter = 0; //start point of the ScalePickup loop
    //[SerializeField]
    public List<GameObject> lastBooks;


    void Start()
    {
        gm = GameObject.Find("GameManager");
        sScript = gm.GetComponent<Spawner>();
        bdScript = gm.GetComponent<BookDropper>();
        totalBooksOnSingleShelf = sScript.spawnRepeat;
        shelfsCount = shelfs.Length - 1;
        for (bpStart = 0; bpStart < shelfs.Length; bpStart++)
        {
            float selfSize = shelfs[bpStart].GetComponent<Transform>().localScale.x;
        }
    }

    public void BookCounter(int totalBooksOnSingleShelf, int loopCounter)
    {
        /*
         * Loop gets Scale of the first book on shelf {currentBookScale}
         * When all book Scales are summirized in {totalBookScale} it adds this float to the  list {totalBookScaleOnShelf}
         * And proceeds to the Checker;
         */
        if (currentShelf > 0 && currentBook <= totalBooksOnSingleShelf) currentBook = currentShelf; 
        for (loopCounter = loopCounter + currentShelf; loopCounter < totalBooksOnSingleShelf + currentShelf; loopCounter++)
        {
            sScript = GameObject.Find("GameManager").GetComponent<Spawner>();

            if (currentBook < sScript.booksList.Count)
            {
                float currentBookScale = sScript.booksList[currentBook].GetComponent<Transform>().localScale.x;
                currentBook += 9;
                totalBookScale += currentBookScale;
            }
            if (loopCounter == (totalBooksOnSingleShelf - 1) + currentShelf && totalBookScaleOnShelf.Count <= shelfsCount) 
            {
                totalBookScaleOnShelf.Add(totalBookScale);
                Checker();
            }
        }
    }

    void Checker()
    {
        /*
         * When all sizes are recorded to the list {totalBookScaleOnShelf}
         * Checker Increases current shelf, currentBook values by 1; resets totalBookScale & loopCounter; 
         */
        
        if (totalBookScaleOnShelf.Count == 9) BookRemover();
        if (currentShelf < shelfsCount)
        {
            totalBookScale = 0;
            currentBook = 0;
            loopCounter = 0;
            totalBooksOnSingleShelf = sScript.spawnRepeat;
            if (loopCounter < shelfsCount) ++currentShelf; 
            //restart measure cycle

            BookCounter(totalBooksOnSingleShelf, loopCounter);
            
        }
    }

    void BookRemover()
    {
        //creating an array of last books which might be deleted if shelf size > 4
        lastBooks = new List<GameObject>
        {
            sScript.booksList[36],
            sScript.booksList[37],
            sScript.booksList[38],
            sScript.booksList[39],
            sScript.booksList[40],
            sScript.booksList[41],
            sScript.booksList[42],
            sScript.booksList[43],
            sScript.booksList[44]
            
        };
        float sizeLimit = 3.75f;
        //loop wiil disable last book that exceed sizeLimit
        for (int i = 0; i < totalBookScaleOnShelf.Count; i++)
        {
            if (totalBookScaleOnShelf[i] > sizeLimit) lastBooks[i].GameObject().SetActive(false);
            if (i == totalBookScaleOnShelf.Count-1) bdScript.GetDefaultComponents();
        }
    }
}