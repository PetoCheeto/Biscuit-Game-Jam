using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItManager1: MonoBehaviour
{
    public int its;
    public TextMeshProUGUI itsText;
    public static ItManager1 instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(its <= 0) 
        {
            its = 1;
        }
        itsText.text = its.ToString();
    }
}
