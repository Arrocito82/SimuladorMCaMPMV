using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriaVirtual : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //recuperando el primer elemento que servira de template
        GameObject direccionTemplate = this.transform.GetChild(0).gameObject;
        for(int itemIndex=0; itemIndex<64; itemIndex++)
        {
            GameObject direccionItem = Instantiate(direccionTemplate, this.transform);
            direccionItem.transform.GetChild(0).GetComponent<Text>().text = "Página" + itemIndex;
        }
        Destroy(this.transform.GetChild(3).gameObject);//probando eliminar un elemento del array
        Destroy(direccionTemplate);// eliminando el item template del view
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
