using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class lobby : MonoBehaviour
{
    public GameObject menu, difficulty, cast, wumpus;
    public AudioSource звук_кнопки;
    public Slider slider_size, slider_hole, slider_bat;
    public TMPro.TMP_Text size_text,hole_text,bat_text;
    public Toggle moves_bat;
    bool en;//i_1,i_2
    void Start()
    {
        звук_кнопки.volume = LORD.слайдер * 0.5f;
        slider_size.value = LORD.LEN;
        slider_hole.value = LORD.HOLE;
        slider_bat.value = LORD.BAT;
        moves_bat.isOn = LORD.moves_bat;
        slider_hole.maxValue = slider_size.value * slider_size.value - 6 - slider_bat.value;
        slider_bat.maxValue = slider_size.value * slider_size.value - 6 - slider_hole.value;
    }
    void Update()
    {
        if (slider_hole.maxValue < 0)
        {
            slider_bat.maxValue--;
        }
        if (slider_bat.maxValue < 0)
        {
            slider_hole.maxValue--;
        }
    }
    public void играть()
    {
        звук_кнопки.Play();
        menu.SetActive(false);
        difficulty.SetActive(true);
    }
    public void bat_if()
    {
        bat_text.text = $"{slider_bat.value}/{slider_size.value * slider_size.value-6}";
        LORD.BAT = (int)slider_bat.value;
        slider_hole.maxValue = slider_size.value * slider_size.value - 6 - slider_bat.value;
    }
    public void hole_if()
    {
        hole_text.text = $"{slider_hole.value}/{slider_size.value * slider_size.value - 6}";
        LORD.HOLE = (int)slider_hole.value;
        slider_bat.maxValue = slider_size.value * slider_size.value - 6 - slider_hole.value;
    }
    public void size_if()
    {
        size_text.text = slider_size.value.ToString();
        LORD.LEN = (int)slider_size.value;
        slider_hole.maxValue = slider_size.value * slider_size.value - 6 - slider_bat.value;
        slider_bat.maxValue = slider_size.value * slider_size.value - 6 - slider_hole.value;
        bat_text.text = $"{slider_bat.value}/{slider_size.value * slider_size.value - 6}";
        hole_text.text = $"{slider_hole.value}/{slider_size.value * slider_size.value - 6}";
    }
    public void cast_en()
    {
        звук_кнопки.Play();
        en = !en;
        cast.SetActive(en);
    }
    public void if_moves_bat()
    {
        LORD.moves_bat = moves_bat.isOn;
    }
    /*private void FixedUpdate()
    {
        if(wumpus.transform.localRotation.z > 0.15f)
        { i_1 = false; }
        if (wumpus.transform.localRotation.z < -0.15f)
        { i_1 = true; }
        if (i_1) 
        { wumpus.transform.Rotate(0, 0, -0.1f); }
        else
        { wumpus.transform.Rotate(0, 0, 0.1f); }
        if (wumpus.transform.localScale.y>3) 
        { i_2 = false; }
        if (wumpus.transform.localScale.y < 2)
        { i_2 = true; }
        if (i_2)
        { wumpus.transform.localScale = new Vector3(wumpus.transform.localScale.x + 0.01f, wumpus.transform.localScale.y + 0.01f, wumpus.transform.localScale.z); }
        else
        { wumpus.transform.localScale = new Vector3(wumpus.transform.localScale.x - 0.01f, wumpus.transform.localScale.y - 0.01f, wumpus.transform.localScale.z); }
    }*/
}
//кол-во плиток = размер**2-6-ям-