using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public AudioClip AudioClipmember;
    AudioSource audioSource;
    public GameObject projectilePrefab; //��� ���� ���� ������� ������� � ����������, � ������� � ��������� ������ �������. � ��������� ������� ������ � ���� ����.
    public float speed = 4f;
    public int maxHealth = 5; //������������ ���-�� ��������. ������ int, ���� � ����� �� ����� ���� 4,234 ��������. ������ ����� �����, ������ �������! ����� public, ���� ��� �������� ����� ���� ������ � ����������.
    public float timeInvincible = 2.0f; //����� "����������" �����, ����� �� �� ������ �����, ���� �� �� 1 ������� �� ��������� ���������

    public int health { get { return currentHealth; } } // get ����� ��� ����, ����� �������� ��, ��� �� ������ �����. � ������ ���� - ������� ������� �������� ���������� currentHealth
    //�� ���� get - ��� ����� �� �������, ��� � ������� �������. currentHealth - ������� �������� �����
    int currentHealth; //��� ���������� ������ ������� ��������� �������� �����

    bool isInvincible; //�� bool ������ true ��� false, ��� ����� ������, ������ ������ ���� ��� ���
    float invincibleTimer; //���������� ������� ������� �������� � �����, ������ ��� �� ����� ������� �����

    Rigidbody2D rigidbody2d; //��� ����� �� ����� �������� ��� ������� ������ (�������������� ������)
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); //�������� � ������� ��������� ������� ����
        animator = GetComponent<Animator>(); //�������� � ���� ��������� ��� ��������
        audioSource = GetComponent<AudioSource>(); //�������� � ���� ��������� ��� ������

        currentHealth = maxHealth; //� ����������, ��� ���� � ������ ���� ����� �������� ��� ��� (������������ ��). ���� ������� ��� ������, �� � ������ ���� �������� = 0

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal"); //����� "Horizontal" �������������, ��� ��� ������� �� ������� "������" � "�����" �������� ����� ��������� ������ � �����
        vertical = Input.GetAxis("Vertical"); //����� "Vertical" �������������, ��� ��� ������� �� ������� "�����" � "����" �������� ����� ��������� ����� � ����
        //������, GetAxis - ��� ����� �� �������, ��� � Update, ������ ��� �� ������� �������� � �����. � ����� ���������� ������������ ������������, ���� ���� �� ���� ���� ���������� � ������ ����� ������� ������.
        Vector2 move = new Vector2(horizontal, vertical); //��������� ���� ������ � ���������� ����������� ���� � ��� �����������
        //�� ������ ���� ��������, �� ����� �� move.x � move.� ���� (�� ����� �� ���� �� �����). ������ == ����� Approximately (�������� - ��������������), ������ ��� ������������ float, �� ���� ���� ��������� "=", �� �������� ����� �� �������. � ����� ����� ���� �������� 0,000001 � ��������� ����� ��� ����� ������ ����. � � �������� ��� �� ��� ��������. ����, ���� ���� ��������, ��:
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y); //���� ������ ��������  � �����������, � ������� ��� ��������.
            lookDirection.Normalize(); //��� ������������ ����������� ������� ����. 
        }
        //��������� 3 ������ ���������� ��������� ������, � ������: ����������� ������� �� �����������, ����������� ������� �� ���������, �������� (����� ������� ��������. �� ���� ���� ������ = 0, �� ���� ����� �� �����)
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible) //���� ���� ��������, ��:
        {
            invincibleTimer -= Time.deltaTime; //���������� ������
            if (invincibleTimer < 0) //���� ������ ������ ���� (�� ���� �� ����������), ��:
                isInvincible = false; //��������� ������������
            
        }

        if (Input.GetKeyDown(KeyCode.C)) //���� ����� ����� ������� � �� ����������, ��: (����� ��������:
                                         //Input - �������� ���� ������
                                         //GetKeyDown - ��� �����
                                         //KeyCode.C - �� ������� �)
        {
            Launch(); //����������� ������� � ��������� Launch
        }
        if (Input.GetKeyDown(KeyCode.X)) //��� ������� �� ������� �
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC")); //��������� RaycastHit2D ��������� ��� ��������� ���������� hit � ��������:
            //Physics2D.Raycast - ��� ����� ��������� ���������� �������, ������� ��������  ���������� ����� � ������ ������
            //�� �������� � ����: rigidbody2d.position + Vector2.up - �� ���� ���� (����)
            //lookDirection - �����������, ���� ������� ���� (���� ������������� � ������ ������, ����� �� ����, � �� � �������)
            //1.5f - ������������ ���������� ����. �� ���� ���� ���� �� ������� �� ����� ����, �� �� ������ ���������� � ������ ������ (��� �� �� ������ ���������)
            //LayerMask.GetMask("NPC") - �������� ���� �����, � ������� ����� ���������. ������ ���� �� ����� �����������, ���� ����� � ���� ������ � ������
            if (hit.collider != null) //���� ��������������� � ������ ������, ��:
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>(); //���������� ���������� character, ������� ��������� ���� ������ � �������� ��� ��������� NonPlayerCharacter
                //� ���� ���������� ������������ ��������� NonPlayerCharacter (�� ���� ������ � ����� ���������)
                if (character != null) //���� ������ NonPlayerCharacter ������� (���� ���� ����� ��������� � ������), ��:
                {
                    character.DisplayDialog(); //���������� ������� � ��������� DisplayDialog, ������� ��������� � ������� NonPlayerCharacter
                }
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position; //Vector2 ������������� ��������� ���� �� ��������� � �����������. position - �������� ����������, ����� ���� �����.
        position.x = position.x + speed * horizontal * Time.deltaTime; //��� ���� ��������� �� �����������. Time.deltaTime ����� ��� ������������� �������� ���� �� �������� �����, � �������� ����� �����. �� ��������� �� �����, � �� ������ �� ������.
        position.y = position.y + speed * vertical * Time.deltaTime; //��� ���� ��������� �� ���������. Time.deltaTime ����� ��� ������������� �������� ���� �� �������� �����, � �������� ����� �����. �� ��������� �� �����, � �� ������ �� ������.
        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount) //��� ������� ��� ��������� �� �����. ��� � ������� ���� ��������, �����������, ��� ������� ���������� ����� ����� �� � ���������� ������ amount. ��� ������� public, ��� ��� ������������ � ������� ������. ��� �� ������� ���� �� ��������� � � ������ �������� �� �� ��������� ��� ���������� � ��� ������. 
    {
        if (amount < 0) //���� �� ������ ����, ��:
        {
            animator.SetTrigger("Hit"); //�������� ����, ����� � ����� (����)
            if (isInvincible) //���� ���� �������� ������,��: (��� ����� ������� ���� ����� �������� ������ �����, �� ���� �� ������)
                return; //������������ �������, �� ����� ���
            isInvincible = true; //�� ���� ���� ������, �� ������ ��� ���������� (��� ����� ���� ����� �������� ������ �����, �� ���� �� ������)
            invincibleTimer = timeInvincible; //����� ���������� ������ (��� ����� ���� ����� �������� ������ �����, �� ���� �� ������)
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); //��� ������������ ��. �� ���� �� ����� ����� �������� (currentHealth + amount), �� �� ����� ���� ���� 0 � ���� maxHealth (��������� 2 ���������).
        //Debug.Log(currentHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth); //UIHealthBar.instance - ���������� �� ������� UIHealthBar (��.��������� ��������� ���)
                                                                         //.SetValue - ������������ ������� � ����� ��������� �� ������� UIHealthBar
                                                                         //currentHealth - ������� �������� �����. (float)maxHealth - ������������ �������� �����.
                                                                         //����� ���� ���� ��� ����� float, ������ ��� �������� ���������� ����� �� 0 �� 1, ���� ��������� int, �� ���� ����� �����, �� ����� ���������
    }
    void Launch() //����������, ����� �� ��������� �� ������, ���� ��������� ������
    {
        PlaySound(AudioClipmember);
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity); //��� ��������� ����� �������� 
        //Instantiate - �������� ������� (����� ��� �������� ����� ��� ������������ �������� (���� ������ ����� ���� ���������� �����,� �� ���� ���))
        //projectilePrefab - ������, ������� ����� ������������ 
        //rigidbody2d.position + Vector2.up * 0.5f - ����������� ������� � ����������� ������� � ������������ �� �����������/���������, ���������� �� �������� �����������
        //Quaternion.identity - ������ �������� �������. ���� ��������� ������ Quaternion, �� ����� ���������

        Projectile projectile = projectileObject.GetComponent<Projectile>(); //����� ���������� projectile, ����� ������������ � ���� ������� ������ � ��������� Projectile
        projectile.Launch(lookDirection, 300); //�������� �����������, � ������� ������� ���� (��� ������ ����� ����, ���� ���� �������) � �������� ����, � ������� ������ ����� ������ (300 ��������)

        animator.SetTrigger("Launch"); //������� ���������, ����� ��������� ��������, ����� ������ ����-������ ����������� �������
    }
    public void PlaySound(AudioClip clip) //������� ��� ������. ���������� clip �������� �� ��������������� �����. ��� �������������, ���� ����� ���� � ������������ � ������ ��������
    {
        audioSource.PlayOneShot(clip); //��������������� ����
    }
}
