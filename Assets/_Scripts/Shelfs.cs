using System.Collections.Generic;
using UnityEngine;

public class Shelfs : MonoBehaviour
{
    [SerializeField]
    public GameObject[] shelfs = new GameObject[9];
    private int shelfsCount;
    private Spawner sScript;
    private int totalBooksOnSingleShelf;
    private int currentBook = 0;
    public List<float> totalBookScaleOnShelf = new List<float>();
    private int currentShelf; //which shelf is curretly being processed
    private int bpStart; //BookPickerStart - start of the size picker
    private float totalBookScale; //total books count on shef
    private int loopCounter = 0; //start point of the ScalePickup loop


    void Start()
    {
        sScript = GameObject.Find("GameManager").GetComponent<Spawner>();
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
}
