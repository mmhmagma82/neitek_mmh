using Application.Service.SeedWork;
using Common.Model;
using CSharpFunctionalExtensions;
using Domain.Model;
using System;

namespace Application.Service.Commands
{
    class SaveGoalCommandHandler : ICommandHandler<SaveGoalCommand>
    {
        private readonly IGoalRepository _repository;

        public SaveGoalCommandHandler(IGoalRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Método para registrar las metas en la base de datos
        /// </summary>
        /// <param name="data">Datos de la meta</param>
        /// <returns>Resultado de la operación de guardado</returns>
        public Result Handle(SaveGoalCommand data)
        {
            Goal _currentGoal = _repository.GetGoalByName(data.GoalData.Id, !string.IsNullOrEmpty(data.GoalData.Name) ? data.GoalData.Name.Trim() : "");

            if (_currentGoal == null && data.GoalData.Operation == (int)OperationsList.New)
            {
                //Registrar la nueva meta
                _currentGoal = new Goal()
                {
                    Name = !string.IsNullOrEmpty(data.GoalData.Name) ? data.GoalData.Name.Trim() : "",
                    RegisterDate = DateTime.Now,
                    TotalTasks = 0,
                    Percentege = 0
                };
                _repository.Add(_currentGoal);
            }
            
            else if (_currentGoal != null && data.GoalData.Operation == (int)OperationsList.Update)
            {
                //Actualizar el nombre de la meta
                _currentGoal.Name = !string.IsNullOrEmpty(data.GoalData.Name) ? data.GoalData.Name.Trim() : "";
            }
            
            else if (_currentGoal != null && data.GoalData.Operation == (int)OperationsList.Delete)
            {
                //Eliminar la meta y todas las tareas asociadas a ella
                _repository.RemoveTaskRange(_currentGoal.GoalId);
                _repository.Remove(_currentGoal);
            }

            else if (_currentGoal != null && data.GoalData.Operation == (int)OperationsList.New)
                return Result.Failure($"¡El nombre de la meta {data.GoalData.Name} ya está reistrado, por favor intente con otro!");

            //Aceptamos los cambios
            _repository.Save();
            //Retornamos un estatus correcto
            return Result.Success("La meta se guardó correctamente");
        }
    }
}