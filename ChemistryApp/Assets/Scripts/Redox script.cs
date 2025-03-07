using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Redoxscript : MonoBehaviour
{
    public TMP_InputField Input;

    List<string> Positive_ElementsEntiPyt = new List<string>();
    List<string> Negative_ElementsEntiPyt = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Input.onEndEdit.AddListener(SubmitRedox);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddElements()
    {
        Positive_ElementsEntiPyt.Add("Li");
        Positive_ElementsEntiPyt.Add("K");
        Positive_ElementsEntiPyt.Add("Ba");
        Positive_ElementsEntiPyt.Add("Ca");
        Positive_ElementsEntiPyt.Add("Na");
        Positive_ElementsEntiPyt.Add("Mg");
        Positive_ElementsEntiPyt.Add("Al");
        Positive_ElementsEntiPyt.Add("Zn");
        Positive_ElementsEntiPyt.Add("Fe");
        Positive_ElementsEntiPyt.Add("Ni");
        Positive_ElementsEntiPyt.Add("Sn");
        Positive_ElementsEntiPyt.Add("Pb");
        Positive_ElementsEntiPyt.Add("Hydrogen");
        Positive_ElementsEntiPyt.Add("Cu");
        Positive_ElementsEntiPyt.Add("Hg");
        Positive_ElementsEntiPyt.Add("Ag");
        Positive_ElementsEntiPyt.Add("Au");


        Negative_ElementsEntiPyt.Add("Siqra");
        Negative_ElementsEntiPyt.Add("I");
        Negative_ElementsEntiPyt.Add("Br");
        Negative_ElementsEntiPyt.Add("Cl");
        Negative_ElementsEntiPyt.Add("OH");
        Negative_ElementsEntiPyt.Add("SO4");
        Negative_ElementsEntiPyt.Add("SO3");
        Negative_ElementsEntiPyt.Add("NO3");

        
    }

    private void SubmitRedox(string arg0)
    {
        //char[] things = { ' ', '+', '(', ')' };
        //string[] atoms = arg0.Split(things);

        List<string> FindAtoms = new List<string>();

        for (int i = 0; i < Positive_ElementsEntiPyt.Count; i++)
        {
            if (arg0.Contains(Positive_ElementsEntiPyt[i]))
            {
                FindAtoms.Add(Positive_ElementsEntiPyt[i]);
            }
        }

        for (int i = 0; i < Negative_ElementsEntiPyt.Count; i++)
        {
            if (arg0.Contains(Negative_ElementsEntiPyt[i]))
            {
                FindAtoms.Add(Negative_ElementsEntiPyt[i]);
            }
        }


        List<string> equations = new List<string>();
       /* foreach (var a in FindAtoms)
        {
            CheckElementsMetals(a);
            CheckElementsNonMetals(a);
            equations.Add(CreateEquation());
        }*/
    }
}
