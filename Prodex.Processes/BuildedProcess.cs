﻿using Blazorise;
using MediatR;
using Microsoft.Extensions.Options;
using Prodex.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodex.Processes;

public interface IProcessable
{
    public long Id { get; set; }
    public long CurrentStepId { get; set; }
}

public class BuildedProcess
{
    private List<ProcessStep> processSteps;

    public BuildedProcess(List<ProcessStep> processSteps)
    {
        this.processSteps = processSteps;
    }


    public async Task Run(IMediator mediator, IProcessable processable, long userId)
    {
        var start = processSteps.Single(p => p.StepType == StepType.Start);
        await Run(mediator, processable, start, userId);
    }

    private async Task Run(IMediator mediator, IProcessable processable, ProcessStep currentStep, long userId)
    {
        var nextSteps = GetNextSteps(currentStep.StepId);

        if(nextSteps.Any(p => p.StepType == StepType.UserTask))
        {
            if(!nextSteps.All(p => p.StepType == StepType.UserTask))
                throw new NotImplementedException();

            processable.CurrentStepId = currentStep.StepId;

            return;
        }

        if(nextSteps.Count > 1)
        {
            throw new NotImplementedException();
        }

        var step = nextSteps.Single();

        switch (step.StepType)
        {
            case StepType.Start:
                throw new InvalidOperationException("Wrong process definition: start should never be in next steps");

            case StepType.UserTask:
                throw new UnreachableException(); // userTask is handled before

            case StepType.ServiceTask:
                await mediator.Send(new ExecuteServiceTaskRequest(processable, step, userId));
                break;

            case StepType.SendTask:

                // ExecuteSendTask(processable, step);
                break;

            case StepType.ExclusiveGateway:

                // ExecuteExclusiveGateway(processable, step);
                break;

            case StepType.End:
                processable.CurrentStepId = step.StepId;
                // processable.IsFinished = true; ???
                return; 

            default: 
                throw new NotImplementedException();
        }

        await mediator.Send(new AfterStepExecutedRequest(processable, step, userId));
        
        processable.CurrentStepId = step.StepId;
        await Run(mediator, processable, step, userId);
    }

    public List<KeyValueResult> GetActions(IProcessable processable)
    {
        var current = processSteps.Single(p => p.StepId == processable.CurrentStepId);

        return processSteps
            .Where(p => current.NextSteps.Contains(p.StepId))
            .Select(p => new KeyValueResult(p.StepId, p.Name))
            .ToList();
    }

    public async Task ExecuteUserTask(IMediator mediator, IProcessable processable, long stepId, long userId)
    {
        var availableActions = GetNextSteps(processable.CurrentStepId);

        var action = availableActions.FirstOrDefault(p => p.StepId == stepId);

        if (action is null || action.StepType != StepType.UserTask)
            throw new InvalidOperationException("Akcja nie istnieje lub nie jest akcją użytkownika");

        await mediator.Send(new AfterStepExecutedRequest(processable, action, userId));

        processable.CurrentStepId = action.StepId;

        await Run(mediator, processable, action, userId);
    }

    private List<ProcessStep> GetNextSteps(long currentStepId)
    {
        var current = processSteps.Single(p => p.StepId == currentStepId);

        return processSteps
            .Where(p => current.NextSteps.Contains(p.StepId))
            .ToList();
    }

}
