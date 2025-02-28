using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Element : MonoBehaviour
{
    public GameObject Element_Table;
    //public Image menu;
    bool mousePressed = false;
    public TMP_Text S_Name;
    public TMP_Text F_Name;
    public TMP_Text Number;

    // Element data
    public string symbol;
    public string fullName;
    public int atomicNumber;
    public float atomicMass;
    public string metalType;
    public float meltingPoint;
    public float boilingPoint;
    public int discoveryYear;

    // Additional information fields
    public TMP_Text atomicMassText; // AtomicMassText
    public TMP_Text metalTypeText; // MetalTypeText
    public TMP_Text meltingPointText; // MeltingPointText
    public TMP_Text boilingPointText; // BoilingPointText
    public TMP_Text discoveryYearText; // DiscoveryYearText

    Vector3 currentScale = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        currentScale = Element_Table.transform.localScale;
        atomicMassText.gameObject.SetActive(false);
        metalTypeText.gameObject.SetActive(false);
        meltingPointText.gameObject.SetActive(false);
        boilingPointText.gameObject.SetActive(false);
        discoveryYearText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseOver()
    {

        Element_Table.transform.localScale = new Vector3(3f, 3f, 3f);
        atomicMassText.gameObject.SetActive(true);
        metalTypeText.gameObject.SetActive(false);
        meltingPointText.gameObject.SetActive(true);
        boilingPointText.gameObject.SetActive(true);
        discoveryYearText.gameObject.SetActive(true);

        // Show additional information on the UI (atomic mass, metal type, etc.)
        atomicMassText.text = atomicMass.ToString();
        meltingPointText.text = "Melts: " + meltingPoint.ToString() + " °C";
        boilingPointText.text = "Boils: " + boilingPoint.ToString() + " °C";
        discoveryYearText.text = "Discovery Year: " + discoveryYear.ToString();
        if (mousePressed)
        {
            
            //menu.rectTransform.anchoredPosition = new Vector3(995, 3, 0);
        }

    }
    private void OnMouseExit()
    {
        Element_Table.transform.localScale = currentScale;
        atomicMassText.gameObject.SetActive(false);
        metalTypeText.gameObject.SetActive(false);
        meltingPointText.gameObject.SetActive(false);
        boilingPointText.gameObject.SetActive(false);
        discoveryYearText.gameObject.SetActive(false);

    }

    private void OnMouseDown()
    {
        mousePressed = true;
    }

}