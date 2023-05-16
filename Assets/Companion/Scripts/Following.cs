using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{

    public float speed;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //¬от тут надо бы проверку isThrown забацать, чтобы он не бегал за игроком, пока ультит
        //ћожно сделать все в одном файле, типа эту строку перенести на когда isThrown == false
        //ј можно сделать публичный статический класс, в котором будут только переменные хранитьс€
        //“ипа статов, хп, урон вс€ шл€па, и туда же впихнуть isThrown
        //ј так как этот класс будет статичным, то к его переменным можно будет обращатьс€ отовсюду,
        //» смотреть их и измен€ть
        //¬от так как-то
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
    }
}
