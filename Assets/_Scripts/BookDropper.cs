using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        //Get random books, move them forward, rotate by RNG and enable physics
        randomList[i].transform.Translate(Vector3.forward * -1.5f, Space.Self);
        float bookRotation = Random.Range(-30f,30f); 
        randomList[i].transform.Rotate(0f,0f, bookRotation,Space.Self);
        randomList[i].GetComponent<Rigidbody>().isKinematic = false;
        randomList[i].GetComponent<Collider>().providesContacts = true;
        randomList[i].tag = "Book";
        //remove moved book from bookList so it wont be moved again
        booksList.Remove(randomList[i]);
        }
    }

}
