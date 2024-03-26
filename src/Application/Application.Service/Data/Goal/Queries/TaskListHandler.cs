using Application.Service.SeedWork;
using Application.Service.Tools;
using Common.Model;
using Common.Model.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;

namespace Application.Service.Queries
{
    class TaskListHandler : IQueryHandler<TaskList, List<TaskVM>>
    {
        private readonly QueriesConnectionString _queryConnectionString;

        public TaskListHandler(QueriesConnectionString queryConnectionString)
        {
            _queryConnectionString = queryConnectionString;
        }

        public List<TaskVM> Handle(TaskList _data)
        {
            var result = new List<TaskVM>();
            var conn = new SqlConnection(_queryConnectionString.Value);
            conn.Open();
            var cmd = conn.CreateCommand();
            if (_data.TaskData.Id != 0)
            {
                cmd.CommandText = "select * from vw_task where Id=@tas";
                cmd.Parameters.AddWithValue("@tas", _data.TaskData.Id);
            }
            else 
            {
                cmd.CommandText = "select * from vw_task where GoalId=@goa";
                cmd.Parameters.AddWithValue("@goa", _data.TaskData.GoalId);
            }

            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                result.Add(new TaskVM()
                {
                    Id = ReadData.RdInt(dataReader["Id"]),
                    Name = ReadData.RdString(dataReader["TaskName"]),
                    RegisterDate = ReadData.RdDate(dataReader["Register"]),
                    IsImportant = ReadData.RdBool(dataReader["Important"]),
                    GoalId = ReadData.RdInt(dataReader["GoalId"]),
                    GoalName = ReadData.RdString(dataReader["GoalName"]),
                    StateId = ReadData.RdInt(dataReader["StateId"]),
                    StateName = ReadData.RdString(dataReader["StateName"])
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