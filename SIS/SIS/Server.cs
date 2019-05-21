﻿using SIS.HTTP.Common;
using SIS.WebServer.Routing.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public class Server
    {

        private const string LocalHost = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener tcpListener;

        private IServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public Server(int port, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));
            this.port = port;
            this.serverRoutingTable = serverRoutingTable;

            this.tcpListener = new TcpListener(IPAddress.Parse(LocalHost), port);

        }
        private async Task Listen(Socket client)
        {
            var connectionHandler = new ConnectionHandler(client, this.serverRoutingTable);
            await connectionHandler.ProcessRequestAsync();

        }
        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;
            Console.WriteLine($"Server started on http://{LocalHost}:{this.port}");

            while (this.isRunning)
            {
                Console.WriteLine("Waiting for client...");
                var client = this.tcpListener.AcceptSocket();

                Task.Run(() => this.Listen(client));
            }
        }

    }
}
