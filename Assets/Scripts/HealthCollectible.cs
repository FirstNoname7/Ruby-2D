using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other) //функция. Название зарезервированное в юнити.
    {
        RubyController controller = other.GetComponent<RubyController>(); //переменная controller появляется. Она подключается к скрипту RubyController, чтобы проверить следующие условия:

        if (controller != null) //если переменная controller (то есть перс) по отношению к триггеру не равен нулю (то есть находится на триггере), тогда:
        {
            if (controller.health < controller.maxHealth) //ещё доп.условие: если текущее здоровье меньше максимального здоровья, тогда:
            {
                controller.ChangeHealth(1); //прибавляется ХП. здесь в скобках указано сколько ХП прибавляется персу
                Destroy(gameObject); //когда перс взял аптечку - она пропадает
                controller.PlaySound(collectedClip); //помимо того, что она исчезает, проигрывается звук, помещенный в инспекторе в открытую переменную с именем collectedClip
                //звук проигрывается при помощи функции PlaySound, которая помещена в скрипте RubyController

            }
        }

    }
}
