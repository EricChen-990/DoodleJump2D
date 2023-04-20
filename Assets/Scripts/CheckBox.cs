using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    bool m_Started;
    public LayerMask m_LayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool NoObjectsInCollider(){

        bool hasColliders = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(transform.localScale.x, transform.localScale.y), 0, m_LayerMask);

        return !hasColliders;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
