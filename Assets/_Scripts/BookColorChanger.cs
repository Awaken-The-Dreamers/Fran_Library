using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BookColorChanger : MonoBehaviour
{
    private BookGen bookGenScript;
    private Color bookInstanceColor;
    

    void Start()
    {
        //SomeFunction();

    }
    void SomeFunction()
    {
        bookGenScript = GameObject.Find("BookGenerator").GetComponent<BookGen>();
        
        for (int i = 0; i < bookGenScript.markers.Length; i++)
        {
            int checker = bookGenScript.colorChecker[i];
            
            
            Debug.Log("pickedColor = " + bookGenScript.pickedColor + " | Color = " + bookInstanceColor);
            gameObject.GetComponent<Renderer>().material.color = bookInstanceColor;
        }
        
        
    }
}
