using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    bool broken = true; //условие: врага починили снарядом

    Rigidbody2D rigidbody2d;
    Animator animator; //достаю из юнити компонент анимации, чтоб использовать его в этом скрипте
    float timer;
    int direction = 1;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>(); //применяю компонент по анимации
    }
    void Update()
    {
        if (!broken) //проверяем, починили ли врага. Если не починили, то:
        {
            return; //возвращаемся в игру, делаем вид что ничего не произошло
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        if (!broken) //проверяем, починили ли врага. Если не починили, то:
        {
            return; //возвращаемся в игру, делаем вид что ничего не произошло
        }


        Vector2 position = rigidbody2d.position; //Vector2 устанавливает положение врага по вертикали и горизонтали. position - название переменной, может быть любым.
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed*direction; //тут враг двигается по вертикали. Time.deltaTime нужно для независимости движения врага от мощности компа, с которого игрок сидит. Всё опирается на время, а не только на фреймы. Умножаем на direction, чтобы видеть направление врага.
            animator.SetFloat("Move X", 0); //подключаю анимацию по горизонтали (ноль, ибо перс движется по вертикали в этом условии)
            animator.SetFloat("Move Y", direction); //подключаю анимацию по вертикали
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed*direction; //тут враг двигается по горизонтали. Time.deltaTime нужно для независимости движения врага от мощности компа, с которого игрок сидит. Всё опирается на время, а не только на фреймы. Умножаем на direction, чтобы видеть направление врага.
            animator.SetFloat("Move X", direction); //подключаю анимацию по горизонтали
            animator.SetFloat("Move Y", 0); //подключаю анимацию по вертикали (ноль, ибо перс движется по горизонтали в этом условии)

        }
        rigidbody2d.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other) //функция. Название зарезервированное в юнити. Вызывается при столкновении твёрдого тела (rigidbody) с чем-то. 
    {
        RubyController player = other.gameObject.GetComponent<RubyController>(); //переменная player появляется. (здесь в отличие от скрипта HealthCollectible добавляется gameObject из-за того, что функция называется Collision, а не Trigger). Она подключается к скрипту RubyController, чтобы проверить следующие условия:
        if (player != null)
        {
            player.ChangeHealth(-1); //убавляется ХП. здесь в скобках указано сколько ХП убавляется у перса
        }
    }
    public void Fix() //функция: если врага починили снарядом, то:
    {
        broken = false;
        rigidbody2d.simulated = false; //удаляем объект как физическое тело (чиним врага)
        smokeEffect.Stop(); //останавливается дым из робота. Использую именно Stop, а не Destroy, чтобы дым исчезал медленно.
                            //Если не поняла почему, то раскомментируй строчку ниже и сравни как дым исчезает при stop, и как при destroy
                            //Destroy(smokeEffect);
        animator.SetTrigger("Fixed"); //тут я устанавливаю,что если во врага запустили снаряд, то он будет танцевать (будет показана анимация с названием Fixed)

    }
}
