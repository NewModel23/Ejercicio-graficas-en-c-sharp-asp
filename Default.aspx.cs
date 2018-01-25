using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace AppGraficos1
{
    public partial class _Default : Page
    {
        string[] productos = new string[4];
        int[] cantidad = new int[4];
        protected void Page_Load(object sender, EventArgs e)
        {
            //Cadena de conexión, también puedes consultar en la página https://www.connectionstrings.com
            // La que corresponda al servidor de datos que uses
            String constr = @"Server=DESKTOP-KRKQVBR;Database=PRUEBAS;Trusted_Connection=True;";

            //Creamos una nueva conexión y lo declaramos como  "con"
            SqlConnection con = new SqlConnection(constr);

            //Abrimos la conexión
            con.Open();

            //Creamos la variable cmd para ejecutar la consulta a nustra tabla
            SqlCommand cmd = new SqlCommand("Select Producto, Cantidad from Catalogo Order by Cantidad Asc", con);

            //Almacenamos el resultado en un SQL Data reader que definimos como "Lector"
            SqlDataReader lector = cmd.ExecuteReader();

            //Creamos un contador para que ayamos almacenando dato por dato conforme corre el lector
            int contador = 0;

            //Mientras existan registros en el lector, agregar a la coleccion productos y cantidades los registros encontrados
            while (lector.Read())
            {
                productos[contador] = lector.GetString(0);
                cantidad[contador] = lector.GetInt32(1);
                contador++;
            }

            //Cerramos el lector
            lector.Close();

            //Agregamos las colecciones de datos a nuestro gráfico
            Grafico.Series["Series1"].Points.DataBindXY(productos, cantidad);

            //Cerramos la conexión y liso!
            con.Close();



        }
    }
}