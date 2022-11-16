using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoriaVirtual : MonoBehaviour
{
    private List<DireccionMemoriaVirtual> direccionesMemoriaVirtual;
    private GameObject direccionTemplate;
    private void Awake()
    {
        //direccionesMemoriaVirtual = new List<DireccionMemoriaVirtual>();
        //recuperando el primer elemento que servira de template
        direccionTemplate = this.transform.GetChild(0).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addDireccionMemoriaVirtual(string direccion)
    {

        GameObject direccionItem = Instantiate(direccionTemplate, this.transform);// agregando al view
        direccionItem.transform.GetChild(0).GetComponent<Text>().text = direccion;
        //direccionesMemoriaVirtual.Add(direccion); //agregando al list 
    }
    public void deleteDireccionMemoriaVirtual(int index)
    {
        Destroy(this.transform.GetChild(index).gameObject);//probando eliminar un elemento del array del view
        //direccionesMemoriaVirtual.RemoveAt(index);// eliminando de la lista
    }
}
