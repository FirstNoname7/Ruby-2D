using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public AudioClip AudioClipmember;
    AudioSource audioSource;
    public GameObject projectilePrefab; //это надо чтоб создать строчку в инспекторе, в которую я поместила префаб снаряда. Я буквально вложила снаряд в руки Руби.
    public float speed = 4f;
    public int maxHealth = 5; //максимальное кол-во здоровья. Ставлю int, чтоб у перса не могло быть 4,234 здоровья. Только целые числа, только хардкор! Стоит public, чтоб это значение можно было менять в Инспекторе.
    public float timeInvincible = 2.0f; //время "бессмертия" перса, когда он на пчёлах стоит, чтоб ХП за 1 секунду не убивалось полностью

    public int health { get { return currentHealth; } } // get нужно для того, чтобы получить то, что во втором блоке. А второй блок - обычная функция возврата переменной currentHealth
    //по сути get - это такая же функция, как и простая функция. currentHealth - текущее здоровье перса
    int currentHealth; //эта переменная хранит текущее состояние здоровья перса

    bool isInvincible; //ну bool бывает true или false, так можно узнать, уязвим сейчас перс или нет
    float invincibleTimer; //показывает сколько времени осталось у перса, прежде чем он снова получит люлей

    Rigidbody2D rigidbody2d; //без этого не будет контроля над жёсткими телами (металлическими кубами)
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); //применяю в скрипте компонент жесткое тело
        animator = GetComponent<Animator>(); //применяю в игре компонент для анимации
        audioSource = GetComponent<AudioSource>(); //применяю в игре компонент для звуков

        currentHealth = maxHealth; //я установила, что перс в начале игры будет здоровым как бык (максимальное ХП). Если удалить эту строку, то в начале игры здоровье = 0

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal"); //слово "Horizontal" устанавливает, что при нажатии на стрелки "вправо" и "влево" персонаж будет двигаться вправо и влево
        vertical = Input.GetAxis("Vertical"); //слово "Vertical" устанавливает, что при нажатии на стрелки "вверх" и "вниз" персонаж будет двигаться вверх и вниз
        //кстати, GetAxis - это такая же функция, как и Update, только она по дефолту встроена в юнити. В общем юнитовские программисты заморочились, чтоб тебе не надо было морочиться и писать такую функцию заново.
        Vector2 move = new Vector2(horizontal, vertical); //благодаря этой строке я привязываю направление Руби к его перемещению
        //на строку ниже проверка, не равно ли move.x и move.у нулю (не стоит ли Руби на месте). Вместо == здесь Approximately (дословно - приблизительно), потому что используется float, то есть если поставить "=", то точность будет оч снижена. У перса может быть значение 0,000001 и равенство видит это число равным нулю. А в анимации это не так работает. Итак, если Руби движется, то:
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y); //Руби должна смотреть  в направлении, в котором она движется.
            lookDirection.Normalize(); //тут нормализация направления взгляда Руби. 
        }
        //следующие 3 строки отправляют аниматору данные, а именно: направление взгляда по горизонтали, направление взгляда по вертикали, скорость (длина вектора движения. То есть если вектор = 0, то Руби стоит на месте)
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible) //если перс неуязвим, то:
        {
            invincibleTimer -= Time.deltaTime; //включается таймер
            if (invincibleTimer < 0) //если таймер меньше нуля (то есть он закончился), то:
                isInvincible = false; //отключаем неуязвимость
            
        }

        if (Input.GetKeyDown(KeyCode.C)) //если игрок нажал клавишу С на клавиатуре, то: (более подробно:
                                         //Input - получаем ввод игрока
                                         //GetKeyDown - при клике
                                         //KeyCode.C - на клавишу С)
        {
            Launch(); //запускается функция с названием Launch
        }
        if (Input.GetKeyDown(KeyCode.X)) //при нажатии на клавишу Х
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC")); //компонент RaycastHit2D прописана под названием переменной hit и отражает:
            //Physics2D.Raycast - это такой компонент физической системы, который позволит  заговорить персу с другим персом
            //он включает в себя: rigidbody2d.position + Vector2.up - то есть перс (Руби)
            //lookDirection - направление, куда смотрит Руби (чтоб разговаривать с другим персом, глядя на него, а не в сторону)
            //1.5f - максимальное расстояние луча. То есть если Руби не доходит до этого луча, то не сможет заговорить с другим персом (ибо он оч далеко находится)
            //LayerMask.GetMask("NPC") - указание слоя перса, с которым можно поболтать. Другие слои не будут реагировать, если рядом с ними кнопку Х нажать
            if (hit.collider != null) //если взаимодействуем с другим персом, то:
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>(); //используем переменную character, которая связывает этот скрипт с скриптом под названием NonPlayerCharacter
                //В этой переменной используется компонент NonPlayerCharacter (то есть скрипт с таким названием)
                if (character != null) //если скрипт NonPlayerCharacter активен (если Руби хочет поболтать с персом), то:
                {
                    character.DisplayDialog(); //вызывается функция с названием DisplayDialog, которая находится в скрипте NonPlayerCharacter
                }
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position; //Vector2 устанавливает положение Руби по вертикали и горизонтали. position - название переменной, может быть любым.
        position.x = position.x + speed * horizontal * Time.deltaTime; //тут Руби двигается по горизонтали. Time.deltaTime нужно для независимости движения Руби от мощности компа, с которого игрок сидит. Всё опирается на время, а не только на фреймы.
        position.y = position.y + speed * vertical * Time.deltaTime; //тут Руби двигается по вертикали. Time.deltaTime нужно для независимости движения Руби от мощности компа, с которого игрок сидит. Всё опирается на время, а не только на фреймы.
        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount) //эта функция для изменения ХП перса. Тут в скобках есть параметр, указывающий, что аптечка прибавляет целое число ХП и выражается словом amount. Эта функция public, ибо она используется в скрипте другом. Без неё функция была бы локальной и в других скриптах на неё ссылаться или обращаться к ней нельзя. 
    {
        if (amount < 0) //если ХП меньше нуля, то:
        {
            animator.SetTrigger("Hit"); //анимация Руби, когда её лупят (бьют)
            if (isInvincible) //если перс неуязвим сейчас,то: (без этого условия перс будет раниться каждый фрейм, то есть оч быстро)
                return; //возвращаемся обратно, не раним его
            isInvincible = true; //но если перс уязвим, то делаем его неуязвимым (без этого перс будет раниться каждый фрейм, то есть оч быстро)
            invincibleTimer = timeInvincible; //снова включается таймер (без этого перс будет раниться каждый фрейм, то есть оч быстро)
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); //это ограничитель ХП. То есть ХП перса может меняться (currentHealth + amount), но не может быть ниже 0 и выше maxHealth (последние 2 параметра).
        //Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth); //UIHealthBar.instance - переменная из скрипта UIHealthBar (см.подробные подсказки там)
                                                                         //.SetValue - используется функция с таким названием из скрипта UIHealthBar
                                                                         //currentHealth - текущее здоровье перса. (float)maxHealth - максимальное здоровье перса.
                                                                         //перед этим пишу тип числа float, потому что ползунок использует числа от 0 до 1, если поставить int, то есть целое число, то херня получится
    }
    void Launch() //вызывается, когда ты нажимаешь на кнопку, чтоб выпустить снаряд
    {
        PlaySound(AudioClipmember);
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity); //тут создаются копии снарядов 
        //Instantiate - создание объекта (нужно для создания копий уже существующих объектов (чтоб пулять можно было бесконечно много,а не один раз))
        //projectilePrefab - объект, который будет копироваться 
        //rigidbody2d.position + Vector2.up * 0.5f - копирование объекта с сохранением позиции и перемещением по горизонтали/вертикали, умноженным на скорость перемещения
        //Quaternion.identity - убираю вращение объекта. Если поставить просто Quaternion, то будет вращаться

        Projectile projectile = projectileObject.GetComponent<Projectile>(); //ввожу переменную projectile, чтобы использовать в этом скрипте скрипт с названием Projectile
        projectile.Launch(lookDirection, 300); //указываю направление, в которое смотрит Руби (шоб снаряд летел туда, куда Руби смотрит) и указываю силу, с которой снаряд будет лететь (300 Ньютонов)

        animator.SetTrigger("Launch"); //включаю аниматора, чтобы запустить анимацию, когда объект чего-нибудь триггерного коснётся
    }
    public void PlaySound(AudioClip clip) //функция для звуков. Переменная clip отвечает за воспроизведение звука. Она общедоступная, чтоб можно было её использовать в других скриптах
    {
        audioSource.PlayOneShot(clip); //воспроизводится звук
    }
}
