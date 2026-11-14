using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComputerNavi : MonoBehaviour
{
    [System.Serializable]
    public class NavigationGroup
    {
        public string groupName;
        public GameObject screenObject;
        public int priority;
        public List<Button> buttons = new List<Button>();
        public Button backButton;
    }

    [Header("Computer Interaction")]
    public Camera pcPOV;

    [Header("Navigation Settings")]
    public int mainGroupIndex = 0;

    [Header("Navigation Groups")]
    public List<NavigationGroup> navigationGroups = new List<NavigationGroup>();

    private int currentGroupIndex = -1;
    private List<int> savedButtonIndices = new List<int>();
    private bool usingComputer = false;
    private bool manualMainFocus = false;

    private void Start()
    {
        for (int i = 0; i < navigationGroups.Count; i++)
        {
            int groupIndex = i;

            for (int j = 0; j < navigationGroups[i].buttons.Count; j++)
            {
                int buttonIndex = j;
                Button button = navigationGroups[i].buttons[j];

                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        MouseButtonClicked(groupIndex, buttonIndex);
                    });
                }
            }
        }
    }

    private void Update()
    {
        if (pcPOV == null || !pcPOV.gameObject.activeInHierarchy)
        {
            usingComputer = false;
            return;
        }

        if (!usingComputer)
        {
            usingComputer = true;
            manualMainFocus = true;

            savedButtonIndices.Clear();

            for (int i = 0; i < navigationGroups.Count; i++)
            {
                savedButtonIndices.Add(0);
            }

            SetCurrentGroup(mainGroupIndex);
        }

        if (!manualMainFocus)
        {
            DetectActiveGroup();
        }

        if (currentGroupIndex < 0)
            return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            MoveSelection(-1);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            MoveSelection(1);

        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
            ActivateSelectedButton();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            GoBack();
    }

    private void DetectActiveGroup()
    {
        int bestGroup = -1;
        int highestPriority = int.MinValue;

        for (int i = 0; i < navigationGroups.Count; i++)
        {
            NavigationGroup group = navigationGroups[i];

            if (group.screenObject == null)
                continue;

            if (group.screenObject.activeInHierarchy &&
                group.priority > highestPriority)
            {
                highestPriority = group.priority;
                bestGroup = i;
            }
        }

        if (bestGroup != -1 && bestGroup != currentGroupIndex)
            SetCurrentGroup(bestGroup);
    }

    private void SetCurrentGroup(int groupIndex)
    {
        if (groupIndex < 0 || groupIndex >= navigationGroups.Count)
            return;

        currentGroupIndex = groupIndex;

        SelectSavedButton();

        UpdateMainTabHighlight();
        UpdateCurrentGroupHighlight();
    }

    private void SelectSavedButton()
    {
        List<Button> buttons = navigationGroups[currentGroupIndex].buttons;

        if (buttons.Count == 0)
            return;

        int savedIndex = savedButtonIndices[currentGroupIndex];

        for (int i = 0; i < buttons.Count; i++)
        {
            int index = (savedIndex + i) % buttons.Count;

            if (IsButtonAvailable(buttons[index]))
            {
                savedButtonIndices[currentGroupIndex] = index;
                buttons[index].Select();
                return;
            }
        }
    }

    private void MoveSelection(int direction)
    {
        List<Button> buttons = navigationGroups[currentGroupIndex].buttons;

        if (buttons.Count == 0)
            return;

        int index = savedButtonIndices[currentGroupIndex];

        for (int i = 0; i < buttons.Count; i++)
        {
            index += direction;

            if (index >= buttons.Count)
                index = 0;

            if (index < 0)
                index = buttons.Count - 1;

            if (IsButtonAvailable(buttons[index]))
            {
                savedButtonIndices[currentGroupIndex] = index;
                buttons[index].Select();

                UpdateMainTabHighlight();
                UpdateCurrentGroupHighlight();

                return;
            }
        }
    }

    private bool IsButtonAvailable(Button button)
    {
        return button != null &&
               button.gameObject.activeInHierarchy &&
               button.interactable;
    }

    private void ActivateSelectedButton()
    {
        List<Button> buttons = navigationGroups[currentGroupIndex].buttons;

        int index = savedButtonIndices[currentGroupIndex];

        if (index < 0 || index >= buttons.Count)
            return;

        Button selectedButton = buttons[index];

        if (IsButtonAvailable(selectedButton))
        {
            manualMainFocus = false;
            selectedButton.onClick.Invoke();
        }
    }

    private void GoBack()
    {
        if (currentGroupIndex == mainGroupIndex)
            return;

        NavigationGroup group = navigationGroups[currentGroupIndex];

        if (IsButtonAvailable(group.backButton))
        {
            group.backButton.onClick.Invoke();
            manualMainFocus = false;
        }
        else
        {
            manualMainFocus = true;
            SetCurrentGroup(mainGroupIndex);
        }
    }

    private void UpdateMainTabHighlight()
    {
        if (mainGroupIndex < 0 || mainGroupIndex >= navigationGroups.Count)
            return;

        List<Button> mainButtons = navigationGroups[mainGroupIndex].buttons;

        for (int i = 0; i < mainButtons.Count; i++)
        {
            Button button = mainButtons[i];

            if (button == null || button.targetGraphic == null)
                continue;

            ColorBlock colors = button.colors;

            if (i == savedButtonIndices[mainGroupIndex])
                button.targetGraphic.color = colors.selectedColor;
            else
                button.targetGraphic.color = colors.normalColor;
        }
    }

    private void UpdateCurrentGroupHighlight()
    {
        if (currentGroupIndex < 0)
            return;

        List<Button> buttons = navigationGroups[currentGroupIndex].buttons;

        for (int i = 0; i < buttons.Count; i++)
        {
            Button button = buttons[i];

            if (button == null)
                continue;

            if (i == savedButtonIndices[currentGroupIndex])
            {
                button.Select();
            }
        }
    }

    private void MouseButtonClicked(int groupIndex, int buttonIndex)
    {
        if (groupIndex < 0 || groupIndex >= navigationGroups.Count)
            return;

        savedButtonIndices[groupIndex] = buttonIndex;

        UpdateMainTabHighlight();

        if (currentGroupIndex == groupIndex)
        {
            UpdateCurrentGroupHighlight();
        }
    }
}
