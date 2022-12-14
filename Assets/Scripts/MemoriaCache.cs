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
    public int tamanoBloque;
    [SerializeField] private GameObject botonVer;
    [SerializeField] private GameObject bloqueTextField;
    [SerializeField] private GameObject lineaTextField;
    private Text bloque, linea;
    [SerializeField] private GameObject memoriaPrincipal;
    MemoriaPrincipal memoriaPrincipalControler;
    private void Awake()
    {
        memoriaPrincipalControler = memoriaPrincipal.GetComponent<MemoriaPrincipal>();
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
        int valueLinea = 0x0;
        int bloqueConv = 0x0;
        Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> lineaBuscada;
        Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> bloqueRecuperado;
        GameObject direccionItemActualizado;
        try
        {
            valueLinea = int.Parse(lineaString, System.Globalization.NumberStyles.HexNumber);
            bloqueConv = int.Parse(bloqueString,System.Globalization.NumberStyles.HexNumber);
        //Debug.Log(valueLinea);
            //Debug.Log($"{Convert.ToInt32(memoriaPrincipalControler.maximoDireccionableMP / tamanoBloque):X}");
            if(valueLinea>= maximoDireccionableMC)
            {
                throw new Exception($"L??nea {valueLinea:X} debe ser menor a {maximoDireccionableMC:X}");
            }
            if (bloqueConv >= 0x80)
            {
                //el m??ximo de bloques es 32 decimal= 20 hexadecimal
                throw new Exception($"Bloque {bloqueConv:X} debe ser menor a {0x80:X}");
            }
            lineaBuscada= this.direccionMemoriaCache[valueLinea];
            if( bloqueConv==lineaBuscada.Item1)
            {
                Debug.Log("Acierto Memoria Cach??");
                Debug.Log(lineaBuscada);// es null
            }
            else // comprobando que el bloque si es v??lido
            {
                Debug.Log("Fallo Memoria Cach??");

                // recuperando l??nea cache de la memoria principal
                bloqueRecuperado=this.BusquedaMemoriaPrincipalFake(bloqueConv);
                // actualizando memoria cache
                direccionMemoriaCache[bloqueRecuperado.Item2] = bloqueRecuperado;
                // actualizando item en view
                direccionItemActualizado = bloqueRecuperado.Item3;
                direccionItemActualizado.transform.GetChild(0).GetComponent<Text>().text = $"{bloqueRecuperado.Item1:X1}";// asignando etiqueta
                direccionItemActualizado.transform.GetChild(2).GetComponent<Text>().text = $"{bloqueRecuperado.Item4.Item1:X2}";// asignando dato 1
                direccionItemActualizado.transform.GetChild(3).GetComponent<Text>().text = $"{bloqueRecuperado.Item4.Item2:X2}";
                direccionItemActualizado.transform.GetChild(4).GetComponent<Text>().text = $"{bloqueRecuperado.Item4.Item3:X2}";
                direccionItemActualizado.transform.GetChild(5).GetComponent<Text>().text = $"{bloqueRecuperado.Item4.Item4:X2}";
                direccionItemActualizado.transform.GetChild(6).GetComponent<Text>().text = $"{bloqueRecuperado.Item5.Item1:X2}";
                direccionItemActualizado.transform.GetChild(7).GetComponent<Text>().text = $"{bloqueRecuperado.Item5.Item2:X2}";
                direccionItemActualizado.transform.GetChild(8).GetComponent<Text>().text = $"{bloqueRecuperado.Item5.Item3:X2}";
                direccionItemActualizado.transform.GetChild(9).GetComponent<Text>().text = $"{bloqueRecuperado.Item5.Item4:X2}";


            }
        }
        catch (Exception e)
        {
            Debug.Log($"Formato de Bloque o L??nea Inv??lido, debe ser hexadecimal.\n{e.Message:s}");
        }
    }

    public Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> BusquedaMemoriaPrincipal(){
        direccionTemplate=this.transform.GetChild(0).gameObject;
        Tuple<int, int, int, int> datos = new Tuple<int, int, int, int>(1, 2, 3, 4);
        return new Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> (1, 2, direccionTemplate, datos, datos);
    }
    public Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> BusquedaMemoriaPrincipalFake(int bloque)
    {
        //asumiendo fallo
        int lineaCache=this.TLB(bloque);// calculando en que l??nea va el bloque no encontrado
        GameObject direccionItem = this.transform.GetChild(lineaCache).gameObject;// l??nea de la cache donde se remplazara el bloque
        Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>> datos =memoriaPrincipalControler.Leer(bloque);
        return new Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>(bloque,lineaCache, direccionItem, datos.Item1, datos.Item2);
    }

    /**
     * Este m??todo tiene por proposito calcular a que l??nea correponde el bloque.
    */
    public int TLB(int bloque)
    {
        // Dado que hay 8 palabras por bloque y 8 l??neas en la cache
        return bloque % maximoDireccionableMC;
    }

}
