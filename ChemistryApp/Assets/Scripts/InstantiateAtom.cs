using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstantiateAtom : MonoBehaviour
{
    public TMP_Dropdown optionMetal;
    public TMP_Dropdown optionNonMetal;
    public List<GameObject> Metals;
    public List<GameObject> NonMetals;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        int index = optionMetal.value;

        for (int i = 0; i < Metals.Count; i++)
        {
            if (i == index)
            {
                Debug.Log(Metals[i].name);
                InstantiateObject(i);
            }
        }
    }

    public void InstantiateObject(int index)
    {
        Instantiate(Metals[index],new Vector3(-3, Metals[index].transform.position.y, Metals[index].transform.position.z),Quaternion.identity);
    }
}
