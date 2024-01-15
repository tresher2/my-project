using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class настройки : MonoBehaviour
{
    public TMPro.TMP_Dropdown wasd,стрелочки,экранные_стрелочки;
    public Slider слайдер;
    /*public TMPro.TMP_InputField w, a, s, d;
    public GameObject w_1, a_1, s_1, d_1;*/
    public void изменение_wasd()
    {
        LORD.WASD = wasd.value;
    }
    public void изменение_экран()
    {
        LORD.экранные_стрелочки = экранные_стрелочки.value;
    }
    public void изменение_стрелочки()
    {
        LORD.стрелочки = стрелочки.value;
    }
    public void Start()
    {
        wasd.value = LORD.WASD;
        стрелочки.value = LORD.стрелочки;
        экранные_стрелочки.value= LORD.экранные_стрелочки;
        слайдер.value = LORD.слайдер;
    }
    public void слайдерc()
    {
        LORD.слайдер = слайдер.value;
    }
    /*public void W_chan()
    {
        w_1.SetActive(false);
        w_1.SetActive(true);
    }
    public void W_sel()
    {
        w.text = "";
    }*/
}
