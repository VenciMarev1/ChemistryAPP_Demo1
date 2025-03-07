using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;

public class InstantiateAtom : MonoBehaviour
{
    public List<GameObject> Metals;
    public List<GameObject> NonMetals;

    public TMP_Text text;
    List<Atom> atoms_Script;

    public TMP_InputField Input;

    public TextMeshProUGUI reduckiq;
    public Image bgRed;
    public TextMeshProUGUI okislenie;
    public Image bgOx;

    Dictionary<string, Dictionary<int, int>> AtomDict;

    List<int> Coefficient;

    List<GameObject> Atoms;

    List<string> ROAM = new List<string>() { "Li", "K", "Ba", "Ca", "Na", "Mg", "Al", "Zn", "Fe", "Ni", "Sn", "Pb", "H", "Cu", "Hg", "Ag", "Au" };

    void Start()
    {
        Input.onEndEdit.AddListener(SubmitRedox);
    }

    void Update()
    {

    }

    public void InstantiateObject(int index)
    {
        Instantiate(Metals[index], new Vector3(-3, Metals[index].transform.position.y, Metals[index].transform.position.z), Quaternion.identity);
    }

    public string SolveChemEqation(int Ion, int Anion)
    {
        List<GameObject> Atoms = new List<GameObject>();
        AtomDict = new Dictionary<string, Dictionary<int, int>>();
        List<Atom> AtomScript = new List<Atom>();
        Coefficient = new List<int>();

        // Checks for metals by their id and adds it to the list with metals
        for (int i = 0; i < Metals.Count; i++)
        {
            if (i == Ion)
            {
                Atoms.Add(Metals[i]);
            }
        }

        // Checks for non-metals by their id and adds it to the list with non-metals
        for (int i = 0; i < NonMetals.Count; i++)
        {
            if (i == Anion)
            {
                Atoms.Add(NonMetals[i]);
            }
        }

        // Gets the script from the gameobject
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

    private string ElementsLogic(List<Atom> atomScript, Dictionary<string, Dictionary<int, int>> AtomDict)
    {
        // Preemption of the electrons/valency
        List<int> electrons = new List<int>();
        foreach (var a in atomScript)
        {
            electrons.Add(Math.Abs(a.Electrons));
        }

        // Putting the information into the dictionary
        for (int i = 0; i < atomScript.Count; i++)
        {
            Dictionary<int, int> valencyAndIndex = new Dictionary<int, int>();
            valencyAndIndex.Add(atomScript[i].Electrons, Math.Abs(ValencyLogic(electrons, atomScript[i].Electrons)));
            AtomDict.Add(atomScript[i].Name, valencyAndIndex);
        }

        return DisplayElement(AtomDict, atomScript);
    }

    private string DisplayElement(Dictionary<string, Dictionary<int, int>> AtomDict, List<Atom> pNames)
    {
        string Text = null;
        List<string> ElementsWithIndex = new List<string>() { "OH", "SO", "SO4", "NO" };
        List<string> names = new List<string>();

        foreach (var a in AtomDict)
        {
            foreach (var b in a.Value)
            {
                if (b.Value == 1 && b.Key > 0)
                {
                    names.Add(pNames[getSpeacialName(a.Key, pNames)].NameForPresentation + $"<sup>{pNames[getSpeacialName(a.Key, pNames)].Electrons}</sup>");
                    Coefficient.Add(1);
                }
                else if (b.Value != 1 && b.Key > 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(pNames[getSpeacialName(a.Key, pNames)].NameForPresentation + $"<sup>{pNames[getSpeacialName(a.Key, pNames)].Electrons}</sup>" + $"<sub>{b.Value}</sub>");
                    Coefficient.Add(b.Value);
                }
                else if (b.Value != 1 && b.Key > 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + pNames[getSpeacialName(a.Key, pNames)].NameForPresentation + ")" + $"<sup>{pNames[getSpeacialName(a.Key, pNames)].Electrons}</sup>" + $"<sub>{b.Value}</sub>");
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
                    names.Add(pNames[getSpeacialName(a.Key, pNames)].NameForPresentation + $"<sup>{pNames[getSpeacialName(a.Key, pNames)].Electrons}</sup>");
                    Coefficient.Add(b.Value);
                }
                else if (b.Value != 1 && b.Key < 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(pNames[getSpeacialName(a.Key, pNames)].NameForPresentation + $"<sup>{pNames[getSpeacialName(a.Key, pNames)].Electrons}</sup>" + $"<sub>{b.Value}</sub>");
                    Coefficient.Add(b.Value);
                }
                else if (b.Value != 1 && b.Key < 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + pNames[getSpeacialName(a.Key, pNames)].NameForPresentation + ")" + $"<sup>{pNames[getSpeacialName(a.Key, pNames)].Electrons}</sup>" + $"<sub>{b.Value}</sub>");
                    Coefficient.Add(b.Value);
                }
            }
        }

        foreach (var a in names)
        {
            Text += a;
        }

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

    private int getSpeacialName(string el, List<Atom> PNames)
    {
        foreach (var a in PNames)
        {
            if (el == a.Name)
            {
                return PNames.IndexOf(a);
            }
        }
        return -1;
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

    private int LCM(int a, int b)
    {
        return Math.Abs(a * b) / GCD(a, b);
    }

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

    private void SubmitRedox(string arg0)
    {
        char[] things = { ' ', '+' };
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

        for (int i = 0; i < NonMetals.Count; i++)
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

        if (ROAM.IndexOf(FindAtoms[0]) > ROAM.IndexOf(FindAtoms[2]))
        {
            BalanceChemicalEquation(FindAtoms);
        }
        else
        {
            text.text = "The reaction is impossible";
        }
    }

    public int CheckElementsMetals(string Element)
    {
        for (int i = 0; i < Metals.Count; i++)
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

    private void BalanceChemicalEquation(List<string> FindAtoms)
    {

        // Update the text display with balanced equation
        string a = SolveChemEqation(CheckElementsMetals(FindAtoms[0]), CheckElementsNonMetals(FindAtoms[1]));
        string b = SolveChemEqation(CheckElementsMetals(FindAtoms[2]), CheckElementsNonMetals(FindAtoms[1]));

        // Get the electrons involved in the reaction
        int e1 = Math.Abs(atoms_Script[0].Electrons); // First metal electrons
        int e2 = Math.Abs(atoms_Script[2].Electrons); // Second metal electrons

        // Calculate LCM of electrons to get balanced coefficients
        List<Nullable<int>> balancedCoefficients = Balance(e1, e2);

        for(int i = 0; i < balancedCoefficients.Count; i++)
        {
            if (balancedCoefficients[i] == 1)
            {
                balancedCoefficients[i] = null;
            }
        }


        string LeftSide = $"{balancedCoefficients[0]}{a} + {balancedCoefficients[1]}{atoms_Script[2].NameForPresentation}<sup>0</sup>";
        string RightSide = $"{balancedCoefficients[1]}{b} + {balancedCoefficients[0]}{atoms_Script[0].NameForPresentation}<sup>0</sup>";

        text.text = LeftSide + " -> " + RightSide;

        // Update redox equations with balanced coefficients
        UpdateRedoxEquations(FindAtoms, balancedCoefficients);
    }

    private void UpdateRedoxEquations(List<string> FindAtoms, List<Nullable<int>> coefficients)
    {
        Atom RED = Metals[CheckElementsMetals(FindAtoms[0])].GetComponent<Atom>();
        Atom OX = Metals[CheckElementsMetals(FindAtoms[2])].GetComponent<Atom>();


        bgRed.gameObject.SetActive(true);
        reduckiq.text = $"R: {coefficients[0]}{RED.NameForPresentation}<sup>{RED.Electrons}</sup> - {coefficients[0] +" "+ RED.Electrons}e<sup>-</sup> -> {coefficients[0]}{RED.NameForPresentation}<sup>0</sup>";

        bgOx.gameObject.SetActive(true);
        okislenie.text = $"O: {coefficients[1]}{OX.NameForPresentation}<sup>0</sup> + {coefficients[1] +" "+ OX.Electrons}e<sup>-</sup> -> {coefficients[1]}{OX.NameForPresentation}<sup>{OX.Electrons}</sup>";
    }

    private List<Nullable<int>> Balance(int e1, int e2)
    {
        // e1 = electrons for first metal (reducing agent)
        // e2 = electrons for second metal (oxidizing agent)

        // Step 1: Calculate number of electrons transferred
        int electronsGiven = Math.Abs(e1);    // electrons given by reducing agent
        int electronsAccepted = Math.Abs(e2); // electrons accepted by oxidizing agent

        // Step 2: Find LCM to ensure electron transfer balance
        int electronLCM = LCM(electronsGiven, electronsAccepted);

        // Step 3: Calculate initial coefficients
        int a = electronLCM / electronsGiven;    // coefficient for reducing agent
        int b = electronLCM / electronsAccepted; // coefficient for oxidizing agent

        // Step 4: Simplify final coefficients
        int finalGCD = GCD(a, b);
        a /= finalGCD;
        b /= finalGCD;

        return new List<Nullable<int>> { a, b };
    }
}