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

        // Store the available symbols
        string[] availableSymbols = (string[])symbols.Clone();

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, availableSymbols.Length);

            // Add the randomly selected symbol
            CurrentCode += availableSymbols[randomIndex];

            // Remove that symbol if it has appeared twice
            int count = 0;

            foreach (char symbol in CurrentCode)
            {
                if (symbol.ToString() == availableSymbols[randomIndex])
                {
                    count++;
                }
            }

            if (count >= 2)
            {
                availableSymbols = System.Array.FindAll(
                    availableSymbols,
                    s => s != availableSymbols[randomIndex]
                );
            }
        }

        codeText.text = CurrentCode;
    }

    void UpdateCountdown()
    {
        countdownText.text = "Next code in: "
            + Mathf.CeilToInt(timer) + "s";
    }
}
