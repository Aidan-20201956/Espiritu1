using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using TMPro;
using Unity.VisualScripting;

public class ManageNPCDialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger = false;
        GetComponent<BoxCollider>().size = new Vector3(1, 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
