using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC_Dia : Interaction
{
    [HideInInspector] public bool canSpeak;

    [SerializeField] private float timePerLetter = 0.1f;
    [SerializeField] private string[] StartDialogue;
    [SerializeField] private string[] RepeatDialogue;
    [SerializeField] private string[] WinDialogue;
    [SerializeField] private string[] LoseDialogue;
    [SerializeField] private string proceedButton;
    private basicMove player;
    private TextMeshProUGUI tmp;
    private TextMeshProUGUI nameTmp;
    private CustomerListV2 customerList;
    private TextMeshProUGUI endText;
    private Image textBox;
    private bool spoken;

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
        Debug.Log(canSpeak);
        if (!canSpeak)
            return;

        string[] dia = spoken ? RepeatDialogue : StartDialogue;
        StartCoroutine(Dialog(dia));
    }

    public void CompletionSpeech(bool win)
    {
        string[] dia = win ? WinDialogue : LoseDialogue;
        StartCoroutine(Dialog(dia));
    }

    public System.Collections.IEnumerator Dialog(string[] dialogue)
    {
        textBox.gameObject.SetActive(true);
        tmp.gameObject.SetActive(true);
        nameTmp.gameObject.SetActive(true);
        player.freeze = true;
        canSpeak = false;

        string dialog;

        nameTmp.text = gameObject.name;

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialog = dialogue[i];

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

        textBox.gameObject.SetActive(false);
        tmp.gameObject.SetActive(false);
        nameTmp.gameObject.SetActive(false);
        player.freeze = false;
        canSpeak = true;

        if (!spoken)
        {
            spoken = true;
            customerList.StartCoroutine(customerList.MoveToWaitPos());
            gameObject.GetComponent<Cust_Timer>().StartTimer();
        }
    }

}