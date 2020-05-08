using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using BaltaStore.Shared;

namespace BaltaStore.Infra.DataContext
{
    public class StoreDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public StoreDataContext()
        {
            Connection = new SqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
