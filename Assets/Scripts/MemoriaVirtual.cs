using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MemoriaVirtual : MonoBehaviour
{
    private List<Tuple<int, int, GameObject>> direccionesMemoriaVirtual;
    private GameObject direccionTemplate;
    [SerializeField] private int maximoDireccionableMV;
    private void Awake()
    {
        direccionesMemoriaVirtual = new List<Tuple<int, int, GameObject>>();
        //recuperando el primer elemento que servira de template
        direccionTemplate = this.transform.GetChild(0).gameObject;

        //inicializando memoria virtual
        for (int i=0x0; i< maximoDireccionableMV; i++)
        {
            GameObject direccionItem = Instantiate(direccionTemplate, this.transform);
            direccionItem.transform.GetChild(0).GetComponent<Text>().text = $"{i:X}";
            direccionItem.transform.GetChild(1).GetComponent<Text>().text = $"{i:X}";
            direccionesMemoriaVirtual.Add(new Tuple<int,int, GameObject>(i, i, direccionItem));
        }
        Destroy(direccionTemplate);
        
    }
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void addDireccionMemoriaVirtual(int pagina, int desplazamiento)
    //{
        
    //    DireccionMemoriaVirtual currentDireccion = new DireccionMemoriaVirtual(pagina, desplazamiento);
    //    //direccionesMemoriaVirtual.Add(currentDireccion);// eliminando de la lista
    //}
    //public void deleteDireccionMemoriaVirtual(int pagina, int desplazamiento)
    //{
    //    DireccionMemoriaVirtual currentDireccion = new DireccionMemoriaVirtual(pagina, desplazamiento);

    //    direccionesMemoriaVirtual.Remove(currentDireccion);// eliminando de la lista
    //    //Destroy(this.transform.GetChild(index).gameObject);//probando eliminar un elemento del array del view
    //}
}
