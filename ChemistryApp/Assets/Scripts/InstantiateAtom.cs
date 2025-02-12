using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Reflection;
using System.Xml.Linq;

public class InstantiateAtom : MonoBehaviour
{
   // public TMP_Dropdown optionMetal;
   // public TMP_Dropdown optionNonMetal;
    public List<GameObject> Metals;
    public List<GameObject> NonMetals;

    public TMP_Text text;

    int Anion;
    int Ion;

    public TMP_InputField Input;
    // Start is called before the first frame update
    void Start()
    {
        Input.onEndEdit.AddListener(SubmitRedox);
        AddElements();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* public void Check()
    {
        int index = optionMetal.value;

        for (int i = 0; i < Metals.Count; i++)
        {
            if (i == index)
            {
                Debug.Log(Metals[i].name);
                //InstantiateObject(i);
            }
        }
    }*/

    public void InstantiateObject(int index)
    {
        Instantiate(Metals[index],new Vector3(-3, Metals[index].transform.position.y, Metals[index].transform.position.z),Quaternion.identity);
    }

    #region
    //Tva nai veroqtno shte trqbva da e v otdelen cs fail.

    public void SolveChemEqation()
    {
        List<GameObject> Atoms = new List<GameObject>();
        Dictionary<string, Dictionary<int, int>> AtomDict = new Dictionary<string, Dictionary<int, int>>();
        List<Atom> AtomScript = new List<Atom>();
       /* Anion = optionNonMetal.value;
        Ion = optionMetal.value;*/

        for (int i = 0; i < Metals.Count; i++)
        {
            if (i == Ion)
            {
               // Debug.Log(Metals[i].name);
                //InstantiateObject(i);
                Atoms.Add(Metals[i]);
            }
        }

        for (int i = 0; i < NonMetals.Count; i++)
        {
            if (i == Anion)
            {
               // Debug.Log(NonMetals[i].name);
                //InstantiateObject(i);
                Atoms.Add(NonMetals[i]);
            }
        }

        for (int i = 0; i < Atoms.Count; i++)
        {
            AtomScript.Add(Atoms[i].GetComponent<Atom>());
        }

        if (AtomScript.Count > 1)
        {
            SortAtoms(AtomScript, AtomDict);
        }
    }


    private void SortAtoms(List<Atom> atomScript, Dictionary<string, Dictionary<int, int>> AtomDict)
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

        ElementsLogic(atomScript, AtomDict);
    }

    private void ElementsLogic(List<Atom> atomScript, Dictionary<string, Dictionary<int,int>> AtomDict)
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
        foreach (var a in AtomDict)
        {
            foreach (var b in a.Value)
            {
               // Debug.Log("Name:" + a.Key + " Valency: " + b.Key + " Index: " + b.Value);
            }
        }


        DisplayElement(AtomDict, atomScript);

    }

    private void DisplayElement(Dictionary<string, Dictionary<int, int>> AtomDict, List<Atom> pNames)
    {
        List<string> ElementsWithIndex = new List<string>() { "OH", "SO<sub>3</sub>", "SO<sub>4</sub>", "NO<sub>3</sub>" };
        List<string> mathc = new List<string>();
        //Da se napravi da gi podrejda pravilno s indeksi s poveche ot 2 elementa.
        text.text = "";

        List<string> names = new List<string>();

        foreach (var a in AtomDict)
        {
            
            foreach (var b in a.Value)
            {
                if (b.Value == 1 && b.Key > 0)
                {
                    names.Add(getSpeacialName(a.Key, pNames));
                }
                else if (b.Value != 1 && b.Key > 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(getSpeacialName(a.Key, pNames) + $"<sub>{b.Value}</sub>");
                }
                else if (b.Value != 1 && b.Key > 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + getSpeacialName(a.Key, pNames) + ")" + $"<sub>{b.Value}</sub>");
                }

            }
        }
        foreach (var a in AtomDict)
        {

            foreach (var b in a.Value)
            {
                if (b.Value == 1 && b.Key < 0)
                {
                    names.Add(getSpeacialName(a.Key, pNames));
                }
                else if (b.Value != 1 && b.Key < 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(getSpeacialName(a.Key, pNames) + $"<sub>{b.Value}</sub>");
                }
                else if (b.Value != 1 && b.Key < 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + getSpeacialName(a.Key, pNames) + ")" + $"<sub>{b.Value}</sub>");
                }

            }
        }

        foreach (var a in names)
        {
            text.text += a;
        }
        Debug.Log(text.text);

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
        foreach (var a in elements)
        {
            if (a == el)
            {
                return true;
            }
        }
        return false;
    }

    private string getSpeacialName(string el, List<Atom> PNames)
    {
        foreach (var a in PNames)
        {
            if (el == a.Name)
            {
                return a.NameForPresentation;
            }
        }
        return "No Matches";
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
        Positive_ElementsEntiPyt.Add("Hydrogen");
        Positive_ElementsEntiPyt.Add("Cu");
        Positive_ElementsEntiPyt.Add("Hg");
        Positive_ElementsEntiPyt.Add("Ag");
        Positive_ElementsEntiPyt.Add("Au");


        Negative_ElementsEntiPyt.Add("null");
        Negative_ElementsEntiPyt.Add("I");
        Negative_ElementsEntiPyt.Add("Br");
        Negative_ElementsEntiPyt.Add("Cl");
        Negative_ElementsEntiPyt.Add("OH");
        Negative_ElementsEntiPyt.Add("SO4");
        Negative_ElementsEntiPyt.Add("SO3");
        Negative_ElementsEntiPyt.Add("NO3");

        //:(
    }

    private void SubmitRedox(string arg0)
    {
        char[] things = {' ', '+'};
        string[] atoms = arg0.Split(things);

        List<string> FindAtoms = new List<string>();

        for(int i = 0; i < Positive_ElementsEntiPyt.Count; i++)
        {
            if (atoms[0].Contains(Positive_ElementsEntiPyt[i]))
            {
                FindAtoms.Add(Positive_ElementsEntiPyt[i]);
            }
        }

        for(int i = 0; i < Negative_ElementsEntiPyt.Count; i++)
        {
            if (atoms[0].Contains(Negative_ElementsEntiPyt[i]))
            {
                FindAtoms.Add(Negative_ElementsEntiPyt[i]);
            }
        }

        for (int i = 0; i < Positive_ElementsEntiPyt.Count; i++)
        {
            for (int y = 1; y < atoms.Length; y++)
            {
                if (atoms[y].Contains(Positive_ElementsEntiPyt[i]))
                {
                    FindAtoms.Add(Positive_ElementsEntiPyt[i]);
                }
            }
        }

        for (int i = 0; i < Negative_ElementsEntiPyt.Count; i++)
        {
            for (int y = 1; y < atoms.Length; y++)
            {
                if (atoms[y].Contains(Negative_ElementsEntiPyt[i]))
                {
                    FindAtoms.Add(Negative_ElementsEntiPyt[i]);
                }
            }
        }
        //The findAtoms list should look like this metal, nonmetal, metal

        CheckElementsMetals(FindAtoms[2]);
        CheckElementsNonMetals(FindAtoms[1]);

        

        List<string> equations = new List<string>();
        equations.Add(CreateEquation());
        text.text += $" + {FindAtoms[0]}";
        /*foreach (var a in FindAtoms)
        {
            CheckElementsMetals(a);
            CheckElementsNonMetals(a);
            equations.Add(CreateEquation());
        }*/
    }

    public void CheckElementsMetals(string Element)
    {
        for(int i = 0; i < Metals.Count; i ++)
        {
            if (Metals[i].GetComponent<Atom>().Name == Element)
            {
                Ion = i;
            }
        }
    }

    public void CheckElementsNonMetals(string Element)
    {
        for (int i = 0; i < NonMetals.Count; i++)
        {
            if (NonMetals[i].GetComponent<Atom>().Name == Element)
            {
                Anion = i;
            }
        }
    }

    public string CreateEquation()
    {
        SolveChemEqation();

        return null;
    }


}
//1:54 :,( last edited