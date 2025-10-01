using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseEnemy : Entity , IDamageable 
{
    [SerializeField] private int enemyHP;
    [SerializeField] private int damage;
    [SerializeField] private bool canAttack= false;
    [SerializeField] private float attackCD = 1;

    [SerializeField] private float range;
    [SerializeField] private float hitRange;
    [SerializeField] private float speed;

    [SerializeField] private Transform target;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        canAttack = true;
        print(ID);
    }
    void Update()
    {
        if(Vector2.Distance(transform.position,target.position) < range)
        {
            Vector2 dir = (target.position - transform.position).normalized;

            if (Vector2.Distance(transform.position, target.position) < hitRange)
            {
                //Como evito que esto se ejecute de manera recurrernte?
                if (canAttack)
                {
                    target.GetComponent<Player>().TakeDamage(1, transform.position);
                    canAttack = false;
                    speed = 0.5f;
                    Invoke("EnableAttack", attackCD);
                    print("Golpeo al jugador!!!");
                }
            }
            else
                transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
    }
    public void EnableAttack()
    {
        canAttack = true;
        speed = 3;
    }
    public void TakeDamage(int damage, Vector3 origin)
    {
        //enemyHP -= damage;
        enemyHP = Mathf.Max(0, enemyHP - damage);

        //enemyHP = Mathf.Min(100, enemyHP + damage);

        if (enemyHP <= 0)
            Destroy(gameObject);
    }


}
