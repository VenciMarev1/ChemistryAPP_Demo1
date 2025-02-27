using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Element : MonoBehaviour
{
    public GameObject Element_Table;
    public TMP_Text S_Name; // Element_text
    public TMP_Text F_Name; // FullName
    public TMP_Text Number;

    // Additional information fields
    public TMP_Text atomicMassText; // AtomicMassText
    public TMP_Text metalTypeText; // MetalTypeText
    public TMP_Text meltingPointText; // MeltingPointText
    public TMP_Text boilingPointText; // BoilingPointText
    public TMP_Text discoveryYearText; // DiscoveryYearText

    // Element data
    public string symbol;
    public string fullName;
    public int atomicNumber;
    public float atomicMass;
    public string metalType;
    public float meltingPoint;
    public float boilingPoint;
    public int discoveryYear;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool mousePressed = false;
    private Coroutine enlargeCoroutine;
    private Coroutine resetCoroutine;

    private float originalFontSize;
    private float enlargedFontSize;

    void Start()
    {
        originalScale = Element_Table.transform.localScale;
        originalPosition = Element_Table.transform.localPosition;

        // Store the original font size
        originalFontSize = S_Name.fontSize;
        enlargedFontSize = originalFontSize * 1.2f; // Adjust the multiplier as needed

        // Hide additional information on start
        atomicMassText.gameObject.SetActive(false);
        metalTypeText.gameObject.SetActive(false);
        meltingPointText.gameObject.SetActive(false);
        boilingPointText.gameObject.SetActive(false);
        discoveryYearText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Any additional update logic can go here
    }

    private void OnMouseOver()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        if (enlargeCoroutine == null)
        {
            enlargeCoroutine = StartCoroutine(EnlargeElementWithDelay());
        }
    }

    private void OnMouseExit()
    {
        if (enlargeCoroutine != null)
        {
            StopCoroutine(enlargeCoroutine);
            enlargeCoroutine = null;
        }

        if (resetCoroutine == null)
        {
            resetCoroutine = StartCoroutine(ResetElementSizeSmoothly());
        }
    }

    private void OnMouseDown()
    {
        mousePressed = true;
    }

    IEnumerator EnlargeElementWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Shorter delay before enlarging

        float duration = 0.5f; // Duration of the smooth transition
        float elapsedTime = 0f;

        Vector3 targetScale = new Vector3(originalScale.x * 1.5f, originalScale.y * 2f, originalScale.z);
        Vector3 targetPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 0.1f);

        while (elapsedTime < duration)
        {
            Element_Table.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            Element_Table.transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / duration);

            // Smoothly change the font size
            float currentFontSize = Mathf.Lerp(originalFontSize, enlargedFontSize, elapsedTime / duration);
            SetFontSize(currentFontSize);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Element_Table.transform.localScale = targetScale;
        Element_Table.transform.localPosition = targetPosition;

        // Ensure the font size is set to the enlarged size
        SetFontSize(enlargedFontSize);

        // Show additional information on the UI (atomic mass, metal type, etc.)
        atomicMassText.text = "Mass: " + atomicMass.ToString();
        metalTypeText.text = "Metal Type: " + metalType;
        meltingPointText.text = "Melting Point: " + meltingPoint.ToString() + " °C";
        boilingPointText.text = "Boiling Point: " + boilingPoint.ToString() + " °C";
        discoveryYearText.text = "Discovery Year: " + discoveryYear.ToString();

        // Position the additional information text boxes above the full name
        atomicMassText.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        metalTypeText.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        meltingPointText.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        boilingPointText.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        discoveryYearText.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed

        // Remap the initial 3 text boxes
        S_Name.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        F_Name.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        Number.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed

        // Ensure all text boxes are visible
        S_Name.gameObject.SetActive(true);
        F_Name.gameObject.SetActive(true);
        Number.gameObject.SetActive(true);
        atomicMassText.gameObject.SetActive(true);
        metalTypeText.gameObject.SetActive(true);
        meltingPointText.gameObject.SetActive(true);
        boilingPointText.gameObject.SetActive(true);
        discoveryYearText.gameObject.SetActive(true);
    }

    IEnumerator ResetElementSizeSmoothly()
    {
        float duration = 0.5f; // Duration of the smooth transition
        float elapsedTime = 0f;

        Vector3 currentScale = Element_Table.transform.localScale;
        Vector3 currentPosition = Element_Table.transform.localPosition;

        while (elapsedTime < duration)
        {
            Element_Table.transform.localScale = Vector3.Lerp(currentScale, originalScale, elapsedTime / duration);
            Element_Table.transform.localPosition = Vector3.Lerp(currentPosition, originalPosition, elapsedTime / duration);

            // Smoothly change the font size
            float currentFontSize = Mathf.Lerp(enlargedFontSize, originalFontSize, elapsedTime / duration);
            SetFontSize(currentFontSize);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Element_Table.transform.localScale = originalScale;
        Element_Table.transform.localPosition = originalPosition;

        // Ensure the font size is set to the original size
        SetFontSize(originalFontSize);

        // Hide the additional information UI
        atomicMassText.gameObject.SetActive(false);
        metalTypeText.gameObject.SetActive(false);
        meltingPointText.gameObject.SetActive(false);
        boilingPointText.gameObject.SetActive(false);
        discoveryYearText.gameObject.SetActive(false);

        // Reset the initial 3 text boxes to their original positions and ensure they are visible
        S_Name.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        F_Name.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed
        Number.rectTransform.anchoredPosition = new Vector2(0, 0); // Adjust as needed

        S_Name.gameObject.SetActive(true);
        F_Name.gameObject.SetActive(true);
        Number.gameObject.SetActive(true);
    }

    void SetFontSize(float fontSize)
    {
        S_Name.fontSize = fontSize;
        F_Name.fontSize = fontSize;
        Number.fontSize = fontSize;
        atomicMassText.fontSize = fontSize;
        metalTypeText.fontSize = fontSize;
        meltingPointText.fontSize = fontSize;
        boilingPointText.fontSize = fontSize;
        discoveryYearText.fontSize = fontSize;
    }
}
