using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC_Dia : Interaction
{

    [SerializeField] private float timePerLetter = 0.1f;
    [SerializeField] private string[] dialogueList;
    [SerializeField] private string proceedButton;
    private basicMove player;
    private TextMeshProUGUI tmp;
    private TextMeshProUGUI nameTmp;
    private CustomerListV2 customerList;
    private TextMeshProUGUI endText;
    private Image textBox;
    private bool speaking;

    private void Awake()
    {
        customerList = CustomerListV2.instance;
        tmp = customerList.dialogueTextBox;
        nameTmp = customerList.nameTextBox;
        textBox = customerList.textBox;
        endText = customerList.endTextPopup;

        if (customerList.player != null)
            player = customerList.player.GetComponent<basicMove>();
    }

    public override void Interact()
    {
        if (speaking)
            return;

        StartCoroutine(Dialog());
    }

    public System.Collections.IEnumerator Dialog()
    {
        textBox.gameObject.SetActive(true);
        tmp.gameObject.SetActive(true);
        nameTmp.gameObject.SetActive(true);
        player.freeze = true;
        speaking = true;

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

                if (Input.GetKey(KeyCode.B))
                {
                    tmp.text = dialog;
                    yield return new WaitForSeconds(0.1f);
                    break;
                }
            }

            endText.gameObject.SetActive(true);

            while (!Input.GetKeyDown(KeyCode.B))
                yield return null;

            endText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        gameObject.GetComponent<Cust_Timer>().StartTimer();
        Cursor.lockState = CursorLockMode.Locked;

        textBox.gameObject.SetActive(false);
        tmp.gameObject.SetActive(false);
        nameTmp.gameObject.SetActive(false);
        player.freeze = false;
    }

}