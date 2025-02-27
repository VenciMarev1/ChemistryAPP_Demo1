using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;

public class InstantiateAtom : MonoBehaviour
{
   // public TMP_Dropdown optionMetal;
   // public TMP_Dropdown optionNonMetal;
    public List<GameObject> Metals;
    public List<GameObject> NonMetals;

    public TMP_Text text;
    List<Atom> atoms_Script;

    public TMP_InputField Input;


    Dictionary<string, Dictionary<int, int>> AtomDict;

    List<int> Coefficient;

    List<GameObject> Atoms;
    // Start is called before the first frame update
    void Start()
    {
        Input.onEndEdit.AddListener(SubmitRedox);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateObject(int index)
    {
        Instantiate(Metals[index],new Vector3(-3, Metals[index].transform.position.y, Metals[index].transform.position.z),Quaternion.identity);
    }

    #region

    public string SolveChemEqation(int Ion, int Anion)
    {
        List<GameObject> Atoms = new List<GameObject>();
        AtomDict = new Dictionary<string, Dictionary<int, int>>();
        List<Atom> AtomScript = new List<Atom>();
        Coefficient =  new List<int>();
        /* Anion = optionNonMetal.value;
         Ion = optionMetal.value;*/


        //Checks for metals by their id and adds it to the list with metals
        for (int i = 0; i < Metals.Count; i++)
        {
            if (i == Ion)
            {
                Atoms.Add(Metals[i]);
            }
        }

        //Checks for non metals by their id and adds it to the list with Non metals
        for (int i = 0; i < NonMetals.Count; i++)
        {
            if (i == Anion)
            {
                Atoms.Add(NonMetals[i]);
            }
        }

        //Gets the script from the gameobject
        for (int i = 0; i < Atoms.Count; i++)
        {
            AtomScript.Add(Atoms[i].GetComponent<Atom>());
        }


        if (AtomScript.Count >= 1)
        {
            return ElementsLogic(AtomScript, AtomDict);
        }
        else return "Null";
    }

    private string ElementsLogic(List<Atom> atomScript, Dictionary<string, Dictionary<int,int>> AtomDict)
    {
        //preemption of the electrons/valency
        List<int> electrons = new List<int>();
        foreach (var a in atomScript)
        {
            electrons.Add(Math.Abs(a.Electrons));
        }

        //Putting the information into the dictonary
        for (int i = 0; i < atomScript.Count; i++)
        {
            Dictionary<int, int> valencyAndIndex = new Dictionary<int, int>();
            valencyAndIndex.Add(atomScript[i].Electrons, Math.Abs(ValencyLogic(electrons, atomScript[i].Electrons)));
            AtomDict.Add(atomScript[i].Name, valencyAndIndex);
        }

        //printing onto the debug log
        foreach (var a in AtomDict)
        {
            foreach (var b in a.Value)
            {
                //elInfo.Add(new ElementInfo(a.Key, b.Key * b.Value));
               // Debug.Log("Name:" + a.Key + " Valency: " + b.Key + " Index: " + b.Value);
            }
        }


        return DisplayElement(AtomDict, atomScript);

    }

    private string DisplayElement(Dictionary<string, Dictionary<int, int>> AtomDict, List<Atom> pNames)
    {

        string Text = null;
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
                    Coefficient.Add(1);
                }
                else if (b.Value != 1 && b.Key > 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(getSpeacialName(a.Key, pNames) + $"<sub>{b.Value}</sub>");
                    Coefficient.Add(b.Value);
                }
                else if (b.Value != 1 && b.Key > 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + getSpeacialName(a.Key, pNames) + ")" + $"<sub>{b.Value}</sub>");
                    Coefficient.Add(b.Value);
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
                    Coefficient.Add(b.Value);
                }
                else if (b.Value != 1 && b.Key < 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(getSpeacialName(a.Key, pNames) + $"<sub>{b.Value}</sub>");
                    Coefficient.Add(b.Value);
                }
                else if (b.Value != 1 && b.Key < 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + getSpeacialName(a.Key, pNames) + ")" + $"<sub>{b.Value}</sub>");
                    Coefficient.Add(b.Value);
                }

            }
        }

        foreach (var a in names)
        {
            Text += a;
        }

        Debug.Log(Text);

        return Text;

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
                return a.NameForPresentation + $"<sup>{a.Electrons}</sup>";
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

    private void SubmitRedox(string arg0)
    {
        char[] things = {' ', '+'};
        string[] atoms = arg0.Split(things);

        List<string> FindAtoms = new List<string>();
        atoms_Script = new List<Atom>();

        for (int i = 0; i < Metals.Count; i++)
        {
            string name = Metals[i].GetComponent<Atom>().Name;
            if (atoms[0].Contains(name))
            {
                FindAtoms.Add(name);
            }
        }

        for(int i = 0; i < NonMetals.Count; i++)
        {
            string name = NonMetals[i].GetComponent<Atom>().Name;
            if (atoms[0].Contains(name))
            {
                FindAtoms.Add(name);
            }
        }

        for (int i = 0; i < Metals.Count; i++)
        {
            string name = Metals[i].GetComponent<Atom>().Name;
            for (int y = 1; y < atoms.Length; y++)
            {
                if (atoms[y].Contains(name))
                {
                    FindAtoms.Add(name);
                }
            }
        }

        for (int i = 0; i < NonMetals.Count; i++)
        {
            string name = NonMetals[i].GetComponent<Atom>().Name;
            for (int y = 1; y < atoms.Length; y++)
            {
                if (atoms[y].Contains(name))
                {
                    FindAtoms.Add(name);
                }
            }
        }
        SomeLogic(FindAtoms);
    }

    public int CheckElementsMetals(string Element)
    {
        for(int i = 0; i < Metals.Count; i ++)
        {
            if (Metals[i].GetComponent<Atom>().Name == Element)
            {
                atoms_Script.Add(Metals[i].GetComponent<Atom>());
                return i;
            }
        }
        return -1;
    }

    public int CheckElementsNonMetals(string Element)
    {
        for (int i = 0; i < NonMetals.Count; i++)
        {
            if (NonMetals[i].GetComponent<Atom>().Name == Element)
            {
                atoms_Script.Add(NonMetals[i].GetComponent<Atom>());
                return i;
            }
        }
        return -1;
    }

    private void SomeLogic(List<string> FindAtoms)
    {

        string a = SolveChemEqation(CheckElementsMetals(FindAtoms[0]), CheckElementsNonMetals(FindAtoms[1]));
        string b = SolveChemEqation(CheckElementsMetals(FindAtoms[2]), CheckElementsNonMetals(FindAtoms[1]));

        string LeftSide = $"{a} + {atoms_Script[2].NameForPresentation}<sup>0</sup>";
        string RightSide = $"{b} + {atoms_Script[0].NameForPresentation}<sup>0</sup>";

        text.text = LeftSide + " -> " + RightSide;

    }
    //for balancing both sides need to be equal

    List<int> Balance(int x, int y, int p, int q)
    {
        int a, b, c;

        if (p % x == 0 && q % y == 0)
        {
            a = p / x;
            b = q / x;
            c = 1;
        }
        else
        {
            p = p * x;
            q = q * x;
            c = x * y;

            int temp = GCD(p, GCD(q, c));

            a = p / temp;
            b = q / temp;
            c = c / temp;

        }

        List<int> tmp = new List<int>();
        tmp.Add(a);
        tmp.Add(b);
        tmp.Add(c);

        return tmp;

    }
}