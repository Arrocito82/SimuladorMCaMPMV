using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MemoriaCache : MonoBehaviour
{
    private List<Tuple<int, int, int, int, int, int, int, GameObject>> direccionMemoriaCache;
    private GameObject direccionTemplate;
    private int bloque, linea, dato0, dato1, dato2, dato3, dato4, dato5, dato6, dato7;
    [SerializeField] private int maximoDireccionableMC;
    private void Awake()
    {
        direccionMemoriaCache = new List<Tuple<int, int, int, int, int, int, int,GameObject>>();
        direccionTemplate=this.transform.GetChild(0).gameObject;
        //inicializando memoria cache
        for (int i=0x0; i< maximoDireccionableMC; i++)
        {
            GameObject direccionItem = Instantiate(direccionTemplate, this.transform);
            direccionItem.transform.GetChild(0).GetComponent<Text>().text = $"{i:X}";
            direccionItem.transform.GetChild(1).GetComponent<Text>().text = $"{i:X}";
            direccionItem.transform.GetChild(2).GetComponent<Text>().text = $"{i:X}";
           
            direccionMemoriaCache.Add(new Tuple<int, int, int, int, int, int, int,GameObject>(i, i,i,i,i,i,i, direccionItem));
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
    //método para agregar la direccion de la memoria caché
    /* public void addDireccionMemoriaCache(string etiqueta, string bloque, string desplazamiento){
        GameObject direccionItem=Instantiate(direccionTemplate, this.transform);
        direccionItem.transform.GetChild(0).GetComponent<Text>().text = etiqueta;
        direccionItem.transform.GetChild(1).GetComponent<Text>().text = bloque;
        direccionItem.transform.GetChild(2).GetComponent<Text>().text = desplazamiento;
    }
    public void deleteDireccionMemoriaCache(int index){
        Destroy(this.transform.GetChild(index).gameObject);
    } */
}
