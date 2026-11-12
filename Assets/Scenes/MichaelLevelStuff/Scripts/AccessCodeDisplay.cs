using UnityEngine;
using TMPro;

public class AccessCodeDisplay : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI countdownText;

    public float changeTime = 30f;

    private float timer;

    public string CurrentCode { get; private set; }

    // Available symbols for the passcode
    private string[] symbols = { "@", "#", "$", "%" };

    void Start()
    {
        timer = changeTime;

        GenerateCode();
        UpdateCountdown();

        codeText.richText = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ResetCode();
        }

        UpdateCountdown();
    }

    public void ResetCode()
    {
        timer = changeTime;
        GenerateCode();
        UpdateCountdown();
    }

    void GenerateCode()
    {
        CurrentCode = "";

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, symbols.Length);
            CurrentCode += symbols[randomIndex];
        }

        codeText.text = CurrentCode;
    }

    void UpdateCountdown()
    {
        countdownText.text = "Next code in: "
            + Mathf.CeilToInt(timer) + "s";
    }
}
