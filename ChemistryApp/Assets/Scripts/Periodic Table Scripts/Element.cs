using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    public GameObject Element_Table;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Element_Table.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    private void OnMouseExit()
    {
        Element_Table.transform.localScale = new Vector3(1, 1, 1);
    }
}
