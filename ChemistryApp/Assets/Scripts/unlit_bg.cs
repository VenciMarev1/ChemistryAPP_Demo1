using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{
    void Start()
    {
        CreateBackground();  // Automatically create a black background when the scene starts.
    }

    void CreateBackground()
    {
        // Create a Quad to act as the background
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.position = new Vector3(10, -3.5f, -2.4f);  // Position it behind the table
        quad.transform.localScale = new Vector3(40f, 20f, 1f); // Scale it to fit the camera view

        // Create and apply a material for shading
        Material shadedMaterial = new Material(Shader.Find("Standard"));
        shadedMaterial.color = Color.black;  // Set the base color to black
        shadedMaterial.SetFloat("_Glossiness", 0.4f);  // Set smoothness for shading

        Renderer quadRenderer = quad.GetComponent<Renderer>();
        quadRenderer.material = shadedMaterial;  // Assign the shaded material to the quad
    }
}