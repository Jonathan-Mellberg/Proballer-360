using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NPC_Dia : Interaction
{
    public GameObject template;
    public GameObject canvas;
    public GameObject player;
    public TextMeshPro text;
    public TextMeshPro name;
    float timer;

    /*
    void Dialog(string text, string name)
    {
        GameObject template_clone = Instantiate(template, template.transform);
        template_clone.transform.parent = canvas.transform;
        template_clone.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = text;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
    }
    */
    public override void Interact()
    {
        
    }
    
     public System.Collections.IEnumerator Dialog(List<string>charList, int charID, string dialog,List<string> diaList, int timePerLetter )
    {
        int l = 0;
        int o = 1;
        name.text = charList[charID];
        for (int i = 0; i < diaList[o].Length; i++)
        {
            
            dialog = diaList[o];
            for (int v = 0; v < dialog.Length; v++)
            {
                l++;
                text.text = dialog.Substring(0, l);
            }
            if (l >= dialog.Length)
            {
                while (!Input.GetMouseButton(0))
                {   

                }
                o++;
                text.text = diaList[o];
            }

        }
        Cursor.lockState = CursorLockMode.Locked;
        yield return null;
    }
}
   