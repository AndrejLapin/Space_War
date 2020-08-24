using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{

    TextMeshProUGUI hpText;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        hpText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = player.GetHealth().ToString();
    }
}
