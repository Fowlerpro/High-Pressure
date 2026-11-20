using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AccessCodeDisplay : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI countdownText;

    public float changeTime = 30f;

    // Force reset cooldown
    public float resetCooldown = 5f;
    public Image cooldownBar;

    private float timer;
    private float cooldownTimer;
    private bool isOnCooldown = false;

    public string CurrentCode { get; private set; }

    // Available symbols for the passcode
    private string[] symbols = { "@", "#", "$", "%" };

    void Start()
    {
        timer = changeTime;

        GenerateCode();
        UpdateCountdown();

        codeText.richText = false;

        if (cooldownBar != null)
        {
            cooldownBar.fillAmount = 1f;
        }
    }

    void Update()
    {
        // Normal passcode countdown
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ResetCode();
        }

        UpdateCountdown();

        // Force reset cooldown
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownBar != null)
            {
                cooldownBar.fillAmount = cooldownTimer / resetCooldown;
            }

            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
                isOnCooldown = false;

                if (cooldownBar != null)
                {
                    cooldownBar.fillAmount = 1f;
                }
            }
        }
    }

    public void ResetCode()
    {
        timer = changeTime;
        GenerateCode();
        UpdateCountdown();
    }

    // Called by the Force Reset button
    public void ForceResetCode()
    {
        // Prevent resetting if cooldown is active
        if (isOnCooldown)
        {
            return;
        }

        // Reset the passcode
        ResetCode();

        // Start cooldown
        cooldownTimer = resetCooldown;
        isOnCooldown = true;

        if (cooldownBar != null)
        {
            cooldownBar.fillAmount = 1f;
        }
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