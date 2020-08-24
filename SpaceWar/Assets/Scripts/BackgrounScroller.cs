using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgrounScroller : MonoBehaviour
{

    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material myMaterieal;
    Vector2 offSet;

    // Start is called before the first frame update
    void Start()
    {
        myMaterieal = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterieal.mainTextureOffset += offSet * Time.deltaTime;
    }
}
