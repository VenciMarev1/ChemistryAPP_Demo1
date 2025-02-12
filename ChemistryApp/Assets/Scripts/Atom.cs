using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Atom : MonoBehaviour
{
    public GameObject AtomGO;
    public string Name;
    public string NameForPresentation;
    public int Electrons;
    public TMP_Text Text_ForSelected;

    private Camera mainCamera;
    private float CameraZDistance;

    public TMP_Text NameCreatedElement;

    public CreatingElementsLogic crel;

    public bool HasIndex;
    void Start()
    {
        //AtomGO = GetComponent<GameObject>();
        mainCamera = Camera.main;
        CameraZDistance =
            mainCamera.WorldToScreenPoint(transform.position).z; //z axis of the game object for screen view

       // crel = GetComponent<CreatingElementsLogic>();
    }

    private float counter = 0;

    private void OnMouseDrag()
    {
        Vector3 ScreenPosition =
        new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance); //z axis added to screen point

        Vector3 NewWorldPosition =
            mainCamera.ScreenToWorldPoint(ScreenPosition); //Screen point converted to world point

        transform.position = NewWorldPosition;
    }



    private void OnMouseOver()
    {
        Text_ForSelected.text = "Name: " + Name;
    }

    private void OnMouseExit()
    {
        Text_ForSelected.text = "Name: ";
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject secondaryAtom = collision.gameObject;
        AtomGO.transform.position = new Vector3(secondaryAtom.transform.position.x - 0.5f, AtomGO.transform.position.y, AtomGO.transform.position.z);

        crel.AddAtoms(secondaryAtom);
        Debug.Log("Triggered");

    }
    private void OnTriggerExit(Collider collision)
    {
        NameCreatedElement.text = "Created element: ";
        crel.RemoveAtoms();
        Debug.Log("Unitrggered");
    }
}
