using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Shelfs : MonoBehaviour
{
    [SerializeField]
    public GameObject[] shelfs = new GameObject[9];
    private int shelfsCount;
    private Spawner sScript;
    private int totalBooksOnSingleShelf;
    private int currentBook = 0;
    //[SerializeField]
    public List<float> totalBookScaleOnShelf = new List<float>();
    private int currentShelf; //which shelf is curretly being processed
    private int bpStart; //BookPickerStart - start of the size picker
    private float totalBookScale; //total books count on shef
    //private int recordTotalBookScaleForShelf = 0; //for which shelf we're currently recording sizes
    private int loopCounter = 0; //start point of the ScalePickup loop


    void Start()
    {
        sScript = GameObject.Find("GameManager").GetComponent<Spawner>();
        totalBooksOnSingleShelf = sScript.spawnRepeat;
        shelfsCount = shelfs.Length - 1;
        for (bpStart = 0; bpStart < shelfs.Length; bpStart++)
        {
            float selfSize = shelfs[bpStart].GetComponent<Transform>().localScale.x;
            //Debug.Log($"Shelf_{i} " + "size: " + selfSize); 
            //0-9-18-27-36

        }
    }

    public void BookCounter(int totalBooksOnSingleShelf, int loopCounter)
    {
        /*
         * Loop gets Scale of the first book on shelf {currentBookScale}
         * When all book Scales are summirized in {totalBookScale} it adds this float to the  list {totalBookScaleOnShelf}
         * And proceeds to the Checker;
         */
        //Debug.Log($"BookCounter Started, totalBooksOnSingleShelf= {totalBooksOnSingleShelf}");
        Debug.Log($"loopCounter:{loopCounter} | currentShelf: {currentShelf}");
        if (currentShelf > 0 && currentBook <= totalBooksOnSingleShelf) currentBook = currentShelf; // since currentShelf never resets it could be used as next book var
        for (loopCounter = loopCounter + currentShelf; loopCounter < totalBooksOnSingleShelf + currentShelf; loopCounter++)
        {

            //Debug.Log($"BookCounter|loopCounter= {loopCounter} |totalBooksOnSingleShelf: {totalBooksOnSingleShelf}");
            sScript = GameObject.Find("GameManager").GetComponent<Spawner>();
            if (currentBook < sScript.booksList.Count)
            {
                Debug.Log($"loop:{loopCounter} | currentBook:{currentBook}");
                float currentBookScale = sScript.booksList[loopCounter + currentBook].GetComponent<Transform>().localScale.x;
                //Debug.Log($"loop= {loopCounter} | currentBook:{currentBook} | currentBookScale={currentBookScale}");
                currentBook += 8;
                totalBookScale += currentBookScale;
            }
            Debug.Log($"loopCounter:{loopCounter} | currentShelf: {currentShelf}");
            if (loopCounter == (totalBooksOnSingleShelf - 1) + currentShelf) //issues here; totalBooksOnSingleShelf =5; loop stops earlier
            {
                totalBookScaleOnShelf.Add(totalBookScale);
                Debug.Log($"loop={loopCounter} | currentShelf{currentShelf} |totalBookScaleHERE: {totalBookScale}");
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
        //if (currentShelf < shelfs.Length) currentShelf++; i = 0; BookCounter(totalBooksOnSingleShelf);
        if (currentShelf <= shelfsCount)
        {
            Debug.Log($"CHECKER|currentShelf:{currentShelf} |totalBooksOnSingleShelf:{totalBooksOnSingleShelf} |AllBooksScale: {totalBookScaleOnShelf.ToCommaSeparatedString()} |");
            if (loopCounter < 8) ++currentShelf;//goes up to 8 included
            totalBookScale = 0;
            currentBook = 0;
            loopCounter = 0;
            totalBooksOnSingleShelf = sScript.spawnRepeat;
            BookCounter(totalBooksOnSingleShelf, loopCounter);
        }


    }
}
