using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldEcon.Entities;

[ExecuteInEditMode]
public class PersonVisual : MonoBehaviour
{
    public Person thisPerson;

    // Start is called before the first frame update
    void Start()
    {
        thisPerson = GetComponent<Person>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
