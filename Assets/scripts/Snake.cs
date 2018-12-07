using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

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

    public Tilemap snake;
    public Tilemap fruitTable;

    public Tile snakePart;
    public Tile edge;
    public Tile fruit;

    Queue snqueue;
    Pozicija next;
    string direction;

    // Use this for initialization
    void Start()
    {
        //order by which the snake shall move
        snqueue = new Queue();

        next = new Pozicija(-1, -2);

        direction = "down";

        snqueue.Enqueue(new Pozicija(-1, 0));
        snqueue.Enqueue(new Pozicija(-1, -1));

        spawnFruit();
        InvokeRepeating("MyUpdate", 1.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!direction.Equals("left") && !direction.Equals("right") && Input.GetButtonDown("left"))
        { 

            direction = "left";
            Invoke("MyUpdate", 0f);
        }

        else if (!direction.Equals("left") && !direction.Equals("right") && Input.GetButtonDown("right"))
        {
            direction = "right";
            Invoke("MyUpdate", 0f);
        }

        else if (!direction.Equals("down") && !direction.Equals("up") && Input.GetButtonDown("down"))
        {
            direction = "down";
            Invoke("MyUpdate", 0f);
        }

        else if (!direction.Equals("down") && !direction.Equals("up") && Input.GetButtonDown("up"))
        {
            direction = "up";
            Invoke("MyUpdate", 0f);
        }
    }

    void MyUpdate()
    {
        //ce je to rob
        if (snake.GetTile(new Vector3Int(next.x, next.y, 0)) == edge
            || snake.GetTile(new Vector3Int(next.x, next.y, 0)) == snakePart)
        {
            EditorApplication.isPlaying = false;
        }

        else if (fruitTable.GetTile(new Vector3Int(next.x, next.y, 0)) == fruit)
        {
            fruitTable.SetTile(new Vector3Int(next.x, next.y, 0), null);
            snake.SetTile(new Vector3Int(next.x, next.y, 0), snakePart);
            spawnFruit();
        }

        else
        {
            snake.SetTile(new Vector3Int(next.x, next.y, 0), snakePart);
            Pozicija zac = (Pozicija)snqueue.Dequeue();
        
            snake.SetTile(new Vector3Int(zac.x, zac.y, 0), null);
        }



        snqueue.Enqueue(next);
        if (direction.Equals("down"))
        {
            next = new Pozicija(next.x, next.y - 1);
        }

        else if (direction.Equals( "up"))
        {
            next = new Pozicija(next.x, next.y + 1);
        }

        else if (direction.Equals("left"))
        {
            next = new Pozicija(next.x - 1, next.y);
        }

        else
        {
            next = new Pozicija(next.x + 1, next.y);
        }

    }

    void spawnFruit()
    {
        int zacx = (int)Random.Range(-12, 11);
        int zacy = (int)Random.Range(-9, 8);
        while (snake.GetTile(new Vector3Int(zacx, zacy, 0)) != null)
        {
            zacx = (int)Random.Range(-12, 11);
            zacy = (int)Random.Range(-9, 8);
        }
        fruitTable.SetTile(new Vector3Int(zacx, zacy, 0), fruit);
    }

}
