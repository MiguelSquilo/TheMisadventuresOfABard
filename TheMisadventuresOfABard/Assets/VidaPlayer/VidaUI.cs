using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaUI : MonoBehaviour
{

    public Sprite[] heartSprites;
    public Image heartsUI;
    private GameManager healthMan;

    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       heartsUI.sprite = heartSprites[healthMan.currentHealth];
    }
}
