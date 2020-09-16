using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{

    Vector2 direction = Vector2.right;

    //Snail Tail track
    List<Transform> tail = new List<Transform>();

    //Eat proccess variable
    bool didSnakeEat = false;

    public GameObject tailPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
      CheckKeyMove();
      // OnTriggerEnter2D();
    }

    void CheckKeyMove(){
      if (Input.GetKey(KeyCode.RightArrow)){
        direction = Vector2.right;
      }else if (Input.GetKey(KeyCode.DownArrow)){
        direction = -Vector2.up;
      }else if (Input.GetKey(KeyCode.LeftArrow)){
        direction = -Vector2.right;
      }else if (Input.GetKey(KeyCode.UpArrow)){
        direction = Vector2.up;
      }
    }

    void Move(){
      //Save current position
      Vector2 position = transform.position;

      //Move head into new dir
      transform.Translate(direction);

      // Snake eat Apple
      if (didSnakeEat){
        GameObject tempObject = (GameObject) Instantiate(tailPrefab, position, Quaternion.identity);
        tail.Insert(0, tempObject.transform);

        didSnakeEat = false;
      }
      //Check If snake have a tail
      else if(tail.Count>0){
        tail.Last().position = position;

        tail.Insert(0, tail.Last());
        tail.RemoveAt(tail.Count-1);
      }
    }

    void OnTriggerEnter2D(Collider2D coll){
      if(coll.name.StartsWith("FoodPrefab")){
        print("+1");
        didSnakeEat = true;
        Destroy(coll.gameObject);
      }else{
        print("You lose!");
      }
    }
}
