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
    List<GameObject> Atoms;
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
                else if (b.Value != 1 && b.Key < 0 && !getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add(getSpeacialName(a.Key, pNames) + $"<sub>{b.Value}</sub>");
                }
                else if (b.Value != 1 && b.Key < 0 && getSpecialAtom(ElementsWithIndex, a.Key))
                {
                    names.Add("(" + getSpeacialName(a.Key, pNames) + ")" + $"<sub>{b.Value}</sub>");
                }

            }
        }

        foreach (var a in names)
        {
            Text += a;
        }

        Debug.Log(Text);

        return Text;

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

    List<string> Positive_Elements = new List<string>();
    List<string> Negative_Elements = new List<string>();

    private void AddElements()
    {
        //Metals
        Positive_Elements.Add("Li");
        Positive_Elements.Add("K");
        Positive_Elements.Add("Ba");
        Positive_Elements.Add("Ca");
        Positive_Elements.Add("Na");
        Positive_Elements.Add("Mg");
        Positive_Elements.Add("Al");
        Positive_Elements.Add("Zn");
        Positive_Elements.Add("Fe");
        Positive_Elements.Add("Ni");
        Positive_Elements.Add("Sn");
        Positive_Elements.Add("Pb");
        Positive_Elements.Add("Hydrogen");
        Positive_Elements.Add("Cu");
        Positive_Elements.Add("Hg");
        Positive_Elements.Add("Ag");
        Positive_Elements.Add("Au");

        //Non Metals
        Negative_Elements.Add("null");
        Negative_Elements.Add("I");
        Negative_Elements.Add("Br");
        Negative_Elements.Add("Cl");
        Negative_Elements.Add("OH");
        Negative_Elements.Add("SO4");
        Negative_Elements.Add("SO3");
        Negative_Elements.Add("NO3");
    }

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

        for (int i = 0; i < Positive_Elements.Count; i++)
        {
            for (int y = 1; y < atoms.Length; y++)
            {
                if (atoms[y].Contains(Positive_Elements[i]))
                {
                    FindAtoms.Add(Positive_Elements[i]);
                }
            }
        }

        for (int i = 0; i < Negative_Elements.Count; i++)
        {
            for (int y = 1; y < atoms.Length; y++)
            {
                if (atoms[y].Contains(Negative_Elements[i]))
                {
                    FindAtoms.Add(Negative_Elements[i]);
                }
            }
        }
        //The findAtoms list should look like this metal, nonmetal, metal

        
        //CheckElementsMetals(FindAtoms[0]);

        //CheckElementsMetals(FindAtoms[2]);
        //CheckElementsNonMetals(FindAtoms[1]);

        SomeLogic(FindAtoms);

        List<string> equations = new List<string>();
        //equations.Add(CreateEquation());
       // text.text += $" + {FindAtoms[0]}";
        /*foreach (var a in FindAtoms)
        {
            CheckElementsMetals(a);
            CheckElementsNonMetals(a);
            equations.Add(CreateEquation());
        }*/
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

    public string CreateEquation()//not used rn
    {
        //SolveChemEqation();

        return null;
    }

    private void SomeLogic(List<string> FindAtoms)
    {
        
        string a = SolveChemEqation(CheckElementsMetals(FindAtoms[0]),CheckElementsNonMetals(FindAtoms[1]));
        string b = SolveChemEqation(CheckElementsMetals(FindAtoms[2]), CheckElementsNonMetals(FindAtoms[1]));

        List<int> el = new List<int>();
        foreach(var c in atoms_Script)
        {
            el.Add(c.Electrons);
        }

        string LeftSide = $"{a} + {atoms_Script[2].NameForPresentation}<sup>0</sup>";
        string RightSide =  $"{b} + {atoms_Script[0].NameForPresentation}<sup>0</sup>";

        
        //ChemicalEquationBalancer ch = new ChemicalEquationBalancer(text);
        //ch.Solve(e);
        //text.text = e;
    }

    void BalancingSide()
    {
        
    }


    //for balancing both sides need to be equal




}

class ElementInfo
{
    public string Name { get; set; }
    public int Valency { get; set; }

    public ElementInfo(string name, int valency)
    {
        this.Name = name;
        this.Valency = valency;
    }
}

#region 
class ChemicalEquationBalancer
{
    public TMP_Text tMP_Text { get; set; }

    public ChemicalEquationBalancer(TMP_Text tMP_Text)
    {
        this.tMP_Text = tMP_Text;
    }
    public void Solve(string equation)
    {
        try
        {
            BalanceEquation(equation);
        }
        catch (Exception ex)
        {
            Debug.Log($"Error: {ex.Message}");
        }
    }

    void BalanceEquation(string equation)
    {
        var sides = equation.Split("->", StringSplitOptions.RemoveEmptyEntries);
        if (sides.Length != 2)
            throw new Exception("Invalid equation format. Use 'reactants -> products'.");

        var reactants = ParseCompounds(sides[0]);
        var products = ParseCompounds(sides[1]);

        var allElements = reactants.SelectMany(c => c.ElementCounts.Keys)
                                   .Union(products.SelectMany(c => c.ElementCounts.Keys))
                                   .Distinct()
                                   .ToList();

        var coefficients = Balance(reactants, products, allElements);

        PrintBalancedEquation(reactants, products, coefficients);
    }

    static List<ChemicalCompound> ParseCompounds(string side)
    {
        return side.Split('+', StringSplitOptions.RemoveEmptyEntries)
                   .Select(compound => new ChemicalCompound(compound.Trim()))
                   .ToList();
    }

    static int[] Balance(List<ChemicalCompound> reactants, List<ChemicalCompound> products, List<string> allElements)
    {
        int n = reactants.Count + products.Count;
        int m = allElements.Count;

        int[,] matrix = new int[m, n];

        for (int i = 0; i < reactants.Count; i++)
        {
            foreach (var kvp in reactants[i].ElementCounts)
            {
                int elementIndex = allElements.IndexOf(kvp.Key);
                matrix[elementIndex, i] = kvp.Value;
            }
        }

        for (int i = 0; i < products.Count; i++)
        {
            foreach (var kvp in products[i].ElementCounts)
            {
                int elementIndex = allElements.IndexOf(kvp.Key);
                matrix[elementIndex, reactants.Count + i] = -kvp.Value;
            }
        }

        int[] coefficients = SolveLinearEquations(matrix);
        return coefficients;
    }

    static int[] SolveLinearEquations(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        int[] solution = new int[cols];
        for (int i = 0; i < cols; i++) solution[i] = 1; // Default coefficients

        for (int row = 0; row < rows; row++)
        {
            int gcd = 0;
            for (int col = 0; col < cols; col++)
            {
                gcd = GCD(gcd, Math.Abs(matrix[row, col]));
            }

            if (gcd > 0)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] /= gcd;
                }
            }
        }

        for (int col = 0; col < cols; col++)
        {
            solution[col] = 1;
            for (int row = 0; row < rows; row++)
            {
                if (matrix[row, col] != 0)
                {
                    solution[col] *= Math.Abs(matrix[row, col]);
                }
            }
        }

        int lcm = 1;
        foreach (int coef in solution)
        {
            lcm = LCM(lcm, coef);
        }

        for (int i = 0; i < cols; i++)
        {
            solution[i] = solution[i] * lcm;
        }

        return solution;
    }

    static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static int LCM(int a, int b)
    {
        return (a / GCD(a, b)) * b;
    }

    void PrintBalancedEquation(List<ChemicalCompound> reactants, List<ChemicalCompound> products, int[] coefficients)
    {
        var reactantStrings = reactants.Select((compound, index) => $"{coefficients[index]} {compound.Formula}").ToList();
        var productStrings = products.Select((compound, index) => $"{coefficients[reactants.Count + index]} {compound.Formula}").ToList();

        tMP_Text.text = string.Join(" + ", reactantStrings) + " -> " + string.Join(" + ", productStrings);
    }
}
class ChemicalCompound
{
    public string Formula { get; private set; }
    public Dictionary<string, int> ElementCounts { get; private set; }

    public ChemicalCompound(string formula)
    {
        Formula = formula;
        ElementCounts = ParseElementCounts(formula);
    }

    private Dictionary<string, int> ParseElementCounts(string formula)
    {
        var elementCounts = new Dictionary<string, int>();
        string element = "";
        int count = 0;

        for (int i = 0; i < formula.Length; i++)
        {
            char c = formula[i];

            if (char.IsUpper(c))
            {
                if (!string.IsNullOrEmpty(element))
                {
                    elementCounts[element] = count == 0 ? 1 : count;
                }

                element = c.ToString();
                count = 0;
            }
            else if (char.IsLower(c))
            {
                element += c;
            }
            else if (char.IsDigit(c))
            {
                count = count * 10 + (c - '0');
            }
        }

        if (!string.IsNullOrEmpty(element))
        {
            elementCounts[element] = count == 0 ? 1 : count;
        }

        return elementCounts;
    }
}
#endregion

//1:54 :,( last edited