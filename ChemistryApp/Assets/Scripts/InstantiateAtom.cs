using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Reflection;

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
                //InstantiateObject(i);
            }
        }
    }

    public void InstantiateObject(int index)
    {
        Instantiate(Metals[index],new Vector3(-3, Metals[index].transform.position.y, Metals[index].transform.position.z),Quaternion.identity);
    }


    //Tva nai veroqtno shte trqbva da e v otdelen cs fail.

    public void SolveChemEqation()
    {
        List<GameObject> Atoms = new List<GameObject>();
        Dictionary<string, Dictionary<int, int>> AtomDict = new Dictionary<string, Dictionary<int, int>>();
        List<Atom> AtomScript = new List<Atom>();
        int Anion = optionNonMetal.value;
        int Ion = optionMetal.value;

        for (int i = 0; i < Metals.Count; i++)
        {
            if (i == Ion)
            {
                Debug.Log(Metals[i].name);
                //InstantiateObject(i);
                Atoms.Add(Metals[i]);
            }
        }

        for (int i = 0; i < NonMetals.Count; i++)
        {
            if (i == Anion)
            {
                Debug.Log(NonMetals[i].name);
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
                Debug.Log("Name:" + a.Key + " Valency: " + b.Key + " Index: " + b.Value);
            }
        }

        DisplayElement(AtomDict);

    }

    private void DisplayElement(Dictionary<string, Dictionary<int, int>> AtomDict)
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
                    names.Add(a.Key);
                }
                else if (b.Value != 1 && b.Key > 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(a.Key + $"<sub>{b.Value}</sub>");
                }
                else if (b.Value != 1 && b.Key > 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + a.Key + ")" + $"<sub>{b.Value}</sub>");
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
                    names.Add(a.Key + $"<sub>{b.Value}</sub>");
                }
                else if (b.Value != 1 && b.Key < 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + a.Key + ")" + $"<sub>{b.Value}</sub>");
                }

            }
        }

        foreach (var a in names)
        {
            text.text += a;
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
        foreach (var a in elements)
        {
            if (a == el)
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

}
