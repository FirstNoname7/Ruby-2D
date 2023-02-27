using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    //в целом скрипт стырен со скрипта HealthCollectible, ибо тут разница невелика - это скрипт тырит здоровье перса, а тот - прибавляет
    void OnTriggerStay2D(Collider2D other) //меняю OnTriggerEnter2D на OnTriggerStay2D, чтобы перс получал люлей от пчёл каждую секунду, которую он стоит на них
    {
        RubyController controller = other.GetComponent<RubyController>(); //переменная controller появляется. Она подключается к скрипту RubyController, чтобы проверить следующие условия:

        if (controller != null) //если переменная controller (то есть перс) по отношению к триггеру не равен нулю (то есть находится на триггере), тогда:
        {
            controller.ChangeHealth(-1); //убавляется ХП. здесь в скобках указано сколько ХП убавляется у перса            
        }
    }
}