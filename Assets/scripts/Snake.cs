using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Snake : MonoBehaviour
{

    class Pozicija
    {
        public int x;
        public int y;

        public Pozicija(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    Queue snqueue;
    public Tilemap snake;
    public Tile snakePart;
    Pozicija next;

    // Use this for initialization
    void Start()
    {
        //order by which the snake shall move
        snqueue = new Queue();

        next = new Pozicija(-1, -2);

        snqueue.Enqueue(new Pozicija(-1, 0));
        //elem = new Pozicija(-1, -1);
        snqueue.Enqueue(new Pozicija(-1, -1));
        /*
        Debug.Log(((Pozicija)snqueue.Peek()).y);
        snqueue.Dequeue();
        Debug.Log(((Pozicija)snqueue.Peek()).y);
        */
        /*
        for (int i = -10; i <= 10; i++)
        {
            for (int k = -10; k <= 10; k++)
            {
                if (snake.GetTile(new Vector3Int(i, k, 0)) != null)
                {

                    Debug.Log("i = " + i + " k = " + k);
                }
            }
        }
        */

        InvokeRepeating("MyUpdate", 4.0f,0.3f);
    }

    // Update is called once per frame

    void MyUpdate()
    {
        snake.SetTile(new Vector3Int(next.x, next.y, 0), snakePart);

        Pozicija zac = (Pozicija)snqueue.Dequeue();

        snake.SetTile(new Vector3Int(zac.x, zac.y, 0), null);

        snqueue.Enqueue(next);
        next = new Pozicija(next.x, next.y - 1);
    }

}
