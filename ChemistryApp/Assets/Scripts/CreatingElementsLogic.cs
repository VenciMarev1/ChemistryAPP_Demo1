using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreatingElementsLogic : MonoBehaviour
{
    List<GameObject> Atoms;
    public TMP_Text ElementText;
    List<Atom> AtomScript = new List<Atom>();
    public TMP_Text Atom;

    Dictionary<string, Dictionary<int, int>> AtomDict; //Dictionary<name of the atom, Dictionary<electrons, index>


   // public TMP_InputField InputEquation;

    // Start is called before the first frame update
    void Start()
    {
        var se = new InputField.SubmitEvent();
      //  InputEquation.onEndEdit.AddListener(SubmitRedox);


        Atoms = new List<GameObject>();
        AtomDict = new Dictionary<string, Dictionary<int, int>>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    
    #region Create Element Logic
    //
    //
    public void AddAtoms(GameObject atom)
    {
        Atoms.Add(atom);
        AtomScript.Add(atom.GetComponent<Atom>());
        if(AtomScript.Count > 1)
        {
            SortAtoms(AtomScript);
        }
    }

    public void RemoveAtoms()
    {
        AtomScript = new List<Atom>();
        AtomDict = new Dictionary<string, Dictionary<int, int>>();
        Atoms = new List<GameObject>();
    }

    private void SortAtoms(List<Atom> atomScript)
    {
       /* for (int i = 0; i < atomScript.Count; i++)
        {
            for (int j = 1; j < atomScript.Count - 1; j++)
            {
                if (atomScript[i].Electrons < atomScript[j].Electrons)
                {
                    Atom tmp = atomScript[i];
                    atomScript[i] = atomScript[j];
                    atomScript[j] = tmp;
                }
            }
        }*/

        ElementsLogic(atomScript);
    }

    private void ElementsLogic(List<Atom> atomScript)
    {
        //preemption of the electrons/valency
        List<int> electrons = new List<int>();
        foreach (var a in atomScript)
        {
            electrons.Add(Math.Abs(a.Electrons));
        }

        //Putting the information in the dictonaries
        for (int i = 0; i < atomScript.Count; i++)
        {
            Dictionary<int, int> valencyAndIndex = new Dictionary<int, int>();
            
            valencyAndIndex.Add(atomScript[i].Electrons, Math.Abs(ValencyLogic(electrons, atomScript[i].Electrons)));

            //Ima greshka zashtoto dictionarytata se podrejdat razlichno pri vsqka kompilaciq for some reason. Goadnqri!
            AtomDict.Add(atomScript[i].Name, valencyAndIndex);
        }

        //printing onto the debug log
        foreach(var a in AtomDict)
        {
            foreach(var b in a.Value)
            {
                Debug.Log("Name:" + a.Key + " Valency: " + b.Key + " Index: " + b.Value);
            }
        }

        DisplayElement();

    }

    private void DisplayElement()
    {
        List<string> ElementsWithIndex = new List<string>() { "OH", "SO3", "SO4", "NO3" };
        List<string> mathc = new List<string>();
        //Da se napravi da gi podrejda pravilno s indeksi s poveche ot 2 elementa.
        ElementText.text = "Created element: ";

        List<string> names = new List<string>();

        foreach (var a in AtomDict)
        {

            foreach (var b in a.Value)
            {
                if (b.Value == 1 && b.Key > 0)
                {
                    names.Add(a.Key);
                }
                else if (b.Value != 1 && b.Key > 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(a.Key + b.Value);
                }
                else if(b.Value != 1 && b.Key > 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + a.Key + ")" + b.Value);
                }

            }
        }
        foreach (var a in AtomDict)
        {

            foreach (var b in a.Value)
            {
                if (b.Value == 1 && b.Key < 0)
                {
                    names.Add(a.Key);
                }
                else if (b.Value != 1 && b.Key < 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(a.Key + b.Value);
                }
                else if (b.Value != 1 && b.Key < 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + a.Key + ")" + b.Value);
                }

            }
        }

        foreach(var a in names)
        {
            ElementText.text += a;
        }

       /* foreach (var a in AtomDict)
        {
            foreach (var b in a.Value)
            {
                if (b.Value != 1 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    ElementText.text += "(" + a.Key + ")" + b.Value;
                }
                else if (b.Value != 1 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    ElementText.text += a.Key + b.Value;
                }
                else if (b.Value == 1 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    ElementText.text += "(" + a.Key + ")";
                }
                else if (b.Value == 1 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    ElementText.text += a.Key;
                }
            }
        }*/

    }

    private bool getSpecialAtom(List<string> elements, string el)
    {
        foreach(var a in elements)
        {
            if(a == el)
            {
                return true;
            }
        }
        return false;
    }

    private int ValencyLogic(List<int> electrons, int el)
    {
        int lcm = FindLCM(electrons);
        int value = lcm / el;
        return value;
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // Function to calculate LCM of two numbers
    private int LCM(int a, int b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

    // Function to calculate LCM of multiple numbers
    private int FindLCM(List<int> numbers)
    {
        if (numbers.Count == 0)
            throw new ArgumentException("Array must contain at least one number.");

        int lcm = numbers[0];
        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = LCM(lcm, numbers[i]);
        }
        return lcm;
    }
    #endregion 



    //Mrazq da programiram taka....
    List<string> Positive_ElementsEntiPyt = new List<string>();
    List<string> Negative_ElementsEntiPyt = new List<string>();

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
        Positive_ElementsEntiPyt.Add("H");
        Positive_ElementsEntiPyt.Add("Cu");
        Positive_ElementsEntiPyt.Add("Hg");
        Positive_ElementsEntiPyt.Add("Ag");
        Positive_ElementsEntiPyt.Add("Au");


        Negative_ElementsEntiPyt.Add("S");
        Negative_ElementsEntiPyt.Add("I");
        Negative_ElementsEntiPyt.Add("Br");
        Negative_ElementsEntiPyt.Add("Cl");
        Negative_ElementsEntiPyt.Add("OH");
        Negative_ElementsEntiPyt.Add("SO4");
        Negative_ElementsEntiPyt.Add("SO3");
        Negative_ElementsEntiPyt.Add("NO3");

        //:(
    }

    //Zadyljitelno da se smenqt tezi listove sys elementite v ui-a.


    private void SubmitRedox(string arg0)
    {
        Debug.Log(arg0);
        char[] things = {' ', '+', '(', ')'};
        string[] NeSeSeshtamIme = arg0.Split(things);

        foreach(var a in NeSeSeshtamIme)
        {
            Debug.Log(a);
        }


        string[] elements = new string[3];
        int count = 0;
        
        for(int i = 0; i < Positive_ElementsEntiPyt.Count; i++)
        {
            for(int y = 0; i < NeSeSeshtamIme.Length; i++)
            {
                if (Positive_ElementsEntiPyt[i] == NeSeSeshtamIme[y])
                {
                    elements[count] = NeSeSeshtamIme[y];
                    count++;
                }
            }
        }

        for (int i = 0; i < Negative_ElementsEntiPyt.Count; i++)
        {
            for (int y = 0; i < NeSeSeshtamIme.Length; i++)
            {
                if (Negative_ElementsEntiPyt[i] == NeSeSeshtamIme[y])
                {
                    elements[count] = NeSeSeshtamIme[y];
                    count++;
                }
            }
        }

    }
}


// last edited: 0:30pm 28.01.2023.
// last edited: 22:22 10.02.2023.


//Idei
/* Moje s drop down menu da napravq da se vyvejda vseki element
 * ot tam v logikata gi razmenqm ako moje. (mai ne e dobra idea zashtoto shte trqbva rychno vseki element da pisha i na moje primerno da ne obrazuvat veshtestva.)
 * 
 * wdyif geqfju yjouegiuoetrynfua sdcjkcxdsalkhcdsafcdsacdk
 * 
 * Ne go pravi mnogo slojno!
 * 
 * 
 * 
 * 
 */
