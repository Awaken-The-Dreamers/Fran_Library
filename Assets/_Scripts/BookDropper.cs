using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookDropper : MonoBehaviour
{
    private GameObject gm;
    private Spawner spawnerScript;
    private List<GameObject> booksList;
    private int booksToPick;
    public List<GameObject> randomList = new List<GameObject>();

public void GetDefaultComponents()
    {
        gm = GameObject.Find("GameManager");
        spawnerScript = gm.GetComponent<Spawner>();
        booksList = spawnerScript.booksList;

        BookDropperLoop();
    }

    void BookDropperLoop()
    {
        booksToPick = Random.Range(7,13);
        for (int i = 0; i < booksToPick; i++)
        {
        List<GameObject> randomList = booksList.OrderBy(x => Random.value).ToList();
        randomList[i].SetActive(false); // this will be replaced by bookMovement function
        //Debug.Log($"i= {i} | booksToPick = {booksToPick}");
        booksList.RemoveAt(i);
        }
    }

}
