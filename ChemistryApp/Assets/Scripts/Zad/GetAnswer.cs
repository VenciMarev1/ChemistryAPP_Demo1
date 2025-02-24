using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetAnswer : MonoBehaviour
{
    public TMP_InputField Input;
    public Slider progressbar;
    public TextMeshProUGUI a1;
    public TextMeshProUGUI a2;
    public TextMeshProUGUI a3;
    public Image help;
    List<TextMeshProUGUI> answers;
    public Image image;
    public Animator a;
    public GameObject ans;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        Input.onEndEdit.AddListener(Ex1);
        progressbar.enabled = false;
        answers = new List<TextMeshProUGUI>()
        {
            a1,
            a2,
            a3
        };

        help.gameObject.SetActive(false);
        image.color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //zad1
    public void Ex1(string arg0)
    {
        List<string> rightAnswers = new List<string>() {"Ba2+H-2", "P3-H+3", "H+2S2-"};
        int count;
        int i = 0;
        for(count = 0; count < rightAnswers.Count; count++)
        {
            if (arg0.Contains(rightAnswers[count]))
            {
                i++;
                answers[count].color = Color.green;
            }
            else
            {
                answers[count].color = Color.red;
            }
        }
        if(i == 3)
        {
            progressbar.value = 30;
            image.color = new Color(255, 255, 255, 255);
            a.Play("Congrats");
        }
    }

    public void GetHelp()
    {
        help.gameObject.SetActive(true);
    }
    public void GetABV()
    {
        Input.gameObject.SetActive(false);
        ans.SetActive(true);

    }

    public void buttonsAnswer()
    {
        count++;
        if (count == 3)
        {
            progressbar.value = 30;
            image.color = new Color(255, 255, 255, 255);
            a.Play("Congrats");
        }
    }

}
