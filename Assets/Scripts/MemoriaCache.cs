using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class MemoriaCache : MonoBehaviour
{
    private List<Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>> direccionMemoriaCache;
    private GameObject direccionTemplate;
    [SerializeField] private int maximoDireccionableMC;
    [SerializeField] private GameObject botonVer;
    [SerializeField] private GameObject bloqueTextField;
    [SerializeField] private GameObject lineaTextField;
    private Text bloque, linea;
    // [SerializeField] private GameObject memoriaPrincipal;
    MemoriaPrincipal memoriaPrincipal= new MemoriaPrincipal();
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
            /* direccionItem.transform.GetChild(2).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(3).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(4).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(5).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(6).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(7).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(8).GetComponent<Text>().text = $"{i:X2}";
            direccionItem.transform.GetChild(9).GetComponent<Text>().text = $"{i:X2}"; */
            Tuple<int, int, int, int> datos = new Tuple<int, int, int, int>(i, i, i, i);
            direccionMemoriaCache.Add(new Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> (i, i, direccionItem, datos, datos));
        }
        Destroy(direccionTemplate);
        bloque=bloqueTextField.transform.GetChild(1).GetComponent<Text>();
        linea=lineaTextField.transform.GetChild(1).GetComponent<Text>();

    }
    
    public void Leer(){
        string lineaString=this.linea.text;
        string bloqueString=this.bloque.text;
        int valueLinea = int.Parse(lineaString, System.Globalization.NumberStyles.HexNumber);
        int bloqueConv=0x0;
        bool valueBloque =Int32.TryParse(bloqueString,out bloqueConv);
        Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>lineaBuscada= this.direccionMemoriaCache[valueLinea];
        if(valueBloque & bloqueConv==lineaBuscada.Item1)
        {
            Debug.Log("Acierto");
            Debug.Log(lineaBuscada);
        }
        else{
            Debug.Log("Fallo");
            Debug.Log(memoriaPrincipal.BusquedaMemoriaPrincipal(bloqueConv));

        }
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
