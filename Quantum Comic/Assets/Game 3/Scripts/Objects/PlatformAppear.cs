using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAppear : MonoBehaviour
{
    public Collider2D Coll { get; private set; }
    [SerializeField] private SpriteRenderer objectSprite;
    private Color tmp;
    [SerializeField] private Button button;
    [SerializeField] private float speed;

    private void Start()
    {
        Coll = GetComponent<Collider2D>();
        tmp = objectSprite.color;
    }

    private void Update()
    {
        if (button.buttonActivated)
        {
            tmp.a = Mathf.Lerp(tmp.a, 1, speed * Time.deltaTime);
            Coll.enabled = true;
        }
        else
        {
            tmp.a = Mathf.Lerp(tmp.a, 0, speed * Time.deltaTime);
            Coll.enabled = false;
        }
        objectSprite.color = tmp;
    }
}
