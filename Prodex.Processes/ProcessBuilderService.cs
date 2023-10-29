using MediatR;
using Prodex.Shared.Utils;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Prodex.Processes
{
    public class ProcessBuilderService
    {
        public BuildedProcess BuildProcess(string xml)
        {
            Definitions def;

            var serializer = new XmlSerializer(typeof(Definitions));

            using (var reader = new StringReader(xml))
            {
                def = (Definitions)serializer.Deserialize(reader);
            }

            var steps = new List<ProcessStep>();
            var start = def.Process.StartEvent;

            steps.Add(new ProcessStep
            {
                Id = start.Id,
                Name = start.Name,
                StepId = 1,
                StepType = StepType.Start,
            });

            steps.Last().NextSteps = CalculateNextSteps(def.Process, steps, start);

            return new BuildedProcess(steps);
        }

        private List<long> CalculateNextSteps(Process process, List<ProcessStep> steps, BaseElement currentElement)
        {
            var currentStep = steps.Last();

            var nextSteps = new List<long>();

            foreach (var outgoing in currentElement.Outgoing)
            {
                var newStep = process.All.Where(p => p.Incoming.Contains(outgoing)).Single();

                var existing = steps.FirstOrDefault(p => p.Id == newStep.Id);

                if (existing != null)
                {
                    existing.PrevSteps.Add(currentElement.Id, outgoing);
                    nextSteps.Add(existing.StepId);
                    continue;
                }

                steps.Add(new ProcessStep
                {
                    Id = newStep.Id,
                    Name = newStep.Name,
                    StepId = steps.Count + 1,
                    StepType = MapStepType(newStep),
                    PrevSteps = new Dictionary<string, string> { { currentElement.Id, outgoing } }
                });

                nextSteps.Add(steps.Count);

                steps.Last().NextSteps = CalculateNextSteps(process, steps, newStep);
            }

            return nextSteps;
        }

        private static StepType MapStepType(BaseElement newStep) => newStep switch
        {
            ExclusiveGateway => StepType.ExclusiveGateway,
            UserTask => StepType.UserTask,
            ServiceTask => StepType.ServiceTask,
            EndEvent => StepType.End,
            StartEvent => StepType.Start,
            SendTask => StepType.SendTask,
            _ => throw new InvalidEnumArgumentException()
        };
    }
}
