using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    bool broken = true; //�������: ����� �������� ��������

    Rigidbody2D rigidbody2d;
    Animator animator; //������ �� ����� ��������� ��������, ���� ������������ ��� � ���� �������
    float timer;
    int direction = 1;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>(); //�������� ��������� �� ��������
    }
    void Update()
    {
        if (!broken) //���������, �������� �� �����. ���� �� ��������, ��:
        {
            return; //������������ � ����, ������ ��� ��� ������ �� ���������
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
        if (!broken) //���������, �������� �� �����. ���� �� ��������, ��:
        {
            return; //������������ � ����, ������ ��� ��� ������ �� ���������
        }


        Vector2 position = rigidbody2d.position; //Vector2 ������������� ��������� ����� �� ��������� � �����������. position - �������� ����������, ����� ���� �����.
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed*direction; //��� ���� ��������� �� ���������. Time.deltaTime ����� ��� ������������� �������� ����� �� �������� �����, � �������� ����� �����. �� ��������� �� �����, � �� ������ �� ������. �������� �� direction, ����� ������ ����������� �����.
            animator.SetFloat("Move X", 0); //��������� �������� �� ����������� (����, ��� ���� �������� �� ��������� � ���� �������)
            animator.SetFloat("Move Y", direction); //��������� �������� �� ���������
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed*direction; //��� ���� ��������� �� �����������. Time.deltaTime ����� ��� ������������� �������� ����� �� �������� �����, � �������� ����� �����. �� ��������� �� �����, � �� ������ �� ������. �������� �� direction, ����� ������ ����������� �����.
            animator.SetFloat("Move X", direction); //��������� �������� �� �����������
            animator.SetFloat("Move Y", 0); //��������� �������� �� ��������� (����, ��� ���� �������� �� ����������� � ���� �������)

        }
        rigidbody2d.MovePosition(position);
    }
    void OnCollisionEnter2D(Collision2D other) //�������. �������� ����������������� � �����. ���������� ��� ������������ ������� ���� (rigidbody) � ���-��. 
    {
        RubyController player = other.gameObject.GetComponent<RubyController>(); //���������� player ����������. (����� � ������� �� ������� HealthCollectible ����������� gameObject ��-�� ����, ��� ������� ���������� Collision, � �� Trigger). ��� ������������ � ������� RubyController, ����� ��������� ��������� �������:
        if (player != null)
        {
            player.ChangeHealth(-1); //���������� ��. ����� � ������� ������� ������� �� ���������� � �����
        }
    }
    public void Fix() //�������: ���� ����� �������� ��������, ��:
    {
        broken = false;
        rigidbody2d.simulated = false; //������� ������ ��� ���������� ���� (����� �����)
        smokeEffect.Stop(); //��������������� ��� �� ������. ��������� ������ Stop, � �� Destroy, ����� ��� ������� ��������.
                            //���� �� ������ ������, �� �������������� ������� ���� � ������ ��� ��� �������� ��� stop, � ��� ��� destroy
                            //Destroy(smokeEffect);
        animator.SetTrigger("Fixed"); //��� � ������������,��� ���� �� ����� ��������� ������, �� �� ����� ��������� (����� �������� �������� � ��������� Fixed)

    }
}
