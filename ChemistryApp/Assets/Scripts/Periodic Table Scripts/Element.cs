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


    Vector3 currentScale = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        currentScale = Element_Table.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseOver()
    {
        Element_Table.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
        if(mousePressed)
        {
            //menu.rectTransform.anchoredPosition = new Vector3(995, 3, 0);
        }
        
    }
    private void OnMouseExit()
    {
        Element_Table.transform.localScale = currentScale;

    }

    private void OnMouseDown()
    {
        mousePressed = true;
    }



}
