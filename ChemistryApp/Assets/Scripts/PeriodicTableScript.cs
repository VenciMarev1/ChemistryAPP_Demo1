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
        "Transition metals",     // 30 - Zinc
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
        "Transition metals",     // 48 - Cadmium
        "Post-transition metals",// 49 - Indium
        "Post-transition metals",// 50 - Tin
        "Metalloids",            // 51 - Antimony
        "Metalloids",            // 52 - Tellurium
        "Reactive non-metals",   // 53 - Iodine
        "Noble gases",           // 54 - Xenon
        "Alkali metals",         // 55 - Cesium
        "Alkaline earth metals", // 56 - Barium
        "Lanthanides",           // 57 - Lanthanum
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
        "Transition metals",     // 79 - Gold
        "Transition metals",     // 80 - Mercury
        "Post-transition metals",// 81 - Thallium
        "Post-transition metals",// 82 - Lead
        "Post-transition metals",// 83 - Bismuth
        "Post-transition metals",// 84 - Polonium
        "Post-transition metals",// 85 - Astatine
        "Noble gases",           // 86 - Radon
        "Alkali metals",         // 87 - Francium
        "Alkaline earth metals", // 88 - Radium
        "Actinides",             // 89 - Actinium
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

    // Add additional data for each element
    private ElementData[] elementData = new ElementData[]
{
    new ElementData("Hydrogen", "H", 1, 1.008f, "Reactive non-metals", -259.16f, -252.87f, 1766),
    new ElementData("Helium", "He", 2, 4.0026f, "Noble gases", -272.2f, -268.93f, 1895),
    new ElementData("Lithium", "Li", 3, 6.94f, "Alkali metals", 180.54f, 1342f, 1817),
    new ElementData("Beryllium", "Be", 4, 9.0122f, "Alkaline earth metals", 1287f, 2469f, 1798),
    new ElementData("Boron", "B", 5, 10.81f, "Metalloids", 2076f, 3927f, 1808),
    new ElementData("Carbon", "C", 6, 12.011f, "Reactive non-metals", 3550f, 4827f, -1),
    new ElementData("Nitrogen", "N", 7, 14.007f, "Reactive non-metals", -210.1f, -195.8f, 1772),
    new ElementData("Oxygen", "O", 8, 15.999f, "Reactive non-metals", -218.79f, -182.96f, 1774),
    new ElementData("Fluorine", "F", 9, 18.998f, "Reactive non-metals", -219.67f, -188.12f, 1886),
    new ElementData("Neon", "Ne", 10, 20.180f, "Noble gases", -248.59f, -246.08f, 1898),
    new ElementData("Sodium", "Na", 11, 22.990f, "Alkali metals", 97.72f, 883f, 1807),
    new ElementData("Magnesium", "Mg", 12, 24.305f, "Alkaline earth metals", 650f, 1090f, 1755),
    new ElementData("Aluminium", "Al", 13, 26.982f, "Post-transition metals", 660.32f, 2519f, 1825),
    new ElementData("Silicon", "Si", 14, 28.085f, "Metalloids", 1414f, 3265f, 1824),
    new ElementData("Phosphorus", "P", 15, 30.974f, "Reactive non-metals", 44.15f, 280.5f, 1669),
    new ElementData("Sulfur", "S", 16, 32.06f, "Reactive non-metals", 115.21f, 444.6f, -1),
    new ElementData("Chlorine", "Cl", 17, 35.45f, "Reactive non-metals", -101.5f, -34.04f, 1774),
    new ElementData("Argon", "Ar", 18, 39.948f, "Noble gases", -189.34f, -185.85f, 1894),
    new ElementData("Potassium", "K", 19, 39.098f, "Alkali metals", 63.5f, 759f, 1807),
    new ElementData("Calcium", "Ca", 20, 40.078f, "Alkaline earth metals", 842f, 1484f, 1808),
    new ElementData("Scandium", "Sc", 21, 44.956f, "Transition metals", 1541f, 2830f, 1879),
    new ElementData("Titanium", "Ti", 22, 47.867f, "Transition metals", 1668f, 3287f, 1791),
    new ElementData("Vanadium", "V", 23, 50.942f, "Transition metals", 1910f, 3407f, 1801),
    new ElementData("Chromium", "Cr", 24, 51.996f, "Transition metals", 1907f, 2671f, 1797),
    new ElementData("Manganese", "Mn", 25, 54.938f, "Transition metals", 1246f, 2061f, 1774),
    new ElementData("Iron", "Fe", 26, 55.845f, "Transition metals", 1538f, 2862f, -1),
    new ElementData("Cobalt", "Co", 27, 58.933f, "Transition metals", 1495f, 2927f, 1735),
    new ElementData("Nickel", "Ni", 28, 58.693f, "Transition metals", 1455f, 2730f, 1751),
    new ElementData("Copper", "Cu", 29, 63.546f, "Transition metals", 1084.62f, 2562f, -1),
    new ElementData("Zinc", "Zn", 30, 65.38f, "Transition metals", 419.53f, 907f, 1746),
    new ElementData("Gallium", "Ga", 31, 69.723f, "Post-transition metals", 29.76f, 2204f, 1875),
    new ElementData("Germanium", "Ge", 32, 72.63f, "Metalloids", 938.25f, 2833f, 1886),
    new ElementData("Arsenic", "As", 33, 74.922f, "Metalloids", 817f, 614f, -1),
    new ElementData("Selenium", "Se", 34, 78.971f, "Reactive non-metals", 221f, 685f, 1817),
    new ElementData("Bromine", "Br", 35, 79.904f, "Reactive non-metals", -7.2f, 58.8f, 1826),
    new ElementData("Krypton", "Kr", 36, 83.798f, "Noble gases", -157.36f, -153.22f, 1898),
    new ElementData("Rubidium", "Rb", 37, 85.468f, "Alkali metals", 39.31f, 688f, 1861),
    new ElementData("Strontium", "Sr", 38, 87.62f, "Alkaline earth metals", 777f, 1382f, 1790),
    new ElementData("Yttrium", "Y", 39, 88.906f, "Transition metals", 1526f, 3338f, 1794),
    new ElementData("Zirconium", "Zr", 40, 91.224f, "Transition metals", 1855f, 4409f, 1789),
    new ElementData("Niobium", "Nb", 41, 92.906f, "Transition metals", 2477f, 4744f, 1801),
    new ElementData("Molybdenum", "Mo", 42, 95.95f, "Transition metals", 2623f, 4639f, 1778),
    new ElementData("Technetium", "Tc", 43, 98f, "Transition metals", 2157f, 4265f, 1937),
    new ElementData("Ruthenium", "Ru", 44, 101.07f, "Transition metals", 2334f, 4150f, 1844),
    new ElementData("Rhodium", "Rh", 45, 102.91f, "Transition metals", 1964f, 3695f, 1803),
    new ElementData("Palladium", "Pd", 46, 106.42f, "Transition metals", 1554.9f, 2963f, 1803),
    new ElementData("Silver", "Ag", 47, 107.87f, "Transition metals", 961.78f, 2162f, -1),
    new ElementData("Cadmium", "Cd", 48, 112.41f, "Transition metals", 321.07f, 767f, 1817),
    new ElementData("Indium", "In", 49, 114.82f, "Post-transition metals", 156.6f, 2072f, 1863),
    new ElementData("Tin", "Sn", 50, 118.71f, "Post-transition metals", 231.93f, 2602f, -1),
    new ElementData("Antimony", "Sb", 51, 121.76f, "Metalloids", 630.63f, 1587f, -1),
    new ElementData("Tellurium", "Te", 52, 127.6f, "Metalloids", 449.51f, 988f, 1782),
    new ElementData("Iodine", "I", 53, 126.9f, "Reactive non-metals", 113.7f, 184.3f, 1811),
    new ElementData("Xenon", "Xe", 54, 131.29f, "Noble gases", -111.8f, -108.1f, 1898),
    new ElementData("Cesium", "Cs", 55, 132.91f, "Alkali metals", 28.44f, 671f, 1860),
    new ElementData("Barium", "Ba", 56, 137.33f, "Alkaline earth metals", 727f, 1845f, 1808),
    new ElementData("Lanthanum", "La", 57, 138.91f, "Lanthanides", 920f, 3464f, 1839),
    new ElementData("Cerium", "Ce", 58, 140.12f, "Lanthanides", 798f, 3424f, 1803),
    new ElementData("Praseodymium", "Pr", 59, 140.91f, "Lanthanides", 931f, 3290f, 1885),
    new ElementData("Neodymium", "Nd", 60, 144.24f, "Lanthanides", 1024f, 3074f, 1885),
    new ElementData("Promethium", "Pm", 61, 145f, "Lanthanides", 1100f, 3000f, 1945),
    new ElementData("Samarium", "Sm", 62, 150.36f, "Lanthanides", 1072f, 1900f, 1879),
    new ElementData("Europium", "Eu", 63, 151.96f, "Lanthanides", 822f, 1529f, 1901),
    new ElementData("Gadolinium", "Gd", 64, 157.25f, "Lanthanides", 1312f, 3273f, 1880),
    new ElementData("Terbium", "Tb", 65, 158.93f, "Lanthanides", 1356f, 3230f, 1843),
    new ElementData("Dysprosium", "Dy", 66, 162.5f, "Lanthanides", 1412f, 2562f, 1886),
    new ElementData("Holmium", "Ho", 67, 164.93f, "Lanthanides", 1474f, 2700f, 1878),
    new ElementData("Erbium", "Er", 68, 167.26f, "Lanthanides", 1529f, 2868f, 1842),
    new ElementData("Thulium", "Tm", 69, 168.93f, "Lanthanides", 1545f, 1950f, 1879),
    new ElementData("Ytterbium", "Yb", 70, 173.04f, "Lanthanides", 824f, 1196f, 1878),
    new ElementData("Lutetium", "Lu", 71, 174.97f, "Lanthanides", 1652f, 3402f, 1907),
    new ElementData("Hafnium", "Hf", 72, 178.49f, "Transition metals", 2233f, 4603f, 1923),
    new ElementData("Tantalum", "Ta", 73, 180.95f, "Transition metals", 3017f, 5458f, 1802),
    new ElementData("Tungsten", "W", 74, 183.84f, "Transition metals", 3422f, 5555f, 1783),
    new ElementData("Rhenium", "Re", 75, 186.21f, "Transition metals", 3186f, 5596f, 1925),
    new ElementData("Osmium", "Os", 76, 190.23f, "Transition metals", 3033f, 5012f, 1803),
    new ElementData("Iridium", "Ir", 77, 192.22f, "Transition metals", 2446f, 4130f, 1803),
    new ElementData("Platinum", "Pt", 78, 195.08f, "Transition metals", 1768.3f, 3825f, -1),
    new ElementData("Gold", "Au", 79, 196.97f, "Transition metals", 1064.18f, 2970f, -1),
    new ElementData("Mercury", "Hg", 80, 200.59f, "Transition metals", -38.83f, 356.73f, -1),
    new ElementData("Thallium", "Tl", 81, 204.38f, "Post-transition metals", 304f, 1473f, 1861),
    new ElementData("Lead", "Pb", 82, 207.2f, "Post-transition metals", 327.46f, 1749f, -1),
    new ElementData("Bismuth", "Bi", 83, 208.98f, "Post-transition metals", 271.3f, 1564f, -1),
    new ElementData("Polonium", "Po", 84, 209f, "Post-transition metals", 254f, 962f, 1898),
    new ElementData("Astatine", "At", 85, 210f, "Post-transition metals", 302f, 337f, 1940),
    new ElementData("Radon", "Rn", 86, 222f, "Noble gases", -71f, -61.7f, 1900),
    new ElementData("Francium", "Fr", 87, 223f, "Alkali metals", 27f, 677f, 1939),
    new ElementData("Radium", "Ra", 88, 226f, "Alkaline earth metals", 700f, 1737f, 1898),
    new ElementData("Actinium", "Ac", 89, 227f, "Actinides", 1050f, 3200f, 1899),
    new ElementData("Thorium", "Th", 90, 232.04f, "Actinides", 1750f, 4788f, 1829),
    new ElementData("Protactinium", "Pa", 91, 231.04f, "Actinides", 1568f, 4027f, 1913),
    new ElementData("Uranium", "U", 92, 238.03f, "Actinides", 1132.2f, 4131f, 1789),
    new ElementData("Neptunium", "Np", 93, 237f, "Actinides", 644f, 3902f, 1940),
    new ElementData("Plutonium", "Pu", 94, 244f, "Actinides", 640f, 3228f, 1940),
    new ElementData("Americium", "Am", 95, 243f, "Actinides", 1176f, 2607f, 1944),
    new ElementData("Curium", "Cm", 96, 247f, "Actinides", 1340f, 3110f, 1944),
    new ElementData("Berkelium", "Bk", 97, 247f, "Actinides", 986f, 2627f, 1949),
    new ElementData("Californium", "Cf", 98, 251f, "Actinides", 900f, 1472f, 1950),
    new ElementData("Einsteinium", "Es", 99, 252f, "Actinides", 860f, 996f, 1952),
    new ElementData("Fermium", "Fm", 100, 257f, "Actinides", 1527f, -1f, 1952),
    new ElementData("Mendelevium", "Md", 101, 258f, "Actinides", 827f, -1f, 1955),
    new ElementData("Nobelium", "No", 102, 259f, "Actinides", 827f, -1f, 1966),
    new ElementData("Lawrencium", "Lr", 103, 262f, "Actinides", 1627f, -1f, 1961),
    new ElementData("Rutherfordium", "Rf", 104, 267f, "Transition metals", 2400f, 5800f, 1964),
    new ElementData("Dubnium", "Db", 105, 270f, "Transition metals", 2573f, 5200f, 1967),
    new ElementData("Seaborgium", "Sg", 106, 271f, "Transition metals", 2830f, 6200f, 1974),
    new ElementData("Bohrium", "Bh", 107, 270f, "Transition metals", 2727f, 5800f, 1981),
    new ElementData("Hassium", "Hs", 108, 277f, "Transition metals", 126f, 1740f, 1984),
    new ElementData("Meitnerium", "Mt", 109, 278f, "Unknown properties", 0f, 0f, 1982),
    new ElementData("Darmstadtium", "Ds", 110, 281f, "Unknown properties", 0f, 0f, 1994),
    new ElementData("Roentgenium", "Rg", 111, 282f, "Unknown properties", 0f, 0f, 1994),
    new ElementData("Copernicium", "Cn", 112, 285f, "Unknown properties", 0f, 0f, 1996),
    new ElementData("Nihonium", "Nh", 113, 286f, "Unknown properties", 0f, 0f, 2003),
    new ElementData("Flerovium", "Fl", 114, 289f, "Unknown properties", 0f, 0f, 1998),
    new ElementData("Moscovium", "Mc", 115, 290f, "Unknown properties", 0f, 0f, 2003),
    new ElementData("Livermorium", "Lv", 116, 293f, "Unknown properties", 0f, 0f, 2000),
    new ElementData("Tennessine", "Ts", 117, 294f, "Unknown properties", 0f, 0f, 2010),
    new ElementData("Oganesson", "Og", 118, 294f, "Unknown properties", 0f, 0f, 2002)
};


    void BuildPeriodicTable()
    {
        for (int i = 0; i < elementPrefabs.Count; i++)
        {
            int atomicNumber = i + 1;
            Vector2 position = GetPositionEL(atomicNumber);

            GameObject element = Instantiate(elementPrefabs[i], periodicTableParent.transform);
            element.transform.localPosition = new Vector3(position.x * 100, position.y * -100, 0); // Adjust the spacing multiplier as needed

            Element elementScript = element.GetComponent<Element>();
            if (elementScript != null)
            {
                ElementData data = elementData[atomicNumber - 1];
                elementScript.S_Name = element.transform.Find("S_Name").GetComponent<TMP_Text>();
                elementScript.F_Name = element.transform.Find("F_Name").GetComponent<TMP_Text>();
                elementScript.Number = element.transform.Find("Number").GetComponent<TMP_Text>();
                elementScript.atomicMassText = element.transform.Find("atomicMassText").GetComponent<TMP_Text>();
                elementScript.metalTypeText = element.transform.Find("metalTypeText").GetComponent<TMP_Text>();
                elementScript.meltingPointText = element.transform.Find("meltingPointText").GetComponent<TMP_Text>();
                elementScript.boilingPointText = element.transform.Find("boilingPointText").GetComponent<TMP_Text>();
                elementScript.discoveryYearText = element.transform.Find("discoveryYearText").GetComponent<TMP_Text>();

                elementScript.S_Name.text = data.symbol;
                elementScript.F_Name.text = data.name;
                elementScript.Number.text = data.atomicNumber.ToString();
                elementScript.atomicMassText.text = "Mass: " + data.atomicMass.ToString();
                elementScript.metalTypeText.text = "Metal Type: " + data.metalType;
                elementScript.meltingPointText.text = "Melting Point: " + data.meltingPoint.ToString() + " °C";
                elementScript.boilingPointText.text = "Boiling Point: " + data.boilingPoint.ToString() + " °C";
                elementScript.discoveryYearText.text = "Discovery Year: " + data.discoveryYear.ToString();
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
        // BuildPeriodicTable();
        CompleteAndPositionElements();
    }

    void CompleteAndPositionElements()
    {
        for (int atomicNumber = 1; atomicNumber <= elementData.Length; atomicNumber++)
        {
            Transform elementTransform = GetElementTransform1(atomicNumber);

            // If the element prefab doesn't exist in the scene yet, instantiate one
            if (elementTransform == null)
            {
                GameObject newElement = Instantiate(elementPrefab, periodicTableParent.transform);
                newElement.name = atomicNumber + "_" + elementData[atomicNumber - 1].name; // Name format: "1_Hydrogen"
                Element elementScript = newElement.GetComponent<Element>();

                // Assign TMP_Text components programmatically
                elementScript.S_Name = newElement.transform.Find("Element_text").GetComponent<TMP_Text>();
                elementScript.F_Name = newElement.transform.Find("FullName").GetComponent<TMP_Text>();
                elementScript.Number = newElement.transform.Find("Number").GetComponent<TMP_Text>();
                elementScript.atomicMassText = newElement.transform.Find("AtomicMassText").GetComponent<TMP_Text>();
                elementScript.metalTypeText = newElement.transform.Find("MetalTypeText").GetComponent<TMP_Text>();
                elementScript.meltingPointText = newElement.transform.Find("MeltingPointText").GetComponent<TMP_Text>();
                elementScript.boilingPointText = newElement.transform.Find("BoilingPointText").GetComponent<TMP_Text>();
                elementScript.discoveryYearText = newElement.transform.Find("DiscoveryYearText").GetComponent<TMP_Text>();

                // Set element data
                ElementData data = elementData[atomicNumber - 1];
                elementScript.symbol = data.symbol;
                elementScript.fullName = data.name;
                elementScript.atomicNumber = atomicNumber;
                elementScript.atomicMass = data.atomicMass;
                elementScript.metalType = data.metalType;
                elementScript.meltingPoint = data.meltingPoint;
                elementScript.boilingPoint = data.boilingPoint;
                elementScript.discoveryYear = data.discoveryYear;

                // Set TMP_Text values
                elementScript.S_Name.text = data.symbol;
                elementScript.F_Name.text = data.name;
                elementScript.Number.text = data.atomicNumber.ToString();
                elementScript.atomicMassText.text = "Mass: " + data.atomicMass.ToString();
                elementScript.metalTypeText.text = "Metal Type: " + data.metalType;
                elementScript.meltingPointText.text = "Melting Point: " + data.meltingPoint.ToString() + " °C";
                elementScript.boilingPointText.text = "Boiling Point: " + data.boilingPoint.ToString() + " °C";
                elementScript.discoveryYearText.text = "Discovery Year: " + data.discoveryYear.ToString();

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
public class ElementData
{
    public string name;
    public string symbol;
    public int atomicNumber;
    public float atomicMass;
    public string metalType;
    public float meltingPoint;
    public float boilingPoint;
    public int discoveryYear;

    public ElementData(string name, string symbol, int atomicNumber, float atomicMass, string metalType, float meltingPoint, float boilingPoint, int discoveryYear)
    {
        this.name = name;
        this.symbol = symbol;
        this.atomicNumber = atomicNumber;
        this.atomicMass = atomicMass;
        this.metalType = metalType;
        this.meltingPoint = meltingPoint;
        this.boilingPoint = boilingPoint;
        this.discoveryYear = discoveryYear;
    }
}