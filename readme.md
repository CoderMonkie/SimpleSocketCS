# 简介

这里简单封装了一下 .NET 下的 Socket，  

不能算是库吧，做简单的通信 demo 够用。

# Development

- 2019-08-01  
  Initial-Commit  
  之前没有做过这方面的，现在需要做一个简单的通信demo，  
  做了一个nodejs的B/S服务端demo，然后做的这个C/S用的。  
  Demo的功能非常简单，只做服务端推送数据的展示，这个小示例就够用了。  
  但是还是希望在之后有时间了，仔细研究下改进它。

# 代码结构

只有简单的四个文件

```

__CM.SimpleSocketCS
  |__BaseSocket.cs
  |__ClientSocket.cs
  |__ServerSocket.cs
  |__Utils.cs

```

# 示例

- Client 端（exe）

  ```csharp
    ClientSocket clientSocket = null;

    // test
    string _host = "192.168.1.254";
    int _port = 8080;

    private void ConnectBtn_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Create ClientSocket and Connect to Server.
            clientSocket = new ClientSocket(_host, _port);

            clientSocket.onReceiveData += ReceiveHandler;

            // Show Message of Connection.
            System.Windows.MessageBox.Show("Connected!");
        }
        catch(Exception ex)
        {
            Console.Write(ex);
            System.Windows.MessageBox.Show("Failed to connect!\r\n" + ex.ToString());
        }
    }

    private void ReceiveHandler(byte[] bytes)
    {
        var strReceived = Encoding.UTF8.GetString(bytes)

        // TODO： Update UI
        // Such as below in WPF
        //this.Dispatcher.Invoke(new Action(() =>
        //{
        //    this.TextBlockCtrl.Text = strReceived;
        //}));
    }
  ```

- Server 端（exe）

  ```csharp
    private ServerSocket serverSocket = null;
  
    // test
    string _host = "192.168.1.254";
    int _port = 8080;

    private void StartServerBtn_Click(object sender, RoutedEventArgs e)
    {
        serverSocket = new ServerSocket(_host, _port);
        serverSocket.onReceiveData += ReceiveHandler;
    }

    private void ReceiveHandler(byte[] bytes)
    {
        serverSocket.Broadcast(bytes);
    }

    private void SendDataBtn_Click(object sender, RoutedEventArgs e)
    {
        // prepare data to send
        var strDataToSend = "test";

        serverSocket.Broadcast(Encoding.UTF8.GetBytes(strDataToSend));
    }
  ```

---

※ It`s too simple...May be improved in future.