using System.Net.Sockets;
using System.Text;

public class Client
{
    public static void Main()
{
    TcpClient tcpClient = new TcpClient("127.0.0.1",8000);
    NetworkStream netStream = tcpClient.GetStream();

    Console.Write("Enter your query :");
    string msg = Console.ReadLine();
    //byte[] buffer = Encoding.UTF8.GetBytes(msg);
    //encrypt
    byte[] buffer = CryptoHelper.Encrypt(msg);
    netStream.Write(buffer,0,buffer.Length);

    //Receive response
    buffer = new byte[1024];
    int bytesRead;
    while ((bytesRead = netStream.Read(buffer,0,buffer.Length)) > 0)
    {
        //string received = Encoding.UTF8.GetString(buffer,0,buffer.Length).Trim();
        //decrypt
        string received = CryptoHelper.Decrypt(buffer.Take(bytesRead).ToArray());
        Console.WriteLine(received);
        if (received == "EMPTY")
            break;
    }
    tcpClient.Close();
}
}
