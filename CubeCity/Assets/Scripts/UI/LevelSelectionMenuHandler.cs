using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionMenuHandler : MonoBehaviour
{
    [SerializeField] private SetLevelButton[] _setLevelButtons;
    private SelectLevelHandler _selectLevelHandler;
    private PopupLevelSelection _popupLevelSelection;

    private void Start() => Init();

    private void Init()
    {
        _selectLevelHandler = FindObjectOfType<SelectLevelHandler>();
        _popupLevelSelection = FindObjectOfType<PopupLevelSelection>();

        for (int i = 0; i < _setLevelButtons.Length; i++)
            _setLevelButtons[i].Init(_selectLevelHandler, _popupLevelSelection);
    }
}
