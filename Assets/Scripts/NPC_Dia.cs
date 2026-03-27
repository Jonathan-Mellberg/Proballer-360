using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC_Dia : Interaction
{
    [HideInInspector] public bool canSpeak;
    [HideInInspector] public string order;

    [Header("Dialogue")]
    [SerializeField] private GameObject icon;
    [SerializeField] private AudioClip speechSound;
    [SerializeField] private float timePerLetter = 0.1f;
    [SerializeField] private string[] startDialogue;
    [SerializeField] private string[] repeatDialogue;
    [SerializeField] private string[] winDialogue;
    [SerializeField] private string[] dissapointDialogue;

    [Header("Emotions")]
    [SerializeField] private string proceedButton;
    [SerializeField] private GameObject happyParticles;
    [SerializeField] private GameObject angryParticles;

    private basicMove player;
    private TextMeshProUGUI tmp;
    private TextMeshProUGUI nameTmp;
    private TextMeshProUGUI postIt;
    private CustomerListV2 customerList;
    private TextMeshProUGUI endText;
    private Image textBox;
    private RectTransform iconOriginPoint;
    private AudioSource audioSource;
    private bool spoken;

    private void Awake()
    {
        customerList = CustomerListV2.instance;
        tmp = customerList.dialogueTextBox;
        nameTmp = customerList.nameTextBox;
        postIt = customerList.postIt;
        textBox = customerList.textBox;
        iconOriginPoint = customerList.iconOriginPoint;
        endText = customerList.endTextPopup;
        audioSource = GetComponent<AudioSource>();

        Instantiate(icon, iconOriginPoint);
        icon.SetActive(false);

        if (customerList.player != null)
            player = customerList.player.GetComponent<basicMove>();
    }

    public override void Interact()
    {
        if (!canSpeak)
            return;

        string[] dia = spoken ? repeatDialogue : startDialogue;
        StartCoroutine(Dialog(dia));
    }

    public void CompletionSpeech()
    {
        StartCoroutine(Dialog(winDialogue));
    }

    public void IncorrectSpeech()
    {
        StartCoroutine(Dialog(dissapointDialogue));
    }

    public System.Collections.IEnumerator Dialog(string[] dialogue)
    {
        gameObject.GetComponent<Customer>().GenerateOrder();

        if (postIt != null) postIt.text = order;

        textBox.gameObject.SetActive(true);
        tmp.gameObject.SetActive(true);
        nameTmp.gameObject.SetActive(true);
        icon.SetActive(true);
        player.freeze = true;
        canSpeak = false;

        string dialog;

        nameTmp.text = gameObject.name;

        for (int i = 0; i < dialogue.Length; i++)
        {
            dialog = dialogue[i];

            // React on action symbols
            if (dialog.Contains("*"))
            {
                dialog = dialog.Replace("*", order);
            }
            else if (dialog.Contains("^"))
            {
                dialog = dialog.Replace("^", "");
                Instantiate(happyParticles);
            }
            else if (dialog.Contains("|"))
            {
                dialog = dialog.Replace("|", "");
                Instantiate(angryParticles);
            }

            // Print dialogue
            for (int v = 0; v <= dialog.Length; v++)
            {
                tmp.text = dialog[..v];
                audioSource.PlayOneShot(speechSound);

                yield return new WaitForSeconds(timePerLetter);

                if (Input.GetKey(KeyCode.Space))
                {
                    tmp.text = dialog;
                    yield return new WaitForSeconds(0.1f);
                    break;
                }
            }

            endText.gameObject.SetActive(true);

            while (!Input.GetKeyDown(KeyCode.Space))
                yield return null;

            endText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }

        textBox.gameObject.SetActive(false);
        tmp.gameObject.SetActive(false);
        nameTmp.gameObject.SetActive(false);
        icon.SetActive(false);
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