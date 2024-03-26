using Application.Service.SeedWork;
using Common.Model;
using System.Collections.Generic;

namespace Application.Service.Queries
{
    public sealed class GoalList : IQuery<List<GoalVM>>
    {
        public GoalVM GoalData { get; set; }
        public GoalList(GoalVM goaldata)
        {
            GoalData = goaldata;
        }
    }
}