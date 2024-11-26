using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : Selectable
{
    public GameObject number_txt;
    private int num_ = 0;
    private void DisplayTxt()
    {
        if (num_ <= 0)
            number_txt.GetComponent<Text>().text = "";
        else
            number_txt.GetComponent<Text>().text = num_.ToString();
    }
    public void SetNumber(int num)
    {
        num_ = num;
        DisplayTxt();
    }
}
