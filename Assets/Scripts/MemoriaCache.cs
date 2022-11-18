using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

public class MemoriaCache : MonoBehaviour
{
    private List<Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>> direccionMemoriaCache;
    private GameObject direccionTemplate, newDireccion;

    [SerializeField] private int maximoDireccionableMC;
    public int tamanoBloque;
    [SerializeField] private GameObject botonVer;
    [SerializeField] private GameObject bloqueTextField;
    [SerializeField] private GameObject lineaTextField;
    [SerializeField] private GameObject dato0, dato1, dato2,dato3, dato4, dato5, dato6, dato7;
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
        try
        {
            valueLinea = int.Parse(lineaString, System.Globalization.NumberStyles.HexNumber);
            bloqueConv = int.Parse(bloqueString,System.Globalization.NumberStyles.HexNumber);
            //Debug.Log($"{Convert.ToInt32(memoriaPrincipalControler.maximoDireccionableMP / tamanoBloque):X}");
            if(valueLinea>= maximoDireccionableMC)
            {
                throw new Exception($"Línea {valueLinea:X} debe ser menor a {maximoDireccionableMC:X}");
            }
            if (bloqueConv >= memoriaPrincipalControler.maximoDireccionableMP / tamanoBloque)
            {
                throw new Exception($"Bloque {bloqueConv:X} debe ser menor a {memoriaPrincipalControler.maximoDireccionableMP / tamanoBloque:X}");
            }
            Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>lineaBuscada= this.direccionMemoriaCache[valueLinea];
            if( bloqueConv==lineaBuscada.Item1)
            {
                Debug.Log("Acierto");
                Debug.Log(lineaBuscada);
            }
            else // comprobando que el bloque si es válido
            {
                Debug.Log("Fallo");

                // recuperando línea cache de la memoria principal
                Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> bloqueRecuperado=this.BusquedaMemoriaPrincipalFake(bloqueConv);
                // actualizando memoria cache
                direccionMemoriaCache[bloqueRecuperado.Item2] = bloqueRecuperado;
                // actualizando item en view
                GameObject direccionItemNuevo = bloqueRecuperado.Item3;
                direccionItemNuevo.transform.GetChild(0).GetComponent<Text>().text = $"{bloqueRecuperado.Item1:X1}";// asignando etiqueta
                dato0.GetComponentInChildren<TMP_InputField>().text= $"{bloqueRecuperado.Item4.Item1:X2}";// asignando dato 1
                dato1.GetComponentInChildren<TMP_InputField>().text = $"{bloqueRecuperado.Item4.Item2:X2}";
                dato2.GetComponentInChildren<TMP_InputField>().text = $"{bloqueRecuperado.Item4.Item3:X2}";
                dato3.GetComponentInChildren<TMP_InputField>().text = $"{bloqueRecuperado.Item4.Item4:X2}";
                dato4.GetComponentInChildren<TMP_InputField>().text = $"{bloqueRecuperado.Item5.Item1:X2}";
                dato5.GetComponentInChildren<TMP_InputField>().text = $"{bloqueRecuperado.Item5.Item2:X2}";
                dato6.GetComponentInChildren<TMP_InputField>().text = $"{bloqueRecuperado.Item5.Item3:X2}";
                dato7.GetComponentInChildren<TMP_InputField>().text = $"{bloqueRecuperado.Item5.Item4:X2}";
            }
        }catch(Exception e)
        {
            Debug.Log($"Formato de Bloque o Línea Inválido, debe ser hexadecimal.\n{e.Message:s}");
        }
    }
    public void Escribir(){
        Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> direccionMemoriaCacheNew;
        newDireccion=this.transform.GetChild(0).gameObject;
        GameObject direccionItemNuevo=Instantiate(newDireccion, this.transform);
        string lineaString=this.linea.text;
        string bloqueString=this.bloque.text;
        int valueLinea = 0x0;
        int bloqueConv = 0x0;
        valueLinea = int.Parse(lineaString, System.Globalization.NumberStyles.HexNumber);
        bloqueConv = int.Parse(bloqueString,System.Globalization.NumberStyles.HexNumber);
        direccionMemoriaCacheNew = direccionMemoriaCache[valueLinea];
        int dato0new = int.Parse(dato0.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        int dato1new = int.Parse(dato1.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        int dato2new = int.Parse(dato2.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        int dato3new = int.Parse(dato3.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        int dato4new = int.Parse(dato4.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        int dato5new = int.Parse(dato5.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        int dato6new = int.Parse(dato6.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        int dato7new = int.Parse(dato7.GetComponentInChildren<TMP_InputField>().text,System.Globalization.NumberStyles.HexNumber);
        Tuple<int, int, int, int> datos1 = new Tuple<int, int, int, int>(dato0new, dato1new, dato2new, dato3new);
        Tuple<int, int, int, int> datos2 = new Tuple<int, int, int, int>(dato4new, dato5new, dato6new, dato7new);
        direccionMemoriaCache[valueLinea]=new Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>(bloqueConv,valueLinea,direccionMemoriaCache[valueLinea].Item3,datos1, datos2);
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(2).GetComponent<Text>().text =$"{dato0new:X}";
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(3).GetComponent<Text>().text =$"{dato1new:X}";
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(4).GetComponent<Text>().text =$"{dato2new:X}";
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(5).GetComponent<Text>().text =$"{dato3new:X}";
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(6).GetComponent<Text>().text =$"{dato4new:X}";
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(7).GetComponent<Text>().text =$"{dato5new:X}";
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(8).GetComponent<Text>().text =$"{dato6new:X}";
        direccionMemoriaCache[valueLinea].Item3.transform.GetChild(9).GetComponent<Text>().text =$"{dato7new:X}";
    }

    public Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> BusquedaMemoriaPrincipal(){
        direccionTemplate=this.transform.GetChild(0).gameObject;
        Tuple<int, int, int, int> datos = new Tuple<int, int, int, int>(1, 2, 3, 4);
        return new Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> (1, 2, direccionTemplate, datos, datos);
    }
    public Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>> BusquedaMemoriaPrincipalFake(int bloque)
    {
        //asumiendo fallo
        int lineaCache=this.TLB(bloque);// calculando en que línea va el bloque no encontrado
        GameObject direccionItem = this.transform.GetChild(lineaCache).gameObject;// línea de la cache donde se remplazara el bloque
        Tuple<Tuple<int, int, int, int>, Tuple<int, int, int, int>> datos =memoriaPrincipalControler.Leer(bloque);
        return new Tuple<int, int, GameObject, Tuple<int, int, int, int>, Tuple<int, int, int, int>>(bloque,lineaCache, direccionItem, datos.Item1, datos.Item2);
    }

    /**
     * Este método tiene por proposito calcular a que línea correponde el bloque.
    */
    public int TLB(int bloque)
    {
        // Dado que hay 8 palabras por bloque y 8 líneas en la cache
        return bloque % maximoDireccionableMC;
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
