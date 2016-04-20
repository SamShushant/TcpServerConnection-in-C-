using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

    
    public string valorQuant = "";
    public int puerto = 8080;
    public void ConexionTcp()
        {
            try
            {
                Thread tcpserverRunThread = new Thread(new ThreadStart(ProcesoAcceptarPeticionDelCliente));
                tcpserverRunThread.Start();
            }
            catch (Exception)
            {

            }
        }
        public void ProcesoAcceptarPeticionDelCliente()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, puerto);
                tcpListener.Start();
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Thread tcpHandlerThread = new Thread(new ParameterizedThreadStart(ProcesoRecibirMessage));
                    tcpHandlerThread.Start(client);
                }
            }
            catch (Exception)
            {
            }
        }

        public void ProcesoRecibirMessage(object client)
        {
            try
            {
                TcpClient mclient = (TcpClient)client;
                NetworkStream stream = mclient.GetStream();
                Byte[] message = new byte[1024];
                stream.Read(message, 0, message.Length);
                Message(Encoding.ASCII.GetString(message));
                MessageBox.Show("Mensaje Recibido");
                stream.Close();
                mclient.Close();
                tcpListener.Stop();
            }
            catch (Exception)
            {
            }
        }
        public string Message(string s)
        {
            try
            {
                Func<int> del = delegate ()
                {
                    valorQuant = s + System.Environment.NewLine;
                    Messagebox.show(valorQuant);
                    return 0;

                };
                Invoke(del);
            }
            catch (Exception)
            {
            }
            return valorQuant;
        }

