using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Prodex.Processes.Tests;

public class Tests
{
    private readonly ITestOutputHelper output;

    public Tests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void Test1()
    {
        var xml = File.ReadAllText("./diagram (4).bpmn");

        var sut = new ProcessBuilderService();

        var result = sut.BuildProcess(xml);

        var expected = new List<ProcessStep>
        {
            new ProcessStep
            {
                Id = "StartEvent_1pwwswl",
                Name = "Start",
                StepId = 1,
                StepType = StepType.Start,
                NextSteps = new List<long> { 2 }
            },
            new ProcessStep
            {
                Id = "Gateway_196u8p7",
                Name = "Bramka",
                StepId = 2,
                StepType = StepType.ExclusiveGateway,
                NextSteps = new List<long> { 3, 9 }
            },
            new ProcessStep
            {
                Id = "Activity_155wo4v",
                Name = "User task 1",
                StepId = 3,
                StepType = StepType.UserTask,
                NextSteps = new List<long> { 4 }
            },
            new ProcessStep
            {
                Id = "Activity_1lpbdhq",
                Name = "Service task",
                StepId = 4,
                StepType = StepType.ServiceTask,
                NextSteps = new List<long> { 5 }
            },
            new ProcessStep
            {
                Id = "Gateway_1obpwzy",
                StepId = 5,
                StepType = StepType.ExclusiveGateway,
                NextSteps = new List<long> { 6, 7 }
            },
            new ProcessStep
            {
                Id = "Activity_0drv8yp",
                StepId = 6,
                StepType = StepType.UserTask,
                NextSteps = new List<long> { 3 }
            },
            new ProcessStep
            {
                Id = "Activity_08cgb7k",
                StepId = 7,
                StepType = StepType.UserTask,
                NextSteps = new List<long> { 8 }
            },
            new ProcessStep
            {
                Id = "Event_0gs72rw",
                StepId = 8,
                Name = "Koniec",
                StepType = StepType.End,
                NextSteps = new List<long>()
            },
            new ProcessStep
            {
                Id = "Activity_029ecvu",
                StepId = 9,
                Name = "User task 2",
                StepType = StepType.UserTask,
                NextSteps = new List<long> { 8 }
            }
        };

        var actualJson = JsonConvert.SerializeObject(result, Formatting.Indented);
        var expectedJson = JsonConvert.SerializeObject(expected, Formatting.Indented);

        Assert.Equal(expectedJson, actualJson);
    }
}