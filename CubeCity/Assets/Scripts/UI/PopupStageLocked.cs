using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupStageLocked : MonoBehaviour
{
    [SerializeField]
    private int starsNedded;

    [SerializeField]
    private GameObject[] firstButtonLockedImages;

    [SerializeField]
    private Button firstelevelButton;

    private void Start() => Init();

    private void Init()
    {
        if(Player.Instance.StarsAmount >= starsNedded)
        {
            for (int i = 0; i < firstButtonLockedImages.Length; i++)
                firstButtonLockedImages[i].SetActive(true);

            firstelevelButton.interactable = true;

            gameObject.SetActive(false);
        }
    }
}
