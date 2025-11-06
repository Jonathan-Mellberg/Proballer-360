using UnityEngine;

public class Next_Dia : MonoBehaviour
{
    int index = 2; 
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.childCount > 1)
        {
            //if (PlayerMovement.Dia)
            transform.GetChild(index).gameObject.SetActive(true);
            index += 1;
            if (transform.childCount == index)
            {
                index = 2;
                //PlayerMovement.Dia = false;
            }
            //else
            //GameObject.SetActive(false);
        }
    }
}
