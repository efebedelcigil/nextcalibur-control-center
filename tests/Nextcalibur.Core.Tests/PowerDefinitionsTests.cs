using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class PowerDefinitionsTests
{
    [Fact]
    public void A_value_that_agrees_with_the_definition_is_the_definers()
    {
        Assert.Equal(SetBy.Intel, PowerDefinitions.Who(45, 45));
        Assert.Equal(SetBy.Nvidia, PowerDefinitions.Who(70, 70, SetBy.Nvidia));
    }

    [Fact]
    public void A_value_that_differs_was_set_by_the_laptop_maker()
    {
        // The case this was written for: 65 W in force, 45 W defined.
        Assert.Equal(SetBy.LaptopMaker, PowerDefinitions.Who(65, 45));
        Assert.Equal(SetBy.LaptopMaker, PowerDefinitions.Who(75, 70, SetBy.Nvidia));
    }

    [Fact]
    public void Without_a_definition_it_is_the_laptop_makers()
    {
        Assert.Equal(SetBy.LaptopMaker, PowerDefinitions.Who(115, null));
    }

    [Fact]
    public void The_graphics_card_shows_NVIDIAs_default_beside_the_limit_in_force()
    {
        var defs = PowerDefinitions.GraphicsCard(new GpuPowerLimits(70, 75, 5, 105)).ToList();
        var limit = defs.Single(d => d.Key == "gpu.limit");
        Assert.Equal(75, limit.Value);
        Assert.Equal(70, limit.Defined);
        Assert.Equal(SetBy.LaptopMaker, limit.SetBy);
        Assert.Equal(SetBy.Nvidia, limit.DefinedBy);
    }
}
