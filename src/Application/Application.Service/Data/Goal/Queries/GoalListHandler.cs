using Application.Service.SeedWork;
using Application.Service.Tools;
using Common.Model;
using Common.Model.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Application.Service.Queries
{
    class GoalListHandler : IQueryHandler<GoalList, List<GoalVM>>
    {
        private readonly QueriesConnectionString _queryConnectionString;

        public GoalListHandler(QueriesConnectionString queryConnectionString)
        {
            _queryConnectionString = queryConnectionString;
        }

        public List<GoalVM> Handle(GoalList _data)
        {
            var result = new List<GoalVM>();
            var conn = new SqlConnection(_queryConnectionString.Value);
            conn.Open();
            var cmd = conn.CreateCommand();
            if(_data.GoalData.Id > 0)
            {
                cmd.CommandText = "select * from vw_goal where Id=@goa";
                cmd.Parameters.AddWithValue("@goa", _data.GoalData.Id);
            }
            else if (_data.GoalData.Id == 0)
                cmd.CommandText = "select * from vw_goal";

            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                result.Add(new GoalVM()
                {
                    Id = ReadData.RdInt(dataReader["Id"]),
                    Name = ReadData.RdString(dataReader["GoalName"]),
                    RegisterDate = ReadData.RdDate(dataReader["Register"]),
                    Tasks = ReadData.RdInt(dataReader["Tasks"]),
                    Progress = ReadData.RdDouble(dataReader["Progress"]),
                    CompleteTasks = ReadData.RdInt(dataReader["Complete"])
                });

            }
            cmd.Parameters.Clear();
            dataReader.Close();

            cmd.Dispose();
            if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            conn.Dispose();

            return result;
        }
    }
}