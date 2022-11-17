using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class MemoriaCache : MonoBehaviour
{
    private List<Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>> direccionMemoriaCache;
    private GameObject direccionTemplate;
    private int bloque, linea, dato0, dato1, dato2, dato3, dato4, dato5, dato6, dato7;
    [SerializeField] private int maximoDireccionableMC;
    private void Awake()
    {
        direccionMemoriaCache = new List<Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>>();
        direccionTemplate=this.transform.GetChild(0).gameObject;
        //inicializando memoria cache
        for (int i=0x0; i< maximoDireccionableMC; i++)
        {
            GameObject direccionItem = Instantiate(direccionTemplate, this.transform);
            direccionItem.transform.GetChild(0).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(1).GetComponent<Text>().text = $"{i:X1}";
            direccionItem.transform.GetChild(2).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(3).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(4).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(5).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(6).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(7).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(8).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(9).GetComponent<Text>().text = $"{i:X2}";
            Tuple<int, int, int, int> datos = new Tuple<int, int, int, int>(i, i, i, i);
            direccionMemoriaCache.Add(new Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> (i, i, direccionItem, datos, datos));
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
