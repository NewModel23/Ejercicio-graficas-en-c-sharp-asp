using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace AppGraficos
{
    public partial class _Default : Page
    {
        string[] nombres = new string[4];
        int[] cantidad = new int[4];
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //Contador
            int contador = 0;

            //Iniciar la conexión a la base SQL Server
            String constr = @"Server=DESKTOP-KRKQVBR;Database=PRUEBAS;Trusted_Connection=True;";

            SqlConnection con = new SqlConnection(constr);

            con.Open();
            SqlCommand cmd = new SqlCommand("Select Producto, Cantidad from Catalogo order by Cantidad asc", con);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                nombres[contador] = reader.GetString(0);
                cantidad[contador] = reader.GetInt32(1);
                contador++;
            }

            //Cerrar el Reader
            reader.Close();


            Grafica.Series["Series"].Points.DataBindXY(nombres, cantidad);

            con.Close();


        }
    }
}