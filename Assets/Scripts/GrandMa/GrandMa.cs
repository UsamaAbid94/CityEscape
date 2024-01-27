using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandMa : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D grandMaBody;

    private void Awake()
    {
        grandMaBody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        grandMaBody.velocity = Vector2.up * moveSpeed * Time.fixedDeltaTime;
        
    }
}
