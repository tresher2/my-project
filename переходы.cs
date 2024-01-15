using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class переходы : MonoBehaviour
{
    public AudioSource звук_кнопки;
    void Start()
    {
        звук_кнопки.volume = LORD.слайдер * 0.5f;
    }
    public void играть()
    {
        звук_кнопки.Play();
        SceneManager.LoadScene(1);
    }
    public void играть_л()
    {
        звук_кнопки.Play();
        LORD.HOLE = 2;
        LORD.BAT = 2;
        LORD.LEN = 6;
        LORD.moves_bat = false;
        SceneManager.LoadScene(1);
    }
    public void играть_ср()
    {
        звук_кнопки.Play();
        LORD.HOLE = 3;
        LORD.BAT = 4;
        LORD.LEN = 8;
        LORD.moves_bat = false;
        SceneManager.LoadScene(1);
    }
    public void играть_с()
    {
        звук_кнопки.Play();
        LORD.HOLE = 5;
        LORD.BAT = 4;
        LORD.LEN = 8;
        LORD.moves_bat = true;
        LORD.moves_bat_count = 20;
        SceneManager.LoadScene(1);
    }
    public void играть_к()
    {
        звук_кнопки.Play();
        SceneManager.LoadScene(1);
    }
    public void меню()
    {
        звук_кнопки.Play();
        SceneManager.LoadScene(0);
    }
    public void выход()
    {
        звук_кнопки.Play();
        Application.Quit();
    }
    public void обучение()
    {
        звук_кнопки.Play();
        SceneManager.LoadScene(2);
    }
    public void настройки()
    {
        Debug.Log(LORD.экранные_стрелочки);
        звук_кнопки.Play();
        SceneManager.LoadScene(3);
    }
}
