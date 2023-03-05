﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePortugueseMan.CodingTracker;

public class GoalOperations
{
    DbCommands dbCmd = new();
    ListOperations listOp = new();
    
    public void UpdateGoals()
    {
        List<Goal> allGoals = dbCmd.ReturnAllGoalsInTable();
        if (allGoals is null) return;
        List<CodingSession> allSessions = dbCmd.ReturnAllLogsInTable();

        foreach (Goal goal in allGoals) 
        {
            Goal updatedGoal = new();
            List<CodingSession> sessionsInGoalRange = listOp.GetLogsBetweenDates(allSessions, goal.StartDate, goal.EndDate);
            updatedGoal.HoursSpent = listOp.TotalTimeInList(sessionsInGoalRange);

            if (goal.TargetHours.Subtract(updatedGoal.HoursSpent) <= TimeSpan.Zero) updatedGoal.Status = "Complete";
            else if (goal.Status != "Active") updatedGoal.Status = "Incomplete";
            else updatedGoal.Status = goal.Status;

            updatedGoal.StartDate = goal.StartDate;
            updatedGoal.EndDate = goal.EndDate;
            updatedGoal.TargetHours = goal.TargetHours;

            dbCmd.Update(goal.Id, updatedGoal);
        }
    }

}
