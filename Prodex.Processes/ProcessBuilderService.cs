using Prodex.Shared.Utils;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Prodex.Processes
{
    public class ProcessBuilderService
    {

        public List<KeyValueResult> GetActions(string xml, long currentStep)
        {
            var steps = BuildProcess(xml);

            var current = steps.Single(p => p.StepId == currentStep);

            return steps
                .Where(p => current.NextSteps.Contains(p.StepId))
                .Select(p => new KeyValueResult(p.StepId, p.Name))
                .ToList();
        }

        internal List<ProcessStep> BuildProcess(string xml)
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

            return steps;
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
                    nextSteps.Add(existing.StepId);
                    continue;
                }

                steps.Add(new ProcessStep
                {
                    Id = newStep.Id,
                    Name = newStep.Name,
                    StepId = steps.Count + 1,
                    StepType = MapStepType(newStep)
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
            _ => throw new InvalidEnumArgumentException()
        };
    }
}
