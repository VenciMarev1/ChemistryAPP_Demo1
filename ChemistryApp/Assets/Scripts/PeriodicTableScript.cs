using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PeriodicTableLayout : MonoBehaviour
{
    [Header("Element Prefabs (ordered by atomic number)")]
    public List<GameObject> elementPrefabs = new List<GameObject>();
    // List<GameObject> El = new List<GameObject>();

    [Header("Periodic Table Parent Object")]
    public GameObject periodicTableParent;

    [Header("Element Colors by Type")]
    public Color alkaliMetalsColor;
    public Color alkalineEarthMetalsColor;
    public Color transitionMetalsColor;
    public Color postTransitionMetalsColor;
    public Color metalloidsColor;
    public Color reactiveNonMetalsColor;
    public Color nobleGasesColor;
    public Color lanthanidesColor;
    public Color actinidesColor;
    public Color unknownPropertiesColor;

    // Mapping of atomic number to element type
    private string[] elementTypes = new string[]
    {
        "Reactive non-metals",   // 1 - Hydrogen
        "Noble gases",           // 2 - Helium
        "Alkali metals",         // 3 - Lithium
        "Alkaline earth metals", // 4 - Beryllium
        "Metalloids",            // 5 - Boron
        "Reactive non-metals",   // 6 - Carbon
        "Reactive non-metals",   // 7 - Nitrogen
        "Reactive non-metals",   // 8 - Oxygen
        "Reactive non-metals",   // 9 - Fluorine
        "Noble gases",           // 10 - Neon
        "Alkali metals",         // 11 - Sodium
        "Alkaline earth metals", // 12 - Magnesium
        "Post-transition metals",// 13 - Aluminum
        "Metalloids",            // 14 - Silicon
        "Reactive non-metals",   // 15 - Phosphorus
        "Reactive non-metals",   // 16 - Sulfur
        "Reactive non-metals",   // 17 - Chlorine
        "Noble gases",           // 18 - Argon
        "Alkali metals",         // 19 - Potassium
        "Alkaline earth metals", // 20 - Calcium
        "Transition metals",     // 21 - Scandium
        "Transition metals",     // 22 - Titanium
        "Transition metals",     // 23 - Vanadium
        "Transition metals",     // 24 - Chromium
        "Transition metals",     // 25 - Manganese
        "Transition metals",     // 26 - Iron
        "Transition metals",     // 27 - Cobalt
        "Transition metals",     // 28 - Nickel
        "Transition metals",     // 29 - Copper
        "Post-transition metals",// 30 - Zinc
        "Post-transition metals",// 31 - Gallium
        "Metalloids",            // 32 - Germanium
        "Metalloids",            // 33 - Arsenic
        "Reactive non-metals",   // 34 - Selenium
        "Reactive non-metals",   // 35 - Bromine
        "Noble gases",           // 36 - Krypton
        "Alkali metals",         // 37 - Rubidium
        "Alkaline earth metals", // 38 - Strontium
        "Transition metals",     // 39 - Yttrium
        "Transition metals",     // 40 - Zirconium
        "Transition metals",     // 41 - Niobium
        "Transition metals",     // 42 - Molybdenum
        "Transition metals",     // 43 - Technetium
        "Transition metals",     // 44 - Ruthenium
        "Transition metals",     // 45 - Rhodium
        "Transition metals",     // 46 - Palladium
        "Transition metals",     // 47 - Silver
        "Post-transition metals",// 48 - Cadmium
        "Post-transition metals",// 49 - Indium
        "Post-transition metals",// 50 - Tin
        "Metalloids",            // 51 - Antimony
        "Metalloids",            // 52 - Tellurium
        "Reactive non-metals",   // 53 - Iodine
        "Noble gases",           // 54 - Xenon
        "Alkali metals",         // 55 - Cesium
        "Alkaline earth metals", // 56 - Barium
        "Transition metals",     // 57 - Lanthanum
        "Lanthanides",           // 58 - Cerium
        "Lanthanides",           // 59 - Praseodymium
        "Lanthanides",           // 60 - Neodymium
        "Lanthanides",           // 61 - Promethium
        "Lanthanides",           // 62 - Samarium
        "Lanthanides",           // 63 - Europium
        "Lanthanides",           // 64 - Gadolinium
        "Lanthanides",           // 65 - Terbium
        "Lanthanides",           // 66 - Dysprosium
        "Lanthanides",           // 67 - Holmium
        "Lanthanides",           // 68 - Erbium
        "Lanthanides",           // 69 - Thulium
        "Lanthanides",           // 70 - Ytterbium
        "Lanthanides",           // 71 - Lutetium
        "Transition metals",     // 72 - Hafnium
        "Transition metals",     // 73 - Tantalum
        "Transition metals",     // 74 - Tungsten
        "Transition metals",     // 75 - Rhenium
        "Transition metals",     // 76 - Osmium
        "Transition metals",     // 77 - Iridium
        "Transition metals",     // 78 - Platinum
        "Post-transition metals",// 79 - Gold
        "Post-transition metals",// 80 - Mercury
        "Post-transition metals",// 81 - Thallium
        "Post-transition metals",// 82 - Lead
        "Post-transition metals",// 83 - Bismuth
        "Post-transition metals",// 84 - Polonium
        "Noble gases",           // 85 - Astatine
        "Noble gases",           // 86 - Radon
        "Alkali metals",         // 87 - Francium
        "Alkaline earth metals", // 88 - Radium
        "Transition metals",     // 89 - Actinium
        "Actinides",             // 90 - Thorium
        "Actinides",             // 91 - Protactinium
        "Actinides",             // 92 - Uranium
        "Actinides",             // 93 - Neptunium
        "Actinides",             // 94 - Plutonium
        "Actinides",             // 95 - Americium
        "Actinides",             // 96 - Curium
        "Actinides",             // 97 - Berkelium
        "Actinides",             // 98 - Californium
        "Actinides",             // 99 - Einsteinium
        "Actinides",             // 100 - Fermium
        "Actinides",             // 101 - Mendelevium
        "Actinides",             // 102 - Nobelium
        "Actinides",             // 103 - Lawrencium
        "Transition metals",     // 104 - Rutherfordium
        "Transition metals",     // 105 - Dubnium
        "Transition metals",     // 106 - Seaborgium
        "Transition metals",     // 107 - Bohrium
        "Transition metals",     // 108 - Hassium
        "Unknown properties",    // 109 - Meitnerium
        "Unknown properties",    // 110 - Darmstadtium
        "Unknown properties",    // 111 - Roentgenium
        "Unknown properties",    // 112 - Copernicium
        "Unknown properties",    // 113 - Nihonium
        "Unknown properties",    // 114 - Flerovium
        "Unknown properties",    // 115 - Moscovium
        "Unknown properties",    // 116 - Livermorium
        "Unknown properties",    // 117 - Tennessine
        "Unknown properties"     // 118 - Oganesson
    };


    void BuildPeriodicTable()
    {
        GameObject element = new GameObject();
        for (int i = 0; i < elementPrefabs.Count; i++)
        {
            int atomicNumber = i + 1;
            Vector2 position = GetElementPosition(atomicNumber);
            element.transform.localPosition = new Vector3(position.x * 100, position.y * -100, 0); // Adjust the spacing multiplier as needed

            TextMeshProUGUI[] textBoxes = element.GetComponentsInChildren<TextMeshProUGUI>();
            if (textBoxes.Length >= 3)
            {
                textBoxes[0].text = atomicNumber.ToString();
                textBoxes[1].text = GetElementSymbol(atomicNumber);
                textBoxes[2].text = GetElementFullName(atomicNumber);
            }

            AssignElementColor(element, elementTypes[atomicNumber - 1]);
        }
    }

    void AssignElementColor(GameObject element, string type)
    {
        Renderer renderer = element.GetComponent<Renderer>();
        Color elementColor = Color.white;

        switch (type)
        {
            case "Alkali metals":
                elementColor = alkaliMetalsColor;
                break;
            case "Alkaline earth metals":
                elementColor = alkalineEarthMetalsColor;
                break;
            case "Transition metals":
                elementColor = transitionMetalsColor;
                break;
            case "Post-transition metals":
                elementColor = postTransitionMetalsColor;
                break;
            case "Metalloids":
                elementColor = metalloidsColor;
                break;
            case "Reactive non-metals":
                elementColor = reactiveNonMetalsColor;
                break;
            case "Noble gases":
                elementColor = nobleGasesColor;
                break;
            case "Lanthanides":
                elementColor = lanthanidesColor;
                break;
            case "Actinides":
                elementColor = actinidesColor;
                break;
            case "Unknown properties":
                elementColor = unknownPropertiesColor;
                break;
        }

        renderer.material.color = elementColor;
    }

    Vector2 GetElementPosition(int atomicNumber)
    {
        // Define your element positions based on their periodic table layout.
        // Here's a basic structure; adjust it based on your table grid system.
        if (atomicNumber == 1) return new Vector2(0, 0); // Example: Hydrogen
        if (atomicNumber == 2) return new Vector2(17, 0); // Example: Helium
        // Add more positioning logic here...
        return new Vector2(atomicNumber % 18, atomicNumber / 18); // Example: Fill the table left to right.
    }

    string GetElementSymbol(int atomicNumber)
    {
        string[] elementSymbols = new string[]
        {
            "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne",
            "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca",
            "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn",
            "Ga", "Ge", "As", "Se", "Br", "Kr", "Rb", "Sr", "Y", "Zr",
            "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn",
            "Sb", "Te", "I", "Xe", "Cs", "Ba", "La", "Ce", "Pr", "Nd",
            "Pm", "Sm", "Eu", "Gd", "Tb", "Dy", "Ho", "Er", "Tm", "Yb",
            "Lu", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg",
            "Tl", "Pb", "Bi", "Po", "At", "Rn", "Fr", "Ra", "Ac", "Th",
            "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es", "Fm",
            "Md", "No", "Lr", "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds",
            "Rg", "Cn", "Nh", "Fl", "Mc", "Lv", "Ts", "Og"
        };
        return elementSymbols[atomicNumber-1];
    }

    string GetElementFullName(int atomicNumber)
    {
        string[] elementFullNames = new string[]
        {
            "Hydrogen", "Helium", "Lithium", "Beryllium", "Boron", "Carbon", "Nitrogen", "Oxygen", "Fluorine", "Neon",
            "Sodium", "Magnesium", "Aluminium", "Silicon", "Phosphorus", "Sulfur", "Chlorine", "Argon", "Potassium", "Calcium",
            "Scandium", "Titanium", "Vanadium", "Chromium", "Manganese", "Iron", "Cobalt", "Nickel", "Copper", "Zinc",
            "Gallium", "Germanium", "Arsenic", "Selenium", "Bromine", "Krypton", "Rubidium", "Strontium", "Yttrium", "Zirconium",
            "Niobium", "Molybdenum", "Technetium", "Ruthenium", "Rhodium", "Palladium", "Silver", "Cadmium", "Indium", "Tin",
            "Antimony", "Tellurium", "Iodine", "Xenon", "Caesium", "Barium", "Lanthanum", "Cerium", "Praseodymium", "Neodymium",
            "Promethium", "Samarium", "Europium", "Gadolinium", "Terbium", "Dysprosium", "Holmium", "Erbium", "Thulium", "Ytterbium",
            "Lutetium", "Hafnium", "Tantalum", "Tungsten", "Rhenium", "Osmium", "Iridium", "Platinum", "Gold", "Mercury",
            "Thallium", "Lead", "Bismuth", "Polonium", "Astatine", "Radon", "Francium", "Radium", "Actinium", "Thorium",
            "Protactinium", "Uranium", "Neptunium", "Plutonium", "Americium", "Curium", "Berkelium", "Californium", "Einsteinium", "Fermium",
            "Mendelevium", "Nobelium", "Lawrencium", "Rutherfordium", "Dubnium", "Seaborgium", "Bohrium", "Hassium", "Meitnerium",
            "Darmstadtium", "Roentgenium", "Copernicium", "Nihonium", "Flerovium", "Moscovium", "Livermorium", "Tennessine", "Oganesson"
        };
        return elementFullNames[atomicNumber - 1];
    }

   // public Transform periodicTableParent; // Drag the "PeriodicTable" parent object here
    public GameObject elementPrefab; // Assign the prefab for the periodic table elements
    public Vector2 elementSize = new Vector2(1.5f, 1.5f); // Adjust size if needed
    public Vector2 startPosition = new Vector2(-12f, 5f); // Starting point for top-left element (adjust as needed)

    // Define the total number of elements in the periodic table
    private const int totalElements = 118;

    void Start()
    {
        //BuildPeriodicTable();
        CompleteAndPositionElements();
    }

    void CompleteAndPositionElements()
    {
        for (int atomicNumber = 1; atomicNumber <= totalElements; atomicNumber++)
        {
            Transform elementTransform = GetElementTransform1(atomicNumber);

            // If the element prefab doesn't exist in the scene yet, instantiate one
            if (elementTransform == null)
            {
                GameObject newElement = Instantiate(elementPrefab, periodicTableParent.transform);
                newElement.name = atomicNumber + "_" + GetElementName1(atomicNumber); // Name format: "1_Hydrogen"
                newElement.GetComponent<Element>().S_Name.text = GetElementSymbol(atomicNumber);
                newElement.GetComponent<Element>().F_Name.text = GetElementFullName(atomicNumber);
                newElement.GetComponent<Element>().Number.text = atomicNumber.ToString();
                AssignElementColor(newElement, elementTypes[atomicNumber - 1]);

                elementTransform = newElement.transform;
            }
            // Position the element in the correct place
            Vector2 position = GetPositionEL(atomicNumber);
            elementTransform.localPosition = new Vector3(position.x * elementSize.x, -position.y * elementSize.y, 0);
        }
    }

    Transform GetElementTransform1(int atomicNumber)
    {
        foreach (Transform element in periodicTableParent.transform)
        {
            string[] parts = element.name.Split('_');
            if (parts.Length > 1 && int.TryParse(parts[0], out int elementAtomicNumber) && elementAtomicNumber == atomicNumber)
            {
                return element;
            }
        }
        return null; // Element not found
    }

    // Custom method to get the element name based on atomic number
    string GetElementName1(int atomicNumber)
    {
        switch (atomicNumber)
        {
            case 1: return "Hydrogen";
            case 2: return "Helium";
            case 3: return "Lithium";
            case 4: return "Beryllium";
            case 5: return "Boron";
            case 6: return "Carbon";
            case 7: return "Nitrogen";
            case 8: return "Oxygen";
            case 9: return "Fluorine";
            case 10: return "Neon";
            case 11: return "Sodium";
            case 12: return "Magnesium";
            case 13: return "Aluminum";
            case 14: return "Silicon";
            case 15: return "Phosphorus";
            case 16: return "Sulfur";
            case 17: return "Chlorine";
            case 18: return "Argon";
            case 19: return "Potassium";
            case 20: return "Calcium";
            case 21: return "Scandium";
            case 22: return "Titanium";
            case 23: return "Vanadium";
            case 24: return "Chromium";
            case 25: return "Manganese";
            case 26: return "Iron";
            case 27: return "Cobalt";
            case 28: return "Nickel";
            case 29: return "Copper";
            case 30: return "Zinc";
            case 31: return "Gallium";
            case 32: return "Germanium";
            case 33: return "Arsenic";
            case 34: return "Selenium";
            case 35: return "Bromine";
            case 36: return "Krypton";
            case 37: return "Rubidium";
            case 38: return "Strontium";
            case 39: return "Yttrium";
            case 40: return "Zirconium";
            case 41: return "Niobium";
            case 42: return "Molybdenum";
            case 43: return "Technetium";
            case 44: return "Ruthenium";
            case 45: return "Rhodium";
            case 46: return "Palladium";
            case 47: return "Silver";
            case 48: return "Cadmium";
            case 49: return "Indium";
            case 50: return "Tin";
            case 51: return "Antimony";
            case 52: return "Tellurium";
            case 53: return "Iodine";
            case 54: return "Xenon";
            case 55: return "Cesium";
            case 56: return "Barium";
            case 57: return "Lanthanum";
            case 58: return "Cerium";
            case 59: return "Praseodymium";
            case 60: return "Neodymium";
            case 61: return "Promethium";
            case 62: return "Samarium";
            case 63: return "Europium";
            case 64: return "Gadolinium";
            case 65: return "Terbium";
            case 66: return "Dysprosium";
            case 67: return "Holmium";
            case 68: return "Erbium";
            case 69: return "Thulium";
            case 70: return "Ytterbium";
            case 71: return "Lutetium";
            case 72: return "Hafnium";
            case 73: return "Tantalum";
            case 74: return "Tungsten";
            case 75: return "Rhenium";
            case 76: return "Osmium";
            case 77: return "Iridium";
            case 78: return "Platinum";
            case 79: return "Gold";
            case 80: return "Mercury";
            case 81: return "Thallium";
            case 82: return "Lead";
            case 83: return "Bismuth";
            case 84: return "Polonium";
            case 85: return "Astatine";
            case 86: return "Radon";
            case 87: return "Francium";
            case 88: return "Radium";
            case 89: return "Actinium";
            case 90: return "Thorium";
            case 91: return "Protactinium";
            case 92: return "Uranium";
            case 93: return "Neptunium";
            case 94: return "Plutonium";
            case 95: return "Americium";
            case 96: return "Curium";
            case 97: return "Berkelium";
            case 98: return "Californium";
            case 99: return "Einsteinium";
            case 100: return "Fermium";
            case 101: return "Mendelevium";
            case 102: return "Nobelium";
            case 103: return "Lawrencium";
            case 104: return "Rutherfordium";
            case 105: return "Dubnium";
            case 106: return "Seaborgium";
            case 107: return "Bohrium";
            case 108: return "Hassium";
            case 109: return "Meitnerium";
            case 110: return "Darmstadtium";
            case 111: return "Roentgenium";
            case 112: return "Copernicium";
            case 113: return "Nihonium";
            case 114: return "Flerovium";
            case 115: return "Moscovium";
            case 116: return "Livermorium";
            case 117: return "Tennessine";
            case 118: return "Oganesson";
            default: return "Unknown";
        }
    }

    // Define the periodic table layout (row, column positions) for each element based on atomic number
    Vector2 GetPositionEL(int atomicNumber)
    {
        switch (atomicNumber)
        {
            // Row 1: H, He
            case 1: return new Vector2(0, 0); // H
            case 2: return new Vector2(17, 0); // He

            // Row 2: Li to Ne
            case 3: return new Vector2(0, 1); // Li
            case 4: return new Vector2(1, 1); // Be
            case 5: return new Vector2(12, 1); // B
            case 6: return new Vector2(13, 1); // C
            case 7: return new Vector2(14, 1); // N
            case 8: return new Vector2(15, 1); // O
            case 9: return new Vector2(16, 1); // F
            case 10: return new Vector2(17, 1); // Ne

            // Row 3: Na to Ar
            case 11: return new Vector2(0, 2); // Na
            case 12: return new Vector2(1, 2); // Mg
            case 13: return new Vector2(12, 2); // Al
            case 14: return new Vector2(13, 2); // Si
            case 15: return new Vector2(14, 2); // P
            case 16: return new Vector2(15, 2); // S
            case 17: return new Vector2(16, 2); // Cl
            case 18: return new Vector2(17, 2); // Ar

            // Row 4: K to Kr
            case 19: return new Vector2(0, 3); // K
            case 20: return new Vector2(1, 3); // Ca
            case 21: return new Vector2(2, 3); // Sc
            case 22: return new Vector2(3, 3); // Ti
            case 23: return new Vector2(4, 3); // V
            case 24: return new Vector2(5, 3); // Cr
            case 25: return new Vector2(6, 3); // Mn
            case 26: return new Vector2(7, 3); // Fe
            case 27: return new Vector2(8, 3); // Co
            case 28: return new Vector2(9, 3); // Ni
            case 29: return new Vector2(10, 3); // Cu
            case 30: return new Vector2(11, 3); // Zn
            case 31: return new Vector2(12, 3); // Ga
            case 32: return new Vector2(13, 3); // Ge
            case 33: return new Vector2(14, 3); // As
            case 34: return new Vector2(15, 3); // Se
            case 35: return new Vector2(16, 3); // Br
            case 36: return new Vector2(17, 3); // Kr

            // Row 5: Rb to Xe
            case 37: return new Vector2(0, 4); // Rb
            case 38: return new Vector2(1, 4); // Sr
            case 39: return new Vector2(2, 4); // Y
            case 40: return new Vector2(3, 4); // Zr
            case 41: return new Vector2(4, 4); // Nb
            case 42: return new Vector2(5, 4); // Mo
            case 43: return new Vector2(6, 4); // Tc
            case 44: return new Vector2(7, 4); // Ru
            case 45: return new Vector2(8, 4); // Rh
            case 46: return new Vector2(9, 4); // Pd
            case 47: return new Vector2(10, 4); // Ag
            case 48: return new Vector2(11, 4); // Cd
            case 49: return new Vector2(12, 4); // In
            case 50: return new Vector2(13, 4); // Sn
            case 51: return new Vector2(14, 4); // Sb
            case 52: return new Vector2(15, 4); // Te
            case 53: return new Vector2(16, 4); // I
            case 54: return new Vector2(17, 4); // Xe

            // Row 6: Cs to Rn, with Lanthanides
            case 55: return new Vector2(0, 5); // Cs
            case 56: return new Vector2(1, 5); // Ba
            case 57: return new Vector2(2, 5); // La (Lanthanides start below)
            case 58: return new Vector2(3, 8); // Ce
            case 59: return new Vector2(4, 8); // Pr
            case 60: return new Vector2(5, 8); // Nd
            case 61: return new Vector2(6, 8); // Pm
            case 62: return new Vector2(7, 8); // Sm
            case 63: return new Vector2(8, 8); // Eu
            case 64: return new Vector2(9, 8); // Gd
            case 65: return new Vector2(10, 8); // Tb
            case 66: return new Vector2(11, 8); // Dy
            case 67: return new Vector2(12, 8); // Ho
            case 68: return new Vector2(13, 8); // Er
            case 69: return new Vector2(14, 8); // Tm
            case 70: return new Vector2(15, 8); // Yb
            case 71: return new Vector2(16, 8); // Lu
            case 72: return new Vector2(3, 5); // Hf
            case 73: return new Vector2(4, 5); // Ta
            case 74: return new Vector2(5, 5); // W
            case 75: return new Vector2(6, 5); // Re
            case 76: return new Vector2(7, 5); // Os
            case 77: return new Vector2(8, 5); // Ir
            case 78: return new Vector2(9, 5); // Pt
            case 79: return new Vector2(10, 5); // Au
            case 80: return new Vector2(11, 5); // Hg
            case 81: return new Vector2(12, 5); // Tl
            case 82: return new Vector2(13, 5); // Pb
            case 83: return new Vector2(14, 5); // Bi
            case 84: return new Vector2(15, 5); // Po
            case 85: return new Vector2(16, 5); // At
            case 86: return new Vector2(17, 5); // Rn

            // Row 7: Fr to Og, with Actinides
            case 87: return new Vector2(0, 6); // Fr
            case 88: return new Vector2(1, 6); // Ra
            case 89: return new Vector2(2, 6); // Ac (Actinides start below)
            case 90: return new Vector2(3, 9); // Th
            case 91: return new Vector2(4, 9); // Pa
            case 92: return new Vector2(5, 9); // U
            case 93: return new Vector2(6, 9); // Np
            case 94: return new Vector2(7, 9); // Pu
            case 95: return new Vector2(8, 9); // Am
            case 96: return new Vector2(9, 9); // Cm
            case 97: return new Vector2(10, 9); // Bk
            case 98: return new Vector2(11, 9); // Cf
            case 99: return new Vector2(12, 9); // Es
            case 100: return new Vector2(13, 9); // Fm
            case 101: return new Vector2(14, 9); // Md
            case 102: return new Vector2(15, 9); // No
            case 103: return new Vector2(16, 9); // Lr
            case 104: return new Vector2(3, 6); // Rf
            case 105: return new Vector2(4, 6); // Db
            case 106: return new Vector2(5, 6); // Sg
            case 107: return new Vector2(6, 6); // Bh
            case 108: return new Vector2(7, 6); // Hs
            case 109: return new Vector2(8, 6); // Mt
            case 110: return new Vector2(9, 6); // Ds
            case 111: return new Vector2(10, 6); // Rg
            case 112: return new Vector2(11, 6); // Cn
            case 113: return new Vector2(12, 6); // Nh
            case 114: return new Vector2(13, 6); // Fl
            case 115: return new Vector2(14, 6); // Mc
            case 116: return new Vector2(15, 6); // Lv
            case 117: return new Vector2(16, 6); // Ts
            case 118: return new Vector2(17, 6); // Og

            default: return new Vector2(-1, -1); // Return an invalid position for elements outside range
        }
    }
}