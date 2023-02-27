using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f; //время, через которое исчезнет диалоговое окно
    public GameObject dialogBox; //благодаря этому диалоговое окно активируется и деактивируется
    float timerDisplay; //таймер для обратного отсчёта
    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false); //диалоговое окно отключено
        timerDisplay = -1.0f; //при этом таймер уходит в минус (то есть заканчивается)
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0) //если таймер больше нуля, то:
        {
            timerDisplay -= Time.deltaTime; //убавляем его, делается обратный отсчёт
            if (timerDisplay < 0) //если таймер меньше нуля, значит он закончился, и:
            {
                dialogBox.SetActive(false); //диалоговое окно исчезает
            }
        }
    }
    public void DisplayDialog() //вызывается, когда диалоговое окно NPC должно быть активно:
    {
        timerDisplay = displayTime; //подключаем таймер к времени, через которое исчезнет диалоговое окно
        dialogBox.SetActive(true); //активируем диалоговое окно
    }
}
