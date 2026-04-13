using System.Xml;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookCatcher : MonoBehaviour
{
private InputAction leftMouseClick;
private InputAction rightMouseClick;
private GameObject Indicator;
private Transform BookSocketTransform;
private Transform bookTransform;
private bool bookCatched;

private void Awake() 
{
    leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
    rightMouseClick = new InputAction(binding: "<Mouse>/rightButton");
    leftMouseClick.Enable();
    Indicator = GameObject.Find("Indicator");
    BookSocketTransform = GameObject.Find("BookSocket").GetComponent<Transform>();
    
}
void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Book")
        {
             if (!bookCatched)
             {
                Indicator.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
             }
             
             //Debug.Log($"Found = {other.gameObject.name}");
             leftMouseClick.performed += ctx => LeftMouseClicked(other);
             //CONTINUE HEREs
             //rightMouseClick.performed += ctx => RightMouseClicked(other);
        }
    
}

private void LeftMouseClicked(Collider other) 
{
        if (other.gameObject.tag == "BookHeld" && bookCatched)
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.tag = "Book";
            bookCatched = false;
        }

    if (other.gameObject.tag == "Book" && !bookCatched)
        {
                print("CLICK");
                bookTransform = other.gameObject.GetComponent<Transform>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                other.gameObject.transform.Rotate(0,0,0, Space.World);
                other.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                bookCatched = true;
                other.gameObject.tag = "BookHeld";
        }
}

private void RightMouseClicked()
    {
        
    }

    void Update()
    {
        if (bookCatched)
        {
           bookTransform.position = BookSocketTransform.position; 
        }
        
    }

}
