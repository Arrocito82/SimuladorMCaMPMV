using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DireccionMemoriaVirtual : MonoBehaviour
{
    private int pagina;
    private int desplazamiento;
    public DireccionMemoriaVirtual(int pagina, int desplazamiento)
    {
        this.pagina=pagina;
        this.desplazamiento = desplazamiento;

    }
    
    public int getPagina()
    {
        return this.pagina;
    }
    public int getDesplazamiento()
    {
        return this.desplazamiento;
    }
    public void setPagina(int pagina)
    {
        this.pagina = pagina;
    }
    public void setDesplazamiento(int desplazamiento)
    {
        this.desplazamiento = desplazamiento;
    }

}


