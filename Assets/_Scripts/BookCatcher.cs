using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookCatcher : MonoBehaviour
{
private InputAction leftMouseClick;
private GameObject Indicator;

private void Awake() 
{
    leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
    leftMouseClick.Enable();
    Indicator = GameObject.Find("Indicator");
    
}
void OnTriggerEnter(Collider other)
{
    if (other.gameObject.tag == "Book")
        {
             Indicator.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;
             //Debug.Log($"Found = {other.gameObject.name}");
             leftMouseClick.performed += ctx => LeftMouseClicked(other);
        }
    
}

private void LeftMouseClicked(Collider other) 
{
    print("CLICK");
    other.gameObject.transform.Rotate(1f,1f,1f);
}

}
