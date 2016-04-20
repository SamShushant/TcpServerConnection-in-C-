using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
namespace Prueba_de_Frenos
{
    class tcpCliente
    {
        public string ip;
        public int port;
        public string cadena;
      
      
        public string[] DadesGasos = new string[3];
        private TcpClient client = new TcpClient();
        public tcpCliente(string ip,int port)

        {
            this.port = port;
            this.ip = ip;
        }
        public void Conexion()
        {
            try
            {
                //Controla el subproceso
                Thread etheard = new Thread(new ThreadStart(ProcesoConexion_Como_Cliente));
                etheard.Start();
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
            
        }
        public void ProcesoConexion_Como_Cliente()
        {


            try
            {
                //Proporciona conexiones cliente para el servicio tcp/ip
                client.Connect(IPAddress.Parse(this.ip), this.port);
                //NetworkStream permite acceso en la red
                NetworkStream stream = client.GetStream();
                //Encoding.ASCII.GetBytes: tranforma el valor string en codigo ASCII
                //byte: representa un entero de 8 bits
                ProcesoEnviarDatosServer(stream);
                //Finalizar la conexión de acceso de red y la conexión cliente para servicio tcp           
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
      
        public void ProcesoEnviarDatosServer(NetworkStream st)
        {
            for (int i = 0; i < DadesGasos.Length; i++)
            {
                byte[] message = Encoding.ASCII.GetBytes(getArray(i));
                st.Write(message, 0, message.Length);
                Console.WriteLine("Message Send it");
            }
            
        }
        
        
        public string setCadena(string cad)
        {
            this.cadena = "";
            this.cadena = cad;
            return this.cadena;
        }
        public string getCadena(){
            return this.cadena;
        }
        public string[] setArray(string cadena, int pos)
        {
            this.DadesGasos[pos] = cadena+":";
            return this.DadesGasos;
        }
        public string getArray(int pos)
        {
            return this.DadesGasos[pos];
        }
      
       
    }
}



    
