using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NPC_Dia : Interaction
{
    [SerializeField] private float timePerLetter = 0.1f;
    [SerializeField] private string[] dialogueList;
    [SerializeField] private string proceedButton;
    private basicMove player;
    private TextMeshProUGUI tmp;
    private TextMeshProUGUI nameTmp;
    private CustomerListV2 customerList;

    private void Awake()
    {
        customerList = CustomerListV2.instance;
        tmp = customerList.dialogueTextBox;
        nameTmp = customerList.nameTextBox;

        if (customerList.player != null)
            player = customerList.player.GetComponent<basicMove>();
    }

    public override void Interact()
    {
        tmp.enabled = true;
        nameTmp.enabled = true;
        StartCoroutine(Dialog());
    }

    public System.Collections.IEnumerator Dialog()
    {
        player.freeze = true;
        Cursor.lockState = CursorLockMode.Confined;

        string dialog;

        nameTmp.text = gameObject.name;

        for (int i = 0; i < dialogueList.Length; i++)
        {
            dialog = dialogueList[i];

            // Print dialogue
            for (int v = 0; v <= dialog.Length; v++)
            {
                tmp.text = dialog[..v];
                yield return new WaitForSeconds(timePerLetter);
            }

            while (!Input.GetKeyDown(KeyCode.B))
                yield return null;

            yield return new WaitForSeconds(0.5f);
        }

        gameObject.GetComponent<Cust_Timer>().StartTimer();
        player.freeze = false;
        Cursor.lockState = CursorLockMode.Locked;
        tmp.enabled = false;
        nameTmp.enabled = false;
    }

}