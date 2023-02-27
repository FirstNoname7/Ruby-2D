using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //этот скрипт нужен дл€ визуальной части ’ѕ перса –уби (дл€ ползунка слева сверху)
    public static UIHealthBar instance { get; private set; } //общедоступна€ переменна€ с названием instance €вл€етс€ статичной (static), то есть можно в любом скрипте написать
    //UIHealthBar.instance и вызоветс€ свойство get. а set € закрыла при помощи private, чтоб никто не мог мен€ть это свойство извне этого скрипта
    //то есть если написать в любом скрипте UIHealthBar.instance, тогда будет показано значение бара здоровь€
    public Image mask; //здесь вставл€ем нужное изображение  в инспекторе
    float originalSize; //здесь указываем,что изображение должно быть в своЄм оригинальном размере
    void Awake() //перед стартом
    {
        instance=this; //переменна€ instance (котора€ уже создана чуть выше) €вл€етс€ статичной, то есть может использоватьс€ где угодно
        //this - это специальное ключевое слово C#, означающее Уобъект, который в данный момент выполн€ет эту функциюФ.
    }
    void Start()
    {
        originalSize = mask.rectTransform.rect.width; //mask.rectTransform - за оригинальный размер берЄм тот, который установлен в маске
        //rect.width - ширина может мен€тьс€ (когда мен€етс€ ’ѕ соответственно)
    }

    // Update is called once per frame
    public void SetValue(float value) //доступна€ функци€ дл€ прив€зки изменени€ здоровь€ к полоске изменени€ здоровь€ перса.
                                      //Ќазываетс€ именно SetValue, чтобы использовать значени€ от 0 до 1 и исход€ из них мен€ть вид ползунка, скрыва€ правую часть.
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value); //SetSizeWithCurrentAnchors - прив€зывает размер ползунка к €корю Anchors
        //RectTransform.Axis.Horizontal - мен€етс€ размер только по горизонтали
        //originalSize * value - само изменение размера: оригинальный размер умножаетс€ на созданную переменную, котора€ прив€зана к изменению здоровь€ перса
    }
}
