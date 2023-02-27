using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //этот скрипт для снаряда используется (для того, чем чинить врагов)
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake() //заменяю функцию Start на Awake, чтобы эта штука работала перед стартом (во избежание ошибки, связанной с тем, что при старте вызывается объект, которого нет)
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    public void Launch(Vector2 direction, float force) //в скобках указано направление снаряда (Vector2 - то есть в плоскости x и y, direction - указывает направление, force - сила, с которой врагу прилетит снарядом по роже, float - потому что идут значения от -1 до 1)
    {
        rigidbody2d.AddForce(direction * force);  //доп. расчёт = сила удара снаряда = направление снаряда * сила снаряда
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f) //условие "если снаряд дальше на 1000 единиц от середины экрана, тогда:"
        {
            Destroy(gameObject); //он уничтожается. Это надо, когда снаряд улетает за пределы экрана. А то если мильон снарядов выпустить и они будут за пределами игры бесконечно лететь, то игра необоснованно лагать будет, мде
        }
    }
    void OnCollisionEnter2D(Collision2D other) //нужна, чтоб обнаружить столкновение. Collision2D в скобках используется под именем переменной other
    {
        EnemyController e = other.collider.GetComponent<EnemyController>(); //ссылаемся на коллайдер в скрипте с названием EnemyController при помощи переменной с названием "е"
        if (e!=null) //условие: если переменная е = false, тогда:
        {
            e.Fix(); //переменная использует функцию с именем Fix (она находится в скрипте EnemyController)
        }
        Destroy(gameObject); //и ещё объект удалится (починим врага)
    }
}
