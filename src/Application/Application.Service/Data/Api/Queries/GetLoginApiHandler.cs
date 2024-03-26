using Application.Service.SeedWork;
using Application.Service.Tools;
using Common.Model;
using Common.Model.Models;
using Microsoft.Data.SqlClient;
using System;

namespace Application.Service.Queries
{
    class GetLoginApiHandler : IQueryHandler<GetLoginApi, ApiLoginVM>
    {
        private readonly QueriesConnectionString _queryConnectionString;

        public GetLoginApiHandler(QueriesConnectionString queryConnectionString)
        {
            _queryConnectionString = queryConnectionString;
        }

        public ApiLoginVM Handle(GetLoginApi _data)
        {
            var result = new ApiLoginVM() { Role = string.Empty };
            using (var conn = new SqlConnection(_queryConnectionString.Value))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    string gkl = Crypto.Encrypt(_data.DatosSesion.UserName);
                    string gkj = Crypto.Encrypt(_data.DatosSesion.Password);

                    cmd.CommandText = "select * from vw_api_user where username=@usr and password=@pas";
                    cmd.Parameters.AddWithValue("@usr", Crypto.Encrypt(_data.DatosSesion.UserName));
                    cmd.Parameters.AddWithValue("@pas", Crypto.Encrypt(_data.DatosSesion.Password));
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (ReadData.RdDate(dataReader["expires"]) > DateTime.Now)
                            result.Role = ReadData.RdString(dataReader["role"]);
                    }
                    cmd.Parameters.Clear();
                    dataReader.Close();

                    cmd.Dispose();       
                }
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
                conn.Dispose();
            }
            return result;
        }
    }
}