using Application.Service.SeedWork;
using Common.Model;
using CSharpFunctionalExtensions;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Service.Commands
{
    class SaveTaskCommandHandler : ICommandHandler<SaveTaskCommand>
    {
        private readonly IGoalRepository _repository;

        public SaveTaskCommandHandler(IGoalRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Método para registrar las tareas en la base de datos
        /// </summary>
        /// <param name="data">Datos de la tarea</param>
        /// <returns>Resultado de la operación de guardado</returns>
        public Result Handle(SaveTaskCommand data)
        {
            GTask _currentTask = _repository.GetTaskByName(data.TaskData.Id, !string.IsNullOrEmpty(data.TaskData.Name) ? data.TaskData.Name.Trim() : "");
            Goal _goal = new Goal();

            if (_currentTask == null && data.TaskData.Operation == (int)OperationsList.New)
            {
                //Obtener la meta asociada a la tarea
                _goal = _repository.GetGoalById(data.TaskData.GoalId);
                //Registrar la nueva tarea
                _currentTask = new GTask()
                {
                    Name = !string.IsNullOrEmpty(data.TaskData.Name) ? data.TaskData.Name.Trim() : "",
                    RegisterDate = DateTime.Now,
                    IsImportant = false,
                    Goal = _goal,
                    GState = _repository.GetStatus((int)TaskState.Open)
                };
                
                _repository.InsertTask(_currentTask);
            }

            else if (_currentTask != null && data.TaskData.Operation == (int)OperationsList.Update)
            {
                //Actualizar el nombre de la tarea
                _currentTask.Name = !string.IsNullOrEmpty(data.TaskData.Name) ? data.TaskData.Name.Trim() : "";
                _currentTask.IsImportant = data.TaskData.IsImportant;
                _currentTask.GState = _repository.GetStatus(data.TaskData.StateId);

                //Aceptamos los cambios
                _repository.Save();

                _goal.GoalId = data.TaskData.GoalId;
            }


            else if (_currentTask != null && data.TaskData.Operation == (int)OperationsList.Delete)
            {
                //Eliminar la tarea
                _repository.RemoveTask(_currentTask);

                //Aceptamos los cambios
                _repository.Save();

                _goal.GoalId = data.TaskData.GoalId;
            }

            else if (_currentTask != null && data.TaskData.Operation == (int)OperationsList.New)
                return Result.Failure($"¡El nombre de la tarea {data.TaskData.Name} ya está reistrado, por favor intente con otro!");

            //Obtener la meta asociada a la tarea
            _goal = _repository.GetGoalById(_goal.GoalId);

            //Actualizar el porcentajes
            List<GTask> _tasks = _repository.GetTasksByGoalId(_goal.GoalId);

            _goal.TotalTasks = _tasks.Count();

            int _totalComplete = _tasks.Where(f => f.stateid == (int)TaskState.Closed).Count();
            if (_totalComplete > 0)
                _goal.Percentege = (_totalComplete * 100) / _tasks.Count();


            //Aceptamos los cambios
            _repository.Save();
            //Retornamos un estatus correcto
            return Result.Success("La tarea se guardó correctamente");
        }
    }
}
